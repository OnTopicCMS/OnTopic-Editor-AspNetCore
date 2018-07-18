/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topic Editor
\=============================================================================================================================*/
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ignia.Topics.Editor.Mvc.Controllers;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Ignia.Topics.Web;
using Ignia.Topics.Web.Mvc;
using Ignia.Topics.Web.Mvc.Controllers;

namespace Ignia.Topics.Editor.Mvc {

  /*============================================================================================================================
  | CLASS: EDITOR CONTROLLER FACTORY
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Acts as the Composition Root for dependency injection.
  /// </summary>
  class EditorControllerFactory : DefaultControllerFactory {

    /*==========================================================================================================================
    | PRIVATE INSTANCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITypeLookupService              _typeLookupService              = null;
    private readonly            ITopicMappingService            _topicMappingService            = null;
    private readonly            ITopicRepository                _topicRepository                = null;
    private readonly            Topic                           _rootTopic                      = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="IgniaControllerFactory"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    public EditorControllerFactory() : base() {
      #pragma warning disable CS0618
      _topicRepository          = TopicRepository.DataProvider;
      _topicMappingService      = new TopicMappingService(_topicRepository, _typeLookupService);
      _rootTopic                = TopicRepository.RootTopic;
      #pragma warning restore CS0618
    }

    /*==========================================================================================================================
    | GET CONTROLLER INSTANCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Overrides the factory method for creating new instances of controllers.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mvcTopicRoutingService = new MvcTopicRoutingService(
        _topicRepository,
        requestContext.HttpContext.Request.Url,
        requestContext.RouteData
      );

      // Set default controller
      if (controllerType == null) {
        controllerType = typeof(FallbackController);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/
      switch (controllerType.Name) {

        case (nameof(EditorController)):
          return new EditorController(_topicRepository, mvcTopicRoutingService.GetCurrentTopic());

        case nameof(TopicController):
          return new TopicController(_topicRepository, mvcTopicRoutingService, _topicMappingService);

        default:
          return base.GetControllerInstance(requestContext, controllerType);

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Release
      \-----------------------------------------------------------------------------------------------------------------------*/
      // There are no resources to release

    }

  } // Class

} // Namespace
