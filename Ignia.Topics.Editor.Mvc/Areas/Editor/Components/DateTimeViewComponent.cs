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
using System.Threading.Tasks;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: DATE/TIME (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a date/time attribute type.
  /// </summary>
  public class DateTimeViewComponent: AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="DateTimeViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic <see cref="NavigationTopicViewComponentBase{T}"/>.</returns>
    public DateTimeViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DateTimeViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
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

      GetAttributeViewModel(model);

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