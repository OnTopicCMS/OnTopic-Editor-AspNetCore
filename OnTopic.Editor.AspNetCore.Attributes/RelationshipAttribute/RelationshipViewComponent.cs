﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.RelationshipAttribute {

  /*============================================================================================================================
  | CLASS: RELATIONSHIP (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a relationship attribute type.
  /// </summary>
  public class RelationshipViewComponent : ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="RelationshipViewComponent"/> with necessary dependencies.
    /// </summary>
    public RelationshipViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="RelationshipViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      RelationshipAttributeDescriptorViewModel attribute,
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

      var model = new AttributeViewModel<RelationshipAttributeDescriptorViewModel>(currentTopic, attribute) {
        Value                   = CleanArray(value)
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

    /*==========================================================================================================================
    | METHOD: CLEAN ARRAY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Takes a string array, converts it to an array, strips any blank entries, and returns it to a string array. Useful for
    ///   dealing with potential artifacts such as empty array items introduced by JavaScript.
    /// </summary>
    private static string CleanArray(string? value) {
      if (String.IsNullOrWhiteSpace(value)) return "";
      return String.Join(",", value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
    }

  } // Class
} // Namespace