/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Internal.Diagnostics;

namespace OnTopic.Editor.AspNetCore.Attributes.LastModifiedByAttribute {

  /*============================================================================================================================
  | CLASS: LAST MODIFIED BY (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a last modified by attribute type.
  /// </summary>
  public class LastModifiedByViewComponent : ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="LastModifiedByViewComponent"/> with necessary dependencies.
    /// </summary>
    public LastModifiedByViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="LastModifiedByViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      LastModifiedByAttributeDescriptorViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));
      Contract.Requires(attribute, nameof(attribute));
      Contract.Requires(attribute.Key, nameof(attribute.Key));

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      currentTopic.Attributes.TryGetValue(attribute.Key, out var value);

      var model                 = new LastModifiedByAttributeViewModel(currentTopic, attribute) {
        CurrentValue            = value,
        Value                   = HttpContext.User.Identity.Name?? "System"
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

  } // Class
} // Namespace