/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Threading.Tasks;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: RELATIONSHIP (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a relationship attribute type.
  /// </summary>
  public class RelationshipViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="RelationshipViewComponent"/> with necessary dependencies.
    /// </summary>
    public RelationshipViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="RelationshipViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      RelationshipAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.Scope           ??= attribute.GetConfigurationValue(            "Scope",                null);
      attribute.ShowRoot        ??= attribute.GetBooleanConfigurationValue(     "ShowRoot",             false);
      attribute.CheckAscendants ??= attribute.GetBooleanConfigurationValue(     "CheckAscendants",      false);
      attribute.AttributeName   ??= attribute.GetConfigurationValue(            "AttributeName",        null);
      attribute.AttributeValue  ??= attribute.GetConfigurationValue(            "AttributeValue",       null);
      attribute.Namespace       ??= attribute.GetConfigurationValue(            "Namespace",            "Related");

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new AttributeViewModel<RelationshipAttributeTopicViewModel>(attribute);

      GetAttributeViewModel(model);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      model.Value = CleanArray(model.Value);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

    /*==========================================================================================================================
    | METHOD: CLEAN ARRAY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Takes a string array, converts it to an array, strips any blank entries, and returns it to a string array.  Useful for
    ///   dealing with potential artifacts such as empty array items introduced by JavaScript.
    /// </summary>
    string CleanArray(string value) {
      if (String.IsNullOrWhiteSpace(value)) return "";
      return String.Join(",", value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
    }

  } // Class
} // Namespace