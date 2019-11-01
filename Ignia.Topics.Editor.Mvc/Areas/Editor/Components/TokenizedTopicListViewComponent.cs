/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components.ViewModels;
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOKENIZED TOPIC LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a tokenized topic list attribute type.
  /// </summary>
  public class TokenizedTopicListViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository                = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TokenizedTopicListViewComponent"/> with necessary dependencies.
    /// </summary>
    public TokenizedTopicListViewComponent(
      ITopicRoutingService topicRoutingService,
      ITopicRepository topicRepository
    ) : base(
      topicRoutingService
    ) {
      _topicRepository = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="TokenizedTopicListViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      TokenizedTopicListAttributeTopicViewModel attribute,
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
      attribute.AttributeKey    ??= attribute.GetConfigurationValue(            "AttributeName",        null);
      attribute.AttributeValue  ??= attribute.GetConfigurationValue(            "AttributeValue",       null);
      attribute.ResultLimit     ??= attribute.GetIntegerConfigurationValue(     "ResultLimit",          100);
      attribute.TokenLimit      ??= attribute.GetIntegerConfigurationValue(     "TokenLimit",           100);
      attribute.AsRelationship  ??= attribute.GetBooleanConfigurationValue(     "AsRelationship",       false);
      attribute.AutoPostBack    ??= attribute.GetBooleanConfigurationValue(     "IsAutoPostBack",       false);

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new TokenizedTopicListAttributeViewModel(attribute);

      GetAttributeViewModel(model);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      model.SelectedTopics = GetSelectedTopics(model.Value);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

    /*==========================================================================================================================
    | SELECTED TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a JSON formatted version of the attribute value. For use with TokenInput's <c>prePopulate</c> setting.
    /// </summary>
    public string GetSelectedTopics(string value) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Split value into Topic.Ids; retrieve JSON for each topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      string selectedTopics = "[";
      if (!String.IsNullOrEmpty(value)) {
          string[] topicValues = value.Split(',');
          foreach (string topicId in topicValues) {
            selectedTopics += GetTopicJson(topicId);
          }
        }

        selectedTopics += "]";
        selectedTopics = selectedTopics.Replace(",]", "]");

        return selectedTopics;
      }

    /*==========================================================================================================================
    | GET TOPIC JSON
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a JSON-formatted object (e.g., <c>{ "id": "123", "key": "Key", … }</c>) for use with TokenInput's
    ///   <c>prePopulate</c> setting, given the provided <see cref="Topic.Id"/>.
    /// </summary>
    private string GetTopicJson(string topicId) {

    /*----------------------------------------------------------------------------------------------------------------------------
    | Set initial variables
    \---------------------------------------------------------------------------------------------------------------------------*/
      string      topicJson       = "";
      Topic       topic           = _topicRepository.Load(Int32.Parse(topicId));

    /*----------------------------------------------------------------------------------------------------------------------------
    | Write out JSON for existing topic, if available
    \---------------------------------------------------------------------------------------------------------------------------*/
      if (topic != null) {
        topicJson               += $"{{"
          + $"'id'              : '{topic.Id}', "
          + $"'key'             : '{EncodeJsonValue(topic.Key)}, "
          + $"'text'            : '{EncodeJsonValue(topic.Title)}', "
          + $"'path'            : '{EncodeJsonValue(topic.GetUniqueKey())}', "
          + $"'webPath'         : '{EncodeJsonValue(topic.GetWebPath())} "
          + $"}},";
      }

    /*----------------------------------------------------------------------------------------------------------------------------
    | Return JSON
    \---------------------------------------------------------------------------------------------------------------------------*/
      return topicJson;

    }

    /*==========================================================================================================================
    | ENCODE JSON VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Encodes a JSON value by escaping any quotes.
    /// </summary>
    private string EncodeJsonValue(string value) => value.Replace("'", "\\'");


} // Class
} // Namespace