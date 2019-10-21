﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Threading.Tasks;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components.Options;
using Microsoft.AspNetCore.Mvc;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOPIC LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic list attribute type.
  /// </summary>
  public class TopicListViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicListViewComponent"/> with necessary dependencies.
    /// </summary>
    public TopicListViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="TopicListViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      AttributeDescriptorTopicViewModel attribute,
      string htmlFieldPrefix,
      DefaultOptions options
    ) {
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
      options ??= new DefaultOptions();
      var viewModel = new AttributeViewModel<DefaultOptions>(attribute, options);
      return View(GetAttributeViewModel(viewModel));
    }

  } // Class
} // Namespace