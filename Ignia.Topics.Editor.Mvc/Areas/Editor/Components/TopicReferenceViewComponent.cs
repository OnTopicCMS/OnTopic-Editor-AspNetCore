/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components.Options;
using Ignia.Topics.Editor.Models.Components.ViewModels;
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOPIC REFERENCE (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic reference attribute type.
  /// </summary>
  public class TopicReferenceViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository                = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicReferenceViewComponent"/> with necessary dependencies.
    /// </summary>
    public TopicReferenceViewComponent(
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
    ///   Assembles the view model for the <see cref="NestedTopicListViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      AttributeDescriptorTopicViewModel attribute,
      string htmlFieldPrefix,
      TopicReferenceOptions options = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      options                   ??= new TopicReferenceOptions();
      options.Scope             ??= attribute.GetConfigurationValue(            "Scope",                "Root");
      options.ResultLimit       ??= attribute.GetIntegerConfigurationValue(     "ResultLimit",          100);
      options.ContentType       ??= attribute.GetConfigurationValue(            "ContentType",          null);

      if (options.ContentType is null) {
        options.ContentType = CurrentTopic.ContentType;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<TopicReferenceOptions>(attribute, options);

      GetAttributeViewModel(viewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace