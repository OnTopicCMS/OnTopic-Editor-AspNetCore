/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ignia.Topics.Collections;
using Ignia.Topics.AspNetCore.Mvc;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components.BindingModels;
using Ignia.Topics.Editor.Models.Queryable;
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Internal.Diagnostics;
using Ignia.Topics.Mapping;
using Ignia.Topics.Metadata;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ignia.Topics.Editor.Mvc.Controllers {

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
    private readonly            ITopicRepository                _topicRepository;
    private readonly            ITopicMappingService            _topicMappingService;
    private                     Topic                           _currentTopic                   = null;

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
      Contract.Requires(topicRepository != null, "A concrete implementation of an ITopicRepository is required.");
      Contract.Requires(topicMappingService != null, "A concrete implementation of an ITopicMappingService is required.");

      /*------------------------------------------------------------------------------------------------------------------------
      | Set values locally
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository = topicRepository;
      _topicMappingService = topicMappingService;

    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the Topic Repository in order to gain arbitrary access to the entire topic graph.
    /// </summary>
    /// <returns>The TopicRepository associated with the controller.</returns>
    protected ITopicRepository TopicRepository => _topicRepository;

    /*==========================================================================================================================
    | CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the current topic associated with the request.
    /// </summary>
    /// <returns>The Topic associated with the current request.</returns>
    protected Topic CurrentTopic {
      get {
        if (_currentTopic == null) {
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
    protected ContentTypeDescriptor GetContentType(string contentType) => _topicRepository
      .GetContentTypeDescriptors()
      .Where(t => t.Key.Equals(contentType))
      .First();

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
    protected async Task<EditorViewModel> GetEditorViewModel(
      ContentTypeDescriptor contentTypeDescriptor,
      bool isNew,
      bool isModal
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH CONTENT TYPE VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypeViewModel  = await _topicMappingService.MapAsync<ContentTypeDescriptorTopicViewModel>(contentTypeDescriptor);
      var parentTopic           = isNew ? CurrentTopic : CurrentTopic.Parent;

      /*------------------------------------------------------------------------------------------------------------------------
      | CONSTRUCT VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModel        = await _topicMappingService.MapAsync<EditingTopicViewModel>(CurrentTopic);

      if (isNew) {
        topicViewModel          = new EditingTopicViewModel() {
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

        //For existing topics, get locally assigned attributes
        if (!isNew) {
          topicViewModel.Attributes.Add(attribute.Key, CurrentTopic.Attributes.GetValue(attribute.Key, null, false, false));
        }

        //For new topics that aren't derived from another topic, assign attribute default, if available
        else if (CurrentTopic.DerivedTopic == null && !attribute.IsRequired && attribute.DefaultValue != null) {
          topicViewModel.Attributes.Add(attribute.Key, attribute.DefaultValue);
        }

        //Otherwise, assign a null value; that way, all attributes are guaranteed to be accounted for
        else {
          topicViewModel.Attributes.Add(attribute.Key, null);
        }

        //Set inherited attribute value, if available
        topicViewModel.InheritedAttributes.Add(attribute.Key, parentTopic.Attributes.GetValue(attribute.Key, true));

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH AND RETURN VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      return new EditorViewModel(topicViewModel, contentTypeViewModel, isNew, isModal);

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
      var editorViewModel = await GetEditorViewModel(contentTypeDescriptor, isNew, isModal);

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
      | SET VARIABLES
      \-----------------------------------------------------------------------------------------------------------------------*/
      var parentTopic           = isNew? CurrentTopic : CurrentTopic.Parent;
      var contentTypeDescriptor = GetContentType(contentType?? CurrentTopic.ContentType);
      var derivedTopicValue     = model.Attributes.Contains("TopicID")? model.Attributes["TopicID"].Value : "-1";
      var derivedTopicId        = String.IsNullOrWhiteSpace(derivedTopicValue)? -1 : Int32.Parse(derivedTopicValue);
      var derivedTopic          = (derivedTopicId >= 0)? TopicRepository.Load(derivedTopicId) : null;
      var newKey                = model.Attributes.Contains("Key")? model.Attributes["Key"].Value : null;

      /*------------------------------------------------------------------------------------------------------------------------
      | VALIDATE REQUIRED FIELDS
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (derivedTopic == null) {
        foreach (var attribute in contentTypeDescriptor.AttributeDescriptors) {
          var submittedValue = model.Attributes.Contains(attribute.Key)? model.Attributes[attribute.Key] : null;
          if (attribute.IsRequired && !attribute.IsHidden && String.IsNullOrEmpty(submittedValue?.Value)) {
            ModelState.AddModelError(attribute.Key, $"The {attribute.Title} field is required.");
          }
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | INHERIT KEY VALUE, IF PRESENT
      \-----------------------------------------------------------------------------------------------------------------------*/
      else if (String.IsNullOrEmpty(newKey)) {
        newKey = derivedTopic.Key;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | VALIDATE KEY
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (isNew || !CurrentTopic.Key.Equals(newKey, StringComparison.InvariantCultureIgnoreCase)) {
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
        var editorViewModel = await GetEditorViewModel(contentTypeDescriptor, isNew, isModal);

        foreach (var attribute in contentTypeDescriptor.AttributeDescriptors) {
          var submittedValue = model.Attributes.Contains(attribute.Key)? model.Attributes[attribute.Key] : null;
          editorViewModel.Topic.Attributes[attribute.Key] = submittedValue?.Value;
        }

        return View(editorViewModel);

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH TOPIC
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topic                 = CurrentTopic;

      if (isNew) {
        topic = TopicFactory.Create(newKey, contentType);
      }
      else {
        contentType = CurrentTopic.ContentType;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | SET ATTRIBUTES
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var attribute in GetContentType(contentType).AttributeDescriptors) {

        //Handle hidden attributes
        if (attribute.IsHidden) {
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
        if (attribute.ModelType.Equals(ModelType.Relationship)) {
          SetRelationships(topic, attribute, attributeValue);
        }
        else if (attribute.Key.Equals("Key")) {
          topic.Key = attributeValue.Value.Replace(" ", "");
        }
        else if (String.IsNullOrEmpty(attributeValue.Value)) {
          topic.Attributes.Remove(attribute.Key);
        }
        else {
          topic.Attributes.SetValue(attribute.Key, attributeValue.Value);
        }

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | SET PARENT
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (isNew) {
        topic.Parent = CurrentTopic;
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | If the editor is in modal view, close the window; otherwise, redirect to the parent topic.
      \-------------------------------------------------------------------------------------------------------------------------*/
      if (isModal) {
        return View("CloseModal");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | SAVE VALUE
      \-----------------------------------------------------------------------------------------------------------------------*/
      //TopicRepository.Save(CurrentTopic);

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN INDEX
      \-----------------------------------------------------------------------------------------------------------------------*/
      return await Edit();

    }

    /*==========================================================================================================================
    | SET RELATIONSHIP VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Private helper function that saves relationship values to the topic.
    /// </summary>
    private void SetRelationships(Topic topic, AttributeDescriptor attribute, AttributeBindingModel attributeValue) {
      var relatedTopics = attributeValue.Value.Split(',').ToList();
      topic.Relationships.ClearTopics(attribute.Key);
      foreach (var topicIdString in relatedTopics) {
        Topic relatedTopic = null;
        var isTopicId = Int32.TryParse(topicIdString, out var topicIdInt);
        if (isTopicId && topicIdInt > 0) {
          relatedTopic = TopicRepository.Load(topicIdInt);
        }
        if (relatedTopic != null) {
          topic.Relationships.SetTopic(attribute.Key, relatedTopic);
        }
      }
    }

    /*============================================================================================================================
    | [POST] SET TOPIC VERSION
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Calls Topic.Rollback() with the selected version datetime to set the data to that version and re-save the Topic.
    /// </summary>
    [HttpGet]
    public IActionResult SetVersion(DateTime version) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Initiate rollback
      \-------------------------------------------------------------------------------------------------------------------------*/
      //TopicRepository.Rollback(CurrentTopic, version);

      /*--------------------------------------------------------------------------------------------------------------------------
      | Render index
      \-------------------------------------------------------------------------------------------------------------------------*/
      return RedirectToAction("Edit", new { path = CurrentTopic.GetWebPath() });

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
      //TopicRepository.Delete(CurrentTopic);
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | If the editor is in modal view, close the window; otherwise, redirect to the parent topic.
      \-------------------------------------------------------------------------------------------------------------------------*/
      if (isModal) {
        return View("CloseModal");
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | If the content type is a nested list, display parent.
      \-------------------------------------------------------------------------------------------------------------------------*/
      else if (parent.Attributes.GetValue("ContentType", "") == "List") {
        return Redirect("/OnTopic/Edit" + parent.Parent.GetWebPath() + "?DeletedTopic=" + deletedTopic + "&DeletedFrom=" + parent.Title + "&Action=Deleted");
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | Redirect to parent
      \-------------------------------------------------------------------------------------------------------------------------*/
      else {
        return Redirect("/OnTopic/Edit" + parent.GetWebPath());
      }

    }

    /*============================================================================================================================
    | [POST] MOVE
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   AJAX-parsable, querystring-configurable wrapper to the Ignia.Topics engine code that moves a node from one place in the
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
      | Reset the source topic's Parent
      \-------------------------------------------------------------------------------------------------------------------------*/
      topic.Parent = target;

      /*--------------------------------------------------------------------------------------------------------------------------
      | Move the topic and/or reorder it with its siblings; lock the Topic repository prior to execution
      \-------------------------------------------------------------------------------------------------------------------------*/
      lock (TopicRepository) {
        if (siblingId > 0) {
          var sibling = TopicRepository.Load(siblingId);
        //TopicRepository.Move(topic, target, sibling);
        }
        else {
        //TopicRepository.Move(topic, target);
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

      /*--------------------------------------------------------------------------------------------------------------------------
      | Get related topics
      \-------------------------------------------------------------------------------------------------------------------------*/
      var relatedTopics = (ReadOnlyTopicCollection<Topic>)null;

      if (options.MarkRelated) {

        var relatedTopic = CurrentTopic;
        if (options.RelatedTopicId > 0) {
          relatedTopic = TopicRepository.Load(options.RelatedTopicId);
        }

        if (!String.IsNullOrWhiteSpace(options.RelatedNamespace)) {
          relatedTopics = new ReadOnlyTopicCollection<Topic>(relatedTopic.Relationships.GetTopics(options.RelatedNamespace));
        }
        else {
          relatedTopics = relatedTopic.Relationships.GetAllTopics();
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
      return new JsonResult(jsonTopicViewModel);

    }

  } // Class

} // Namespace