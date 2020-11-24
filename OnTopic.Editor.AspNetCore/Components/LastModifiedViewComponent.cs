/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Components.ViewModels;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Internal.Diagnostics;

namespace OnTopic.Editor.AspNetCore.Components {

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
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));
      Contract.Requires(attribute, nameof(attribute));

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
      if (currentTopic.LastModified != DateTime.MinValue) {
        model.CurrentValue      = currentTopic.LastModified.ToString(CultureInfo.InvariantCulture);
      }
      else {
        model.CurrentValue      = model.Value;
      }
      model.Value               = DateTime.Now.ToString(CultureInfo.InvariantCulture);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

  } // Class
} // Namespace