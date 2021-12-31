﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Globalization;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Attributes.TokenizedTopicListAttribute {

  /*============================================================================================================================
  | CLASS: TOKENIZED TOPIC LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a tokenized topic list attribute type.
  /// </summary>
  public class TokenizedTopicListViewComponent : ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TokenizedTopicListViewComponent"/> with necessary dependencies.
    /// </summary>
    public TokenizedTopicListViewComponent(ITopicRepository topicRepository) : base() {
      _topicRepository = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="TokenizedTopicListViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      TokenizedTopicListAttributeDescriptorViewModel attribute,
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
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new TokenizedTopicListAttributeViewModel(currentTopic, attribute);

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
    public string GetSelectedTopics(string? value) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Split value into Topic.Ids; retrieve JSON for each topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var selectedTopics = "[";
      if (!String.IsNullOrEmpty(value)) {
          var topicValues = value.Split(',');
          foreach (var topicId in topicValues) {
            selectedTopics += GetTopicJson(topicId);
          }
        }

        selectedTopics += "]";
        selectedTopics = selectedTopics.Replace(",]", "]", StringComparison.Ordinal);

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

    /*--------------------------------------------------------------------------------------------------------------------------
    | Set initial variables
    \-------------------------------------------------------------------------------------------------------------------------*/
      var topicJson               = "";
      var topic                   = _topicRepository.Load(Int32.Parse(topicId, CultureInfo.InvariantCulture));

    /*--------------------------------------------------------------------------------------------------------------------------
    | Write out JSON for existing topic, if available
    \-------------------------------------------------------------------------------------------------------------------------*/
      if (topic is not null) {
        topicJson               += $"{{"
          + $"'id'              : '{topic.Id}', "
          + $"'key'             : '{EncodeJsonValue(topic.Key)}', "
          + $"'text'            : '{EncodeJsonValue(topic.Title)}', "
          + $"'path'            : '{EncodeJsonValue(topic.GetUniqueKey())}', "
          + $"'webPath'         : '{EncodeJsonValue(topic.GetWebPath())}' "
          + $"}},";
      }

    /*--------------------------------------------------------------------------------------------------------------------------
    | Return JSON
    \-------------------------------------------------------------------------------------------------------------------------*/
      return topicJson;

    }

    /*==========================================================================================================================
    | ENCODE JSON VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Encodes a JSON value by escaping any quotes.
    /// </summary>
    private static string EncodeJsonValue(string value) => value.Replace("'", "\\'", StringComparison.Ordinal);

  } // Class
} // Namespace