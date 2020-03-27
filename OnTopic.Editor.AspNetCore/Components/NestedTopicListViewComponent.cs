﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Components.ViewModels;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Components {

  /*============================================================================================================================
  | CLASS: NESTED TOPIC LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic list attribute type.
  /// </summary>
  public class NestedTopicListViewComponent : ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="NestedTopicListViewComponent"/> with necessary dependencies.
    /// </summary>
    public NestedTopicListViewComponent(ITopicRepository topicRepository) : base() {
      _topicRepository = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="NestedTopicListViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      NestedTopicListAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.ContentTypes    ??= attribute.GetConfigurationValue(            "ContentTypes",         "");
      attribute.EnableModal     ??= attribute.GetBooleanConfigurationValue(     "TargetPopup",          true);

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new NestedTopicListAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (HttpContext.Request.Query.TryGetValue("IsNew", out var action)) {
        viewModel.IsNew = action.FirstOrDefault().Equals("true", StringComparison.InvariantCultureIgnoreCase);
      }
      viewModel.UniqueKey = currentTopic.UniqueKey;
      viewModel.WebPath   = currentTopic.WebPath;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish nested topic container, if needed
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!viewModel.IsNew) {
        var topic = _topicRepository.Load(viewModel.UniqueKey);
        if (!topic.Children.Contains(attribute.Key)) {
          var topicContainer = TopicFactory.Create(attribute.Key, "List", topic);
          _topicRepository.Save(topicContainer);
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace