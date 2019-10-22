/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components.Options;
using Ignia.Topics.Editor.Models.Components.ViewModels;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOPIC POINTER (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic pointer attribute type.
  /// </summary>
  public class TopicPointerViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository                = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicPointerViewComponent"/> with necessary dependencies.
    /// </summary>
    public TopicPointerViewComponent(
      ITopicRoutingService topicRoutingService,
      ITopicRepository topicRepository
    ) : base(
      topicRoutingService
    ) {
      _topicRepository = topicRepository;
    }


    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="TopicListViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      AttributeDescriptorTopicViewModel attribute,
      string htmlFieldPrefix,
      TopicPointerOptions options = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      options                   ??= new TopicPointerOptions();
      options.Scope             ??= attribute.GetConfigurationValue(            "Scope",                "Root");
      options.ResultLimit       ??= attribute.GetIntegerConfigurationValue(     "ResultLimit",          100);
      options.ContentType       ??= attribute.GetConfigurationValue(            "ContentType",          null);

      if (options.ContentType is null) {
        options.ContentType = CurrentTopic.ContentType;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<TopicPointerOptions>(attribute, options);

      GetAttributeViewModel(viewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace