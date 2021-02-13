/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc;
using OnTopic.Collections;
using OnTopic.Data.Transfer;
using OnTopic.Data.Transfer.Interchange;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.AspNetCore.Models.Metadata;
using OnTopic.Editor.AspNetCore.Models.Queryable;
using OnTopic.Editor.AspNetCore.Models.Transfer;
using OnTopic.Internal.Diagnostics;
using OnTopic.Mapping;
using OnTopic.Metadata;
using OnTopic.Querying;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Controllers {

  /*============================================================================================================================
  | CLASS: EDITOR CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides data access and processing for editor related functions.
  /// </summary>
  [Area("Editor")]
  public class EditorController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicMappingService            _topicMappingService;
    private                     Topic                           _currentTopic;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public EditorController(
      ITopicRepository topicRepository,
      ITopicMappingService topicMappingService
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(topicRepository, "A concrete implementation of an ITopicRepository is required.");
      Contract.Requires(topicMappingService, "A concrete implementation of an ITopicMappingService is required.");

      /*------------------------------------------------------------------------------------------------------------------------
      | Set values locally
      \-----------------------------------------------------------------------------------------------------------------------*/
      TopicRepository = topicRepository;
      _topicMappingService = topicMappingService;

    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the Topic Repository in order to gain arbitrary access to the entire topic graph.
    /// </summary>
    /// <returns>The TopicRepository associated with the controller.</returns>
    private ITopicRepository TopicRepository { get; }

    /*==========================================================================================================================
    | CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the current topic associated with the request.
    /// </summary>
    /// <returns>The Topic associated with the current request.</returns>
    private Topic CurrentTopic {
      get {
        if (_currentTopic is null) {
          _currentTopic = TopicRepository.Load(RouteData);
        }
        return _currentTopic;
      }
    }

    /*==========================================================================================================================
    | GET CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the a strongly typed content type, if available.
    /// </summary>
    /// <returns>The Content Type associated with the current request.</returns>
    private ContentTypeDescriptor GetContentType(string contentType) => TopicRepository
      .GetContentTypeDescriptors()
      .Where(t => t.Key.Equals(contentType?? "", StringComparison.Ordinal))
      .FirstOrDefault()??
      (ContentTypeDescriptor)TopicFactory.Create(contentType, "ContentTypeDescriptor");

    /*==========================================================================================================================
    | GET EDITOR VIEW MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given a <see cref="ContentTypeDescriptor"/> and any arguments from the corresponding action, constructs a new
    ///   <see cref="EditorViewModel"/>, including a mapped <see cref="EditingTopicViewModel"/>.
    /// </summary>
    /// <remarks>
    ///   This helps centralize the logic between e.g. <see cref="Edit(EditorBindingModel, Boolean, String, Boolean)"/> and <see
    ///   cref="Edit(Boolean, String, Boolean)"/>.
    /// </remarks>
    /// <param name="contentTypeDescriptor">The strongly-typed <see cref="ContentTypeDescriptor"/> of the topic.</param>
    /// <param name="isNew">Determines whether the topic represents a new or existing object.</param>
    /// <param name="isModal">Determines whether whether the view is being displayed within a modal window.</param>
    /// <returns>The Content Type associated with the current request.</returns>
    private async Task<T> GetEditorViewModel<T>(
      ContentTypeDescriptor contentTypeDescriptor,
      bool isNew,
      bool isModal
    ) where T: EditorViewModel, new() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(contentTypeDescriptor, nameof(contentTypeDescriptor));

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH CONTENT TYPE VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypeViewModel  = await _topicMappingService.MapAsync<ContentTypeDescriptorTopicViewModel>(contentTypeDescriptor).ConfigureAwait(true);
      var parentTopic           = isNew ? CurrentTopic : CurrentTopic.Parent;

      /*------------------------------------------------------------------------------------------------------------------------
      | CONSTRUCT VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModel        = await _topicMappingService.MapAsync<EditingTopicViewModel>(CurrentTopic).ConfigureAwait(true);

      if (isNew) {
        topicViewModel          = new() {
          ContentType           = contentTypeDescriptor.Key,
          UniqueKey             = CurrentTopic.GetUniqueKey(),
          Parent                = topicViewModel
        };
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | ASSIGN ATTRIBUTES
      \-----------------------------------------------------------------------------------------------------------------------*/
      //The attribute collections follow special conventions that can't be automatically mapped from the topic
      foreach (var attribute in contentTypeDescriptor.AttributeDescriptors) {

        //Serialize relationships, if it's a relationship type
        if (!isNew && attribute.ModelType is ModelType.Relationship) {
          var relatedTopicIds = CurrentTopic.Relationships.GetValues(attribute.Key).Select<Topic, int>(m => m.Id).ToArray();
          topicViewModel.Attributes.Add(attribute.Key, String.Join(",", relatedTopicIds));
        }

        //Serialize references, if it's a topic reference
        else if (!isNew && attribute.ModelType is ModelType.Reference) {
          topicViewModel.Attributes.Add(attribute.Key, CurrentTopic.References.GetValue(attribute.Key)?.Id.ToString(CultureInfo.InvariantCulture));
        }

        //Provide special handling for Key, since it's not stored as an attribute
        else if (attribute.Key is "Key") {
          topicViewModel.Attributes.Add(attribute.Key, CurrentTopic.Key);
        }

        //For existing topics, get locally assigned attributes
        else if (!isNew) {
          topicViewModel.Attributes.Add(attribute.Key, CurrentTopic.Attributes.GetValue(attribute.Key, null, false, false));
        }

        //For new topics that aren't derived from another topic, assign attribute default, if available
        else if (CurrentTopic.BaseTopic is null && !attribute.IsRequired && attribute.DefaultValue is not null) {
          topicViewModel.Attributes.Add(attribute.Key, attribute.DefaultValue);
        }

        //Otherwise, assign a null value; that way, all attributes are guaranteed to be accounted for
        else {
          topicViewModel.Attributes.Add(attribute.Key, null);
        }

        //Set inherited attribute value, if available
        topicViewModel.InheritedAttributes.Add(
          attribute.Key,
          isNew? null : CurrentTopic.BaseTopic?.Attributes.GetValue(attribute.Key)
        );

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH AND RETURN VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      return new T() {
        Topic                   = topicViewModel,
        ContentTypeDescriptor   = contentTypeViewModel,
        IsNew                   = isNew,
        IsModal                 = isModal,
        IsFullyLoaded           = CurrentTopic.Relationships.IsFullyLoaded && CurrentTopic.References.IsFullyLoaded
      };

    }

    /*==========================================================================================================================
    | [GET] EDIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Present an editor view bound to a specific topic.
    /// </summary>
    /// <param name="isNew">Determines whether the topic represents a new or existing object.</param>
    /// <param name="contentType">The key name of the <see cref="ContentTypeDescriptor"/> representing the topic.</param>
    /// <param name="isModal">Determines whether whether the view is being displayed within a modal window.</param>
    public async Task<IActionResult> Edit(bool isNew = false, string contentType = null, bool isModal = false) {

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH CONTENT TYPE VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypeDescriptor = GetContentType(contentType?? CurrentTopic.ContentType);

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var editorViewModel = await GetEditorViewModel<EditorViewModel>(contentTypeDescriptor, isNew, isModal).ConfigureAwait(true);

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN VIEW (MODEL)
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(editorViewModel);

    }

    /*==========================================================================================================================
    | [POST] EDIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Handles postback from the editor, based on an <see cref="EditorBindingModel"/>.
    /// </summary>
    /// <param name="model">An instance of the <see cref="EditorBindingModel"/> constructed from the HTTP Post.</param>
    /// <param name="isNew">Determines whether the topic represents a new or existing object.</param>
    /// <param name="contentType">The key name of the <see cref="ContentTypeDescriptor"/> representing the topic.</param>
    /// <param name="isModal">Determines whether whether the view is being displayed within a modal window.</param>
    [HttpPost]
    public async Task<IActionResult> Edit(
      EditorBindingModel model,
      bool isNew = false,
      string contentType = null,
      bool isModal = false
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(model, nameof(model));

      /*------------------------------------------------------------------------------------------------------------------------
      | SET VARIABLES
      \-----------------------------------------------------------------------------------------------------------------------*/
      var parentTopic           = isNew? CurrentTopic : CurrentTopic.Parent;
      var contentTypeDescriptor = GetContentType(contentType?? CurrentTopic.ContentType);
      var baseTopicId           = model.Attributes.GetInteger("BaseTopic");
      var baseTopic             = baseTopicId.HasValue? TopicRepository.Load(baseTopicId.Value) : null;
      var newKey                = model.Attributes.GetValue("Key");

      /*------------------------------------------------------------------------------------------------------------------------
      | VALIDATE REQUIRED FIELDS
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (baseTopic is null) {
        foreach (var attribute in contentTypeDescriptor.AttributeDescriptors) {
          var submittedValue = model.Attributes.GetValue(attribute.Key);
          if (attribute.IsRequired && !attribute.IsHidden && String.IsNullOrEmpty(submittedValue)) {
            ModelState.AddModelError(attribute.Key, $"The {attribute.Title} field is required.");
          }
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | INHERIT KEY VALUE, IF PRESENT
      \-----------------------------------------------------------------------------------------------------------------------*/
      else if (String.IsNullOrEmpty(newKey)) {
        newKey = baseTopic.Key;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | VALIDATE KEY
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (isNew || !CurrentTopic.Key.Equals(newKey, StringComparison.OrdinalIgnoreCase)) {
        if (parentTopic.Children.Contains(newKey)) {
          ModelState.AddModelError(
            "Key",
            $"The folder name {newKey} already exists under '{parentTopic.Title}'. Please choose a unique folder name."
          );
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN ERROR STATE
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!ModelState.IsValid) {

        //Establish view model
        var editorViewModel = await GetEditorViewModel<EditorViewModel>(contentTypeDescriptor, isNew, isModal).ConfigureAwait(true);

        foreach (var attribute in contentTypeDescriptor.AttributeDescriptors) {
          editorViewModel.Topic.Attributes[attribute.Key] = model.Attributes.GetValue(attribute.Key);
        }

        return View(editorViewModel);

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH TOPIC
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topic                 = CurrentTopic;

      if (isNew) {
        topic = TopicFactory.Create(newKey, contentType, CurrentTopic);
      }
      else {
        contentType = CurrentTopic.ContentType;
      }

      if (baseTopic is not null && topic.BaseTopic != baseTopic) {
        topic.BaseTopic = baseTopic;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | SET ATTRIBUTES
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var attribute in GetContentType(contentType).AttributeDescriptors) {

        //Handle hidden attributes
        if (attribute.IsHidden) {
          continue;
        }

        //Handle new keys
        if (isNew && attribute.Key.Equals("Key", StringComparison.OrdinalIgnoreCase)) {
          continue;
        }

        //Handle missing attributes
        if (!model.Attributes.Contains(attribute.Key)) {
          topic.Attributes.Remove(attribute.Key);
          continue;
        }

        //Get reference to current instance
        var attributeValue = model.Attributes[attribute.Key];

        //Save value
        if (attribute.ModelType is ModelType.Relationship) {
          SetRelationships(topic, attribute, attributeValue);
        }
        else if (attribute.ModelType is ModelType.Reference) {
          SetReference(topic, attribute, attributeValue);
        }
        else if (attribute.Key is "Key") {
          topic.Key = attributeValue.Value.Replace(" ", "", StringComparison.Ordinal);
        }
        else {
          topic.Attributes.SetValue(attribute.Key, attributeValue.Value);
        }

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | SAVE VALUE
      \-----------------------------------------------------------------------------------------------------------------------*/
      TopicRepository.Save(topic);

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN INDEX
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (isModal) {
        return View("CloseModal");
      }
      else if (isNew) {
        return RedirectToAction("Edit", new { path = topic.GetWebPath() });
      }
      return await Edit().ConfigureAwait(true);

    }

    /*==========================================================================================================================
    | SET RELATIONSHIPS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Private helper function that saves relationship values to the topic.
    /// </summary>
    private void SetRelationships(Topic topic, AttributeDescriptor attribute, AttributeBindingModel attributeValue) {
      var relatedTopics = attributeValue.Value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
      topic.Relationships.Clear(attribute.Key);
      foreach (var topicIdString in relatedTopics) {
        Topic relatedTopic = null;
        var isTopicId = Int32.TryParse(topicIdString, out var topicIdInt);
        if (isTopicId && topicIdInt > 0) {
          relatedTopic = TopicRepository.Load(topicIdInt);
        }
        if (relatedTopic is not null) {
          topic.Relationships.SetValue(attribute.Key, relatedTopic);
        }
      }
    }

    /*==========================================================================================================================
    | SET REFERENCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Private helper function that saves a topic reference to the topic.
    /// </summary>
    private void SetReference(Topic topic, AttributeDescriptor attribute, AttributeBindingModel attributeValue) {
      Topic referencedTopic = null;
      var isTopicId = Int32.TryParse(attributeValue.Value, out var topicIdInt);
      if (isTopicId && topicIdInt > 0) {
        referencedTopic = TopicRepository.Load(topicIdInt);
      }
      topic.References.SetValue(attribute.Key, referencedTopic);
    }

    /*============================================================================================================================
    | [POST] SET TOPIC VERSION
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Calls Topic.Rollback() with the selected version datetime to set the data to that version and re-save the Topic.
    /// </summary>
    [HttpGet]
    public IActionResult SetVersion(DateTime version, bool isModal = false) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Initiate rollback
      \-------------------------------------------------------------------------------------------------------------------------*/
      TopicRepository.Rollback(CurrentTopic, version);

      /*--------------------------------------------------------------------------------------------------------------------------
      | Render index
      \-------------------------------------------------------------------------------------------------------------------------*/
      return RedirectToAction("Edit", new { path = CurrentTopic.GetWebPath(), IsModal = isModal });

    }

    /*============================================================================================================================
    | [POST] DELETE TOPIC
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Fires when the user clicks the "Delete" button; deletes the current topic and any child attributes.
    /// </summary>
    [HttpPost, HttpGet]
    public IActionResult Delete(bool isModal = false) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Define variables
      \-------------------------------------------------------------------------------------------------------------------------*/
      var parent = CurrentTopic.Parent;
      var deletedTopic = CurrentTopic.Title;

      /*--------------------------------------------------------------------------------------------------------------------------
      | Lock the Topic repository before executing the delete
      \-------------------------------------------------------------------------------------------------------------------------*/
      lock (TopicRepository) {
        TopicRepository.Delete(CurrentTopic, true);
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | If the editor is in modal view, close the window; otherwise, redirect to the parent topic.
      \-------------------------------------------------------------------------------------------------------------------------*/
      if (isModal) {
        return View("CloseModal");
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | If the content type is a nested list, display grandparent.
      \-------------------------------------------------------------------------------------------------------------------------*/
      else if (parent.Attributes.GetValue("ContentType", "") is "List") {
        return RedirectToAction(
          "Edit",
          new {
            path                = parent.Parent.GetWebPath(),
            DeletedTopic        = deletedTopic,
            DeletedFrom         = parent.Title,
            Action              = "Deleted"
          }
        );
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | Redirect to parent
      \-------------------------------------------------------------------------------------------------------------------------*/
      else {
        return RedirectToAction("Edit", new { path = parent.GetWebPath() });
      }

    }

    /*============================================================================================================================
    | [POST] MOVE
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   AJAX-parsable, querystring-configurable wrapper to the OnTopic engine code that moves a node from one place in the
    ///   hierarchy to another. "true" if succeeded, Returns "false" if failure, as string values. The JS throws a generic
    ///   "failure" error on "false".
    /// </summary>
    /// <param name="topicId">The <see cref="Topic.Id"/> of the topic to be moved.</param>
    /// <param name="targetTopicId">The <see cref="Topic.Id"/> of the parent topic. <c>-1</c> implies the same parent.</param>
    /// <param name="siblingId">
    ///   The <see cref="Topic.Id"/> of the sibling to place the <paramref name="topicId"/> after. <c>0</c> implies it should be
    ///   placed at the end.
    /// </param>
    [HttpPost]
    public IActionResult Move(int topicId, int targetTopicId = -1, int siblingId = -1) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Retrieve the source and destination topics
      \-------------------------------------------------------------------------------------------------------------------------*/
      var topic = TopicRepository.Load(topicId);
      var target = (targetTopicId >= 0)? TopicRepository.Load(targetTopicId) : topic.Parent;

      /*--------------------------------------------------------------------------------------------------------------------------
      | Move the topic and/or reorder it with its siblings; lock the Topic repository prior to execution
      \-------------------------------------------------------------------------------------------------------------------------*/
      lock (TopicRepository) {
        if (siblingId > 0) {
          var sibling = TopicRepository.Load(siblingId);
          TopicRepository.Move(topic, target, sibling);
        }
        else {
          TopicRepository.Move(topic, target);
        }
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | Return
      \-------------------------------------------------------------------------------------------------------------------------*/
      return Content("true");

    }

    /*==========================================================================================================================
    | JSON
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves JSON the specified topic, and all of its children.
    /// </summary>
    public JsonResult Json(TopicQueryOptions options) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(options, nameof(options));

      /*--------------------------------------------------------------------------------------------------------------------------
      | Get related topics
      \-------------------------------------------------------------------------------------------------------------------------*/
      var relatedTopics = (ReadOnlyTopicCollection)null;

      if (options.MarkRelated) {

        var relatedTopic = CurrentTopic;
        if (options.RelatedTopicId > 0) {
          relatedTopic = TopicRepository.Load(options.RelatedTopicId);
        }

        if (!String.IsNullOrWhiteSpace(options.RelatedNamespace)) {
          relatedTopics = new(relatedTopic.Relationships.GetValues(options.RelatedNamespace));
        }
        else {
          relatedTopics = relatedTopic.Relationships.GetAllValues();
        }

      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | Assemble view model
      \-------------------------------------------------------------------------------------------------------------------------*/
      var jsonTopicMappingService = new TopicQueryService();
      var jsonTopicViewModel = jsonTopicMappingService.Query(CurrentTopic, options, relatedTopics);

      /*--------------------------------------------------------------------------------------------------------------------------
      | Return hierarchical view
      \-------------------------------------------------------------------------------------------------------------------------*/
      return new(jsonTopicViewModel);

    }

    /*==========================================================================================================================
    | [GET] EXPORT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Presents options for exporting the current topic.
    /// </summary>
    public async Task<IActionResult> Export() {

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH CONTENT TYPE VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypeDescriptor = GetContentType(CurrentTopic.ContentType);

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var editorViewModel = await GetEditorViewModel<ExportViewModel>(contentTypeDescriptor, false, false).ConfigureAwait(true);

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN VIEW (MODEL)
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(editorViewModel);

    }

    /*==========================================================================================================================
    | [POST] EXPORT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Exports the current topic, based on any specified <see cref="ExportOptions"/>.
    /// </summary>
    /// <param name="options">The <see cref="ExportOptions"/> for determing what values should be exported.</param>
    [HttpPost]
    public IActionResult Export([Bind(Prefix="ExportOptions")]ExportOptions options) {

      /*------------------------------------------------------------------------------------------------------------------------
      | EXPORT TO JSON
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicData             = CurrentTopic.Export(options);
      var json                  = JsonSerializer.Serialize(topicData, new() { IgnoreNullValues = true });
      var jsonStream            =  new MemoryStream(Encoding.UTF8.GetBytes(json), false);

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN JSON
      \-----------------------------------------------------------------------------------------------------------------------*/
      return new FileStreamResult(jsonStream, "application/json") {
        FileDownloadName        = CurrentTopic.GetUniqueKey().Replace(":", ".", StringComparison.Ordinal) + ".json"
      };

    }

    /*==========================================================================================================================
    | [GET] IMPORT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Presents options for importing the current topic.
    /// </summary>
    public async Task<IActionResult> Import() {

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH CONTENT TYPE VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypeDescriptor = GetContentType(CurrentTopic.ContentType);

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var editorViewModel = await GetEditorViewModel<ImportViewModel>(contentTypeDescriptor, false, false).ConfigureAwait(true);

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN VIEW (MODEL)
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(editorViewModel);

    }

    /*==========================================================================================================================
    | [POST] IMPORT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Imports the current topic, based on any specified <see cref="ImportOptions"/>.
    /// </summary>
    /// <param name="options">The <see cref="ImportOptions"/> for determing how the file should be imported.</param>
    [HttpPost]
    public async Task<IActionResult> Import(IFormFile jsonFile, [Bind(Prefix = "ImportOptions")]ImportOptions options) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(jsonFile, nameof(jsonFile));
      Contract.Requires(options, nameof(options));

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH CONTENT TYPE VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypeDescriptor = GetContentType(CurrentTopic.ContentType);

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var editorViewModel       = await GetEditorViewModel<ImportViewModel>(contentTypeDescriptor, false, false).ConfigureAwait(true);

      options.CurrentUser       = HttpContext.User.Identity.Name?? "System";

      editorViewModel           = editorViewModel with {
        ImportOptions           = options
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | VALIDATE PARAMETERS
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (jsonFile is null) {
        ModelState.AddModelError("jsonFile", "The JSON file is required to import data.");
        return View(editorViewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | READ JSON
      \-----------------------------------------------------------------------------------------------------------------------*/
      var json                  = new StringBuilder();
      using (var reader = new StreamReader(jsonFile.OpenReadStream())) {
        while (reader.Peek() >= 0) {
          json.AppendLine(await reader.ReadLineAsync().ConfigureAwait(true));
        }
      }
      var jsonString            = json.ToString();
      var jsonOptions           = new JsonSerializerOptions() {
        PropertyNameCaseInsensitive = true
      };
      var topicData             = JsonSerializer.Deserialize<TopicData>(jsonString, jsonOptions);

      if (topicData is null) {
        ModelState.AddModelError("jsonFile", "The JSON file could not be read correctly.");
        return View(editorViewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | IDENTIFY TARGET TOPIC
      \-----------------------------------------------------------------------------------------------------------------------*/
      var uniqueKey             = topicData.UniqueKey;
      var target                = TopicRepository.Load(uniqueKey);

      //Create target if it doesn't exist
      if (target is null) {
        var parentKey           = uniqueKey.Substring(0, uniqueKey.LastIndexOf(":", StringComparison.Ordinal));
        var parent              = TopicRepository.Load(parentKey);

        if (parent is not null) {
          target                = TopicFactory.Create(topicData.Key, topicData.ContentType, parent);
        }
      }

      if (target is null) {
        ModelState.AddModelError(
          "jsonFile",
          $"The root namespace, '{topicData.UniqueKey}', is not available in the topic graph"
        );
        return View(editorViewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | INDEX TOPICS IN SCOPE
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topics                = target.FindAll(t => !t.IsNew).ToList();

      /*------------------------------------------------------------------------------------------------------------------------
      | IMPORT
      \-----------------------------------------------------------------------------------------------------------------------*/
      target.Import(topicData, options);

      /*------------------------------------------------------------------------------------------------------------------------
      | DELETE UNMATCHED TOPICS
      >-------------------------------------------------------------------------------------------------------------------------
      | ### HACK JJC20200327: The Data Transfer library doesn't have access to the ITopicRepository, so it can't delete topics.
      | Instead, it removes them from the topic graph. But the ITopicRepository implementations don't have a means of detecting
      | removed topics during a recursive save and, therefore, the deletions aren't persisted to the database. To mitigate this,
      | we evaluate the topic graph after the save, and then delete any orphans.
      \-----------------------------------------------------------------------------------------------------------------------*/
      var unmatchedTopics       = topics.Except(target.FindAll(t => !t.IsNew));

      foreach (var unmatchedTopic in unmatchedTopics) {
        TopicRepository.Delete(unmatchedTopic, true);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | SET SAVE SCOPE
      >-------------------------------------------------------------------------------------------------------------------------
      | ### HACK JJC20200519: If the parent hasn't been saved, then it should be set to the target to be saved. This should only
      | happen when working with an empty database, in which case the Root topic will be autogenerated by TopicRepositoryBase.
      | Otherwise, Save() will generate an error since the parent ID won't be found.
      \-----------------------------------------------------------------------------------------------------------------------*/
      var saveRoot              = target;
      if (target.Parent.IsNew) {
        saveRoot = target.Parent;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | SAVE
      \-----------------------------------------------------------------------------------------------------------------------*/
      TopicRepository.Save(saveRoot, topicData.Children.Count > 0);

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN JSON
      \-----------------------------------------------------------------------------------------------------------------------*/
      editorViewModel           = editorViewModel with {
        IsImported              = true
      };

      return View(editorViewModel);

    }

  } // Class
} // Namespace