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
    | [GET] INDEX
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Present an editor view bound to a specific topic.
    /// </summary>
    public ActionResult Index() {
      return View();
    }

    [HttpPost]
    public ActionResult Index(EditorBindingModel) {
      return Index();
    }

  } //Class

} //Namespace