/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topic Editor
\=============================================================================================================================*/
using Ignia.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Ignia.Topics.Data.Sql;
using Ignia.Topics.Editor.Mvc.Controllers;
using System.Configuration;
using Ignia.Topics.Data.Caching;
using Ignia.Topics.Repositories;

namespace Ignia.Topics.Editor.Mvc {

  /*============================================================================================================================
  | CLASS: EDITOR CONTROLLER FACTORY
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Acts as the Composition Root for dependency injection.
  /// </summary>
  class EditorControllerFactory : DefaultControllerFactory {

    /*==========================================================================================================================
    | DEFINE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    static ITopicRepository _topicRepository = null;

    /*==========================================================================================================================
    | GET CONTROLLER INSTANCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Overrides the factory method for creating new instances of controllers.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish repository cache
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (_topicRepository == null) {
        _topicRepository = new CachedTopicRepository(
          new SqlTopicRepository(ConfigurationManager.ConnectionStrings["TopicsServer"].ConnectionString)
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/
      var rootTopic = _topicRepository.Load();
      var topicRoutingService = new TopicRoutingService(
        _topicRepository,
        requestContext.HttpContext.Request.Url,
        requestContext.RouteData
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (controllerType == typeof(EditorController)) {
        return new EditorController(_topicRepository, topicRoutingService.Topic);
      }

      return base.GetControllerInstance(requestContext, controllerType);

      /*------------------------------------------------------------------------------------------------------------------------
      | Release
      \-----------------------------------------------------------------------------------------------------------------------*/
      //There are no resources to release

    }

  } //Class
} //Namespace
