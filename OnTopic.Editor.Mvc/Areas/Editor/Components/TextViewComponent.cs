/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Components;
using OnTopic.Editor.Models.Components.ViewModels;
using OnTopic.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OnTopic.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TEXT (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a text area attribute type.
  /// </summary>
  public class TextViewComponent: ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TextViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A <see cref="TextViewComponent"/>.</returns>
    public TextViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      TextAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<TextAttributeTopicViewModel>(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.MaximumLength   ??= attribute.GetIntegerConfigurationValue(     "MaximumLength",        Int32.MaxValue);
      attribute.CssClass        ??= attribute.GetConfigurationValue(            "CssClass",             "FormField Field");

      attribute.InputType       ??= attribute.GetBooleanConfigurationValue("ValidateEmail", false)? "email" : null;
      attribute.InputType       ??= attribute.GetBooleanConfigurationValue("ValidatePhone", false)? "tel" : null;
      attribute.InputType       ??= "text";

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class

} // Namespace