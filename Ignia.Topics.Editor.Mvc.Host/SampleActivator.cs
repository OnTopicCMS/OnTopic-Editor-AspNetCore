/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Sample OnTopic Site
\=============================================================================================================================*/
using System;
using Ignia.Topics;
using Ignia.Topics.AspNetCore.Mvc;
using Ignia.Topics.AspNetCore.Mvc.Components;
using Ignia.Topics.Data.Caching;
using Ignia.Topics.Data.Sql;
using Ignia.Topics.Editor.Mvc.Components;
using Ignia.Topics.Editor.Mvc.Controllers;
using Ignia.Topics.Editor.Mvc.Infrastructure;
using Ignia.Topics.Mapping;
using Ignia.Topics.Repositories;
using Ignia.Topics.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace OnTopicTest {

  /*============================================================================================================================
  | CLASS: SAMPLE ACTIVATOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Responsible for creating instances of factories in response to web requests. Represents the Composition Root for
  ///   Dependency Injection.
  /// </summary>
  public class SampleActivator : IControllerActivator, IViewComponentActivator {

    /*==========================================================================================================================
    | PRIVATE INSTANCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            string                          _connectionString               = null;
    private readonly            ITypeLookupService              _typeLookupService              = null;
    private readonly            ITopicMappingService            _topicMappingService            = null;
    private readonly            ITopicRepository                _topicRepository                = null;
    private readonly            IWebHostEnvironment             _webHostEnvironment             = null;
    private readonly            Topic                           _rootTopic                      = null;

    /*==========================================================================================================================
    | HIERARCHICAL TOPIC MAPPING SERVICE
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly IHierarchicalTopicMappingService<NavigationTopicViewModel> _hierarchicalMappingService = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="SampleControllerFactory"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    /// <remarks>
    ///   The constructor is responsible for establishing dependencies with the singleton lifestyle so that they are available
    ///   to all requests.
    /// </remarks>
    public SampleActivator(string connectionString, IWebHostEnvironment webHostEnvironment) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Initialize Topic Repository
      \-----------------------------------------------------------------------------------------------------------------------*/
                                _connectionString               = connectionString;
                                _webHostEnvironment             = webHostEnvironment;
      var                       sqlTopicRepository              = new SqlTopicRepository(connectionString);
      var                       cachedTopicRepository           = new CachedTopicRepository(sqlTopicRepository);
      var                       topicViewModel                  = new PageTopicViewModel();

      /*------------------------------------------------------------------------------------------------------------------------
      | Preload repository
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository                                          = cachedTopicRepository;
      _typeLookupService                                        = new EditorViewModelLookupService();
      _topicMappingService                                      = new TopicMappingService(_topicRepository, _typeLookupService);
      _rootTopic                                                = _topicRepository.Load();

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish hierarchical topic mapping service
      \-----------------------------------------------------------------------------------------------------------------------*/
      _hierarchicalMappingService = new CachedHierarchicalTopicMappingService<NavigationTopicViewModel>(
        new HierarchicalTopicMappingService<NavigationTopicViewModel>(
          _topicRepository,
          _topicMappingService
        )
      );

    }

    /*==========================================================================================================================
    | METHOD: CREATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Registers dependencies, and injects them into new instances of controllers in response to each request.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    public object Create(ControllerContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine controller type
      \-----------------------------------------------------------------------------------------------------------------------*/
      Type type = context.ActionDescriptor.ControllerTypeInfo.AsType();

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure and return appropriate controller
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (type == typeof(EditorController)) {
        return CreateEditorController(context);
      }
      else {
        throw new Exception($"Unknown controller {type.Name}");
      }

    }

    /// <summary>
    ///   Registers dependencies, and injects them into new instances of view components in response to each request.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    public object Create(ViewComponentContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine view component type
      \-----------------------------------------------------------------------------------------------------------------------*/
      Type type = context.ViewComponentDescriptor.TypeInfo.AsType();

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure and return appropriate view component
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (type == typeof(LastModifiedViewComponent)) {
        return new LastModifiedViewComponent();
      }
      if (type == typeof(BooleanViewComponent)) {
        return new BooleanViewComponent();
      }
      if (type == typeof(DateTimeViewComponent)) {
        return new DateTimeViewComponent();
      }
      if (type == typeof(DisplayOptionsViewComponent)) {
        return new DisplayOptionsViewComponent();
      }
      if (type == typeof(FileListViewComponent)) {
        return new FileListViewComponent(_webHostEnvironment);
      }
      if (type == typeof(FilePathViewComponent)) {
        return new FilePathViewComponent(
          new MvcTopicRoutingService(
            _topicRepository,
            new Uri($"https://{context.ViewContext.HttpContext.Request.Host}/{context.ViewContext.HttpContext.Request.Path}"),
            context.ViewContext.RouteData
          )
        );
      }
      if (type == typeof(HtmlViewComponent)) {
        return new HtmlViewComponent();
      }
      if (type == typeof(LastModifiedViewComponent)) {
        return new LastModifiedViewComponent();
      }
      if (type == typeof(LastModifiedByViewComponent)) {
        return new LastModifiedByViewComponent();
      }
      if (type == typeof(NestedTopicListViewComponent)) {
        return new NestedTopicListViewComponent();
      }
      if (type == typeof(NumberViewComponent)) {
        return new NumberViewComponent();
      }
      if (type == typeof(RelationshipViewComponent)) {
        return new RelationshipViewComponent();
      }
      if (type == typeof(TextViewComponent)) {
        return new TextViewComponent();
      }
      if (type == typeof(TextAreaViewComponent)) {
        return new TextAreaViewComponent();
      }
      if (type == typeof(TokenizedTopicListViewComponent)) {
        return new TokenizedTopicListViewComponent(_topicRepository);
      }
      if (type == typeof(TopicListViewComponent)) {
        return new TopicListViewComponent(_topicRepository);
      }
      if (type == typeof(TopicReferenceViewComponent)) {
        return new TopicReferenceViewComponent(_topicRepository);
      }
      else {
        throw new Exception($"Unknown view component {type.Name}");
      }

    }

    /*==========================================================================================================================
    | METHOD: CREATE EDITOR CONTROLLER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Responds to a request to create a <see cref="EditorController"/> instance, the default controller for the editor area.
    /// </summary>
    private EditorController CreateEditorController(ControllerContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mvcTopicRoutingService = new MvcTopicRoutingService(
        _topicRepository,
        new Uri($"https://{context.HttpContext.Request.Host}/{context.HttpContext.Request.Path}"),
        context.RouteData
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Return EditorController
      \-----------------------------------------------------------------------------------------------------------------------*/
      return new EditorController(_topicRepository, mvcTopicRoutingService, _topicMappingService);

    }

    /*==========================================================================================================================
    | METHOD: RELEASE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Responds to a request to release resources associated with a particular controller.
    /// </summary>
    public void Release(ControllerContext context, object controller) { }

    /// <summary>
    ///   Responds to a request to release resources associated with a particular view component.
    /// </summary>
    public void Release(ViewComponentContext context, object viewComponent) { }

  } //Class
} //Namespace