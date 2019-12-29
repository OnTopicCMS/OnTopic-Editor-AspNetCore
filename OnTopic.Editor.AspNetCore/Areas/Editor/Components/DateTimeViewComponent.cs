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
using System.Threading.Tasks;

namespace OnTopic.Editor.AspNetCore.Components {

  /*============================================================================================================================
  | CLASS: DATE/TIME (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a date/time attribute type.
  /// </summary>
  public class DateTimeViewComponent: ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="DateTimeViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic <see cref="NavigationTopicViewComponentBase{T}"/>.</returns>
    public DateTimeViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DateTimeViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      DateTimeAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new DateTimeAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.IncludeDatePicker       ??= attribute.GetBooleanConfigurationValue("IncludeDatePicker", true);
      attribute.IncludeTimePicker       ??= attribute.GetBooleanConfigurationValue("IncludeTimePicker", true);
      attribute.DateFormat              ??= attribute.GetConfigurationValue("DateFormat", "yy-mm-dd");
      attribute.TimeFormat              ??= attribute.GetConfigurationValue("TimeFormat", "hh:mm tt");
      attribute.DateTimeSeparator       ??= attribute.GetConfigurationValue("DateTimeSeparator", " ");
      attribute.DateTimeOffset          ??= attribute.GetIntegerConfigurationValue("DateTimeOffset", 0);
      attribute.DateTimeOffsetUnits     ??= attribute.GetConfigurationValue("DateTimeOffsetUnits", "Days");

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

  } // Class
} // Namespace