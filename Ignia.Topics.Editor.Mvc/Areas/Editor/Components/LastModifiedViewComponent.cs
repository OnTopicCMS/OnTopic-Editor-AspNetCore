﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Threading.Tasks;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components.ViewModels;
using Ignia.Topics.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: LAST MODIFIED (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a last modified attribute type.
  /// </summary>
  public class LastModifiedViewComponent: ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="LastModifiedViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic <see cref="NavigationTopicViewComponentBase{T}"/>.</returns>
    public LastModifiedViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="LastModifiedViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      LastModifiedAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new LastModifiedAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      model.Value = DateTime.Now.ToString();
      if (currentTopic.LastModified != null && currentTopic.LastModified != DateTime.MinValue) {
        model.CurrentValue = currentTopic.LastModified.ToString();
      }
      else {
        model.CurrentValue = model.Value;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

  } // Class
} // Namespace