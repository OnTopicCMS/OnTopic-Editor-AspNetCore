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
      | CONSTRUCT VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      EditorViewModel editorViewModel;
      var contentTypes = TopicRepository.GetContentTypes();
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
      foreach (var attribute in GetContentType(contentType).SupportedAttributes.Values) {

        //Handle hidden attributes
        if (attribute.IsHidden) {
          continue;
        }

        //Get reference to current instance
        EditorAttribute attributeValue = model.Attributes[attribute.Key];

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
      List<string> relatedTopics = attributeValue.Value.Split(',').ToList();
      topic.Relationships.ClearTopics(attribute.Key);
      foreach (string topicIdString in relatedTopics) {
        Topic relatedTopic = null;
        bool isTopicId = Int32.TryParse(topicIdString, out int topicIdInt);
        if (isTopicId && topicIdInt > 0) {
          relatedTopic = TopicRepository.Load().GetTopic(topicIdInt);
        }
        if (relatedTopic != null) {
          topic.Relationships.SetTopic(attribute.Key, relatedTopic);
        }
      }
    }

  } //Class

} //Namespace