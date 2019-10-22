/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Threading.Tasks;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components.Options;
using Microsoft.AspNetCore.Mvc;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: RELATIONSHIPS (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a relationships attribute type.
  /// </summary>
  public class RelationshipsViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="RelationshipsViewComponent"/> with necessary dependencies.
    /// </summary>
    public RelationshipsViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="RelationshipsViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      AttributeDescriptorTopicViewModel attribute,
      string htmlFieldPrefix,
      RelationshipsOptions options = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      options                   ??= new RelationshipsOptions();
      options.Scope             ??= attribute.GetConfigurationValue(            "Scope",                null);
      options.ShowRoot          ??= attribute.GetBooleanConfigurationValue(     "ShowRoot",             false);
      options.CheckAscendants   ??= attribute.GetBooleanConfigurationValue(     "CheckAscendants",      false);
      options.AttributeName     ??= attribute.GetConfigurationValue(            "AttributeName",        null);
      options.AttributeValue    ??= attribute.GetConfigurationValue(            "AttributeValue",       null);
      options.Namespace         ??= attribute.GetConfigurationValue(            "Namespace",            "Related");

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new AttributeViewModel<RelationshipsOptions>(attribute, options);

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