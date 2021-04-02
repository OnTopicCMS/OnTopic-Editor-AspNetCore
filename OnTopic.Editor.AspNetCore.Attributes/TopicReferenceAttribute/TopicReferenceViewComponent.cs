/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Internal.Diagnostics;

namespace OnTopic.Editor.AspNetCore.Attributes.TopicReferenceAttribute {

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
    ///   Assembles the view model for the <see cref="TopicReferenceViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      TopicReferenceAttributeDescriptorViewModel attribute,
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
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      //### TODO 6.0.0: Remove hard-coded reference to BaseTopic; this is only needed for backward compatibility with 5.0.0.
      var isBaseTopic           = attribute.Key.Equals("BaseTopic", StringComparison.OrdinalIgnoreCase);
      if (attribute.UseCurrentContentType || isBaseTopic) {
        attribute = attribute with {
          AttributeKey          = "ContentType",
          AttributeValue        = currentTopic.ContentType,
          AutoPostBack          = isBaseTopic? true : attribute.AutoPostBack
        };
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<TopicReferenceAttributeDescriptorViewModel>(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace