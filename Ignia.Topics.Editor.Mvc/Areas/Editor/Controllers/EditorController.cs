/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ignia.Topics.Collections;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Attributes;
using Ignia.Topics.Repositories;

namespace Ignia.Topics.Editor.Mvc.Controllers {

  /*============================================================================================================================
  | CLASS: EDITOR CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides data access and processing for editor related functions.
  /// </summary>
  [RouteArea("Editor", AreaPrefix = "")]
  public class EditorController : Controller {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     ITopicRepository                _topicRepository                = null;
    private                     Topic                           _currentTopic                   = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a Topic Controller with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public EditorController(ITopicRepository topicRepository, Topic currentTopic) {
      _topicRepository = topicRepository;
      _currentTopic = currentTopic;
    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the Topic Repository in order to gain arbitrary access to the entire topic graph.
    /// </summary>
    /// <returns>The TopicRepository associated with the controller.</returns>
    protected ITopicRepository TopicRepository {
    	get {
        return _topicRepository;
      }
    }

    /*==========================================================================================================================
    | CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the current topic associated with the request.
    /// </summary>
    /// <returns>The Topic associated with the current request.</returns>
    protected Topic CurrentTopic {
      get {
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
    protected ContentType GetContentType(string contentType) {
      return TopicRepository.GetContentTypes().Where(t => t.Key.Equals(contentType)).First();
    }

    /*==========================================================================================================================
    | [GET] INDEX
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Present an editor view bound to a specific topic.
    /// </summary>
    public ActionResult Index(bool isNew = false, string contentType = null, bool isModal = false) {

      /*------------------------------------------------------------------------------------------------------------------------
      | FILTER CONTENT TYPES
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypes = new TopicCollection<ContentType>();
      var currentContentType = GetContentType(CurrentTopic.ContentType);

      if (currentContentType.PermittedContentTypes.Count >= 0) {
        foreach (var permittedContentType in currentContentType.PermittedContentTypes) {
          contentTypes.Add(permittedContentType);
        }
      }
      else {
        contentTypes = TopicRepository.GetContentTypes();
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | CONSTRUCT VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      EditorViewModel editorViewModel;
      if (isNew) {
        editorViewModel = new EditorViewModel(Topic.Create("NewTopic", contentType), GetContentType(contentType), contentTypes);
      }
      else {
        editorViewModel = new EditorViewModel(CurrentTopic, GetContentType(CurrentTopic.ContentType), contentTypes);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN VIEW
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(editorViewModel);

    }

    /*==========================================================================================================================
    | [POST] INDEX
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Handles postback from the editor, based on an <see cref="EditorBindingModel"/>.
    /// </summary>
    /// <param name="model">An instance of the <see cref="EditorBindingModel"/> constructed from the HTTP Post.</param>
    [HttpPost]
    public ActionResult Index(EditorBindingModel model, bool isNew = false, string contentType = null, bool isModal = false) {

      /*------------------------------------------------------------------------------------------------------------------------
      | SET TOPIC
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topic = CurrentTopic;

      if (isNew) {
        topic = Topic.Create("NewTopic", contentType);
      }
      else {
        contentType = CurrentTopic.ContentType;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | SET ATTRIBUTES
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var attribute in GetContentType(contentType).SupportedAttributes) {

        //Handle hidden attributes
        if (attribute.IsHidden) {
          continue;
        }

        //Get reference to current instance
        var attributeValue = model.Attributes[attribute.Key];

        //Save value
        if (attribute.Type.Equals("Relationship")) {
          SetRelationships(topic, attribute, attributeValue);
        }
        else if (attribute.Key.Equals("Key")) {
          CurrentTopic.Key = attributeValue.Value.TrimStart(' ').TrimEnd(' ').Replace(" ", "");
        }
        else if (String.IsNullOrEmpty(attributeValue.Value)) {
          CurrentTopic.Attributes.Remove(attribute.Key);
        }
        else {
          CurrentTopic.Attributes.SetValue(attribute.Key, attributeValue.Value);
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
      return Index();

    }

    /*==========================================================================================================================
    | SET RELATIONSHIP VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Private helper function that saves relationship values to the topic.
    /// </summary>
    private void SetRelationships(Topic topic, Attribute attribute, EditorAttribute attributeValue) {
      var relatedTopics = attributeValue.Value.Split(',').ToList();
      topic.Relationships.ClearTopics(attribute.Key);
      foreach (var topicIdString in relatedTopics) {
        Topic relatedTopic = null;
        var isTopicId = Int32.TryParse(topicIdString, out int topicIdInt);
        if (isTopicId && topicIdInt > 0) {
          relatedTopic = TopicRepository.Load().GetTopic(topicIdInt);
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
    [HttpPost]
    public ActionResult SetVersion(DateTime version) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Initiate rollback
      \-------------------------------------------------------------------------------------------------------------------------*/
      TopicRepository.Rollback(CurrentTopic, version);

      /*--------------------------------------------------------------------------------------------------------------------------
      | Render index
      \-------------------------------------------------------------------------------------------------------------------------*/
      return Index();

    }

    /*============================================================================================================================
    | [POST] DELETE TOPIC
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Fires when the user clicks the "Delete" button; deletes the current topic and any child attributes.
    /// </summary>
    [HttpPost]
    public ActionResult Delete(bool isModal = false) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Define variables
      \-------------------------------------------------------------------------------------------------------------------------*/
      var parent = CurrentTopic.Parent;
      var deletedTopic = CurrentTopic.Title;

      /*--------------------------------------------------------------------------------------------------------------------------
      | Lock the Topic repository before executing the delete
      \-------------------------------------------------------------------------------------------------------------------------*/
      lock (TopicRepository) {
        TopicRepository.Delete(CurrentTopic);
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
        return Redirect("?Path=" + parent.Parent.UniqueKey + "&DeletedTopic=" + deletedTopic + "&DeletedFrom=" + parent.Title + "&Action=Deleted");
      }

      /*--------------------------------------------------------------------------------------------------------------------------
      | Redirect to parent
      \-------------------------------------------------------------------------------------------------------------------------*/
      return Redirect("?Path=" + parent.UniqueKey);

    }

    /*============================================================================================================================
    | [POST] MOVE
    \---------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   AJAX-parsable, querystring-configurable wrapper to the Ignia.Topics engine code that moves a node from one place in the
    ///   hierarchy to another. "true" if succeeded, Returns "false" if failure, as string values. The JS throws a generic
    ///   "failure" error on "false".
    /// </summary>
    [HttpPost]
    public ActionResult Move(int topicId, int targetTopicId, int siblingId) {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Retrieve the source and destination topics
      \-------------------------------------------------------------------------------------------------------------------------*/
      var topic = TopicRepository.Load().GetTopic(topicId);
      var target = TopicRepository.Load().GetTopic(targetTopicId);

      /*--------------------------------------------------------------------------------------------------------------------------
      | Reset the source topic's Parent
      \-------------------------------------------------------------------------------------------------------------------------*/
      topic.Parent = target;

      /*--------------------------------------------------------------------------------------------------------------------------
      | Move the topic and/or reorder it with its siblings; lock the Topic repository prior to execution
      \-------------------------------------------------------------------------------------------------------------------------*/
      lock (TopicRepository) {
        if (siblingId > 0) {
          var sibling = TopicRepository.Load().GetTopic(siblingId);
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

  } //Class

} //Namespace