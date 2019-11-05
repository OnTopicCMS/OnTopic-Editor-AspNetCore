/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components;
using Ignia.Topics.Editor.Models.Components.ViewModels;
using Ignia.Topics.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TEXT AREA (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a text area attribute type.
  /// </summary>
  public class TextAreaViewComponent: AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TextAreaViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A <see cref="TextAreaViewComponent"/>.</returns>
    public TextAreaViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      EditingTopicViewModel currentTopic,
      TextAreaAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<TextAreaAttributeTopicViewModel>(currentTopic, attribute);

      GetAttributeViewModel(viewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.Columns         ??= attribute.GetIntegerConfigurationValue(     "Columns",              70);
      attribute.Rows            ??= attribute.GetIntegerConfigurationValue(     "Rows",                 5);
      attribute.MaximumLength   ??= attribute.GetIntegerConfigurationValue(     "MaximumLength",        Int32.MaxValue);
      attribute.CssClass        ??= attribute.GetConfigurationValue(            "CssClass",             "FormField Field");

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class

} // Namespace