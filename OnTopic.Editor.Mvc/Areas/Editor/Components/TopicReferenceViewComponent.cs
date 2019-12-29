/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Components.ViewModels;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnTopic.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOPIC REFERENCE (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic reference attribute type.
  /// </summary>
  public class TopicReferenceViewComponent : ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicReferenceViewComponent"/> with necessary dependencies.
    /// </summary>
    public TopicReferenceViewComponent() : base() {
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="NestedTopicListViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      TopicReferenceAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.RootTopicKey    ??= attribute.GetConfigurationValue(            "Scope",                "Root");
      attribute.ResultLimit     ??= attribute.GetIntegerConfigurationValue(     "ResultLimit",          100);
      attribute.TargetContentType ??= attribute.GetConfigurationValue(          "ContentType",          null);

      if (String.IsNullOrWhiteSpace(attribute.TargetContentType)) {
        attribute.TargetContentType = currentTopic.ContentType;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<TopicReferenceAttributeTopicViewModel>(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace