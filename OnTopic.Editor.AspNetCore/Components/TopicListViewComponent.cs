/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Editor.Models.Queryable;
using OnTopic.Internal.Diagnostics;
using OnTopic.Querying;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Components {

  /*============================================================================================================================
  | CLASS: TOPIC LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic list attribute type.
  /// </summary>
  public class TopicListViewComponent : ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicListViewComponent"/> with necessary dependencies.
    /// </summary>
    public TopicListViewComponent(ITopicRepository topicRepository): base() {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="TopicListViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      TopicListAttributeTopicViewModel attribute,
      string htmlFieldPrefix = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));
      Contract.Requires(attribute, nameof(attribute));

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.DefaultLabel    ??= attribute.GetConfigurationValue(            "Label",                "Select a Topic…");
      attribute.RootTopicKey    ??= attribute.GetConfigurationValue(            "Scope",                null);
      attribute.AttributeKey    ??= attribute.GetConfigurationValue(            "AttributeName",        null);
      attribute.AttributeValue  ??= attribute.GetConfigurationValue(            "AttributeValue",       null);
      attribute.ValueProperty   ??= attribute.GetConfigurationValue(            "ValueProperty",        "Key");
      var       allowedKeys       = attribute.GetConfigurationValue(            "AllowedKeys",          null);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new TopicListAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set label
      \-----------------------------------------------------------------------------------------------------------------------*/
      viewModel.TopicList.Add(
        new() {
          Value = "",
          Text = attribute.DefaultLabel
        }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Set default value
      \-----------------------------------------------------------------------------------------------------------------------*/
      var defaultValue = currentTopic.Attributes.ContainsKey(attribute.Key)? currentTopic.Attributes[attribute.Key] : null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Get root topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var rootTopic             = (Topic)null;

      if (attribute.RelativeTopicBase is not null) {
        var baseTopic             = _topicRepository.Load(currentTopic.UniqueKey);
        if (String.IsNullOrEmpty(currentTopic.Key)) {
          baseTopic               = TopicFactory.Create("NewTopic", currentTopic.ContentType, baseTopic);
          baseTopic.Parent.Children.Remove(baseTopic);
        }
        rootTopic                 = attribute.RelativeTopicBase switch {
          "CurrentTopic"          => baseTopic,
          "ParentTopic"           => baseTopic.Parent,
          "GrandparentTopic"      => (Topic)baseTopic.Parent?.Parent,
          "ContentTypeDescriptor" => (Topic)_topicRepository.GetContentTypeDescriptors().FirstOrDefault(t => t.Key.Equals(baseTopic.ContentType, StringComparison.Ordinal)),
          _ => baseTopic
        };
      }
      else {
        rootTopic = _topicRepository.Load(attribute.RootTopic?.UniqueKey?? attribute.RootTopicKey);
      }

      if (rootTopic is not null && !String.IsNullOrEmpty(attribute.RelativeTopicPath)) {
        rootTopic = rootTopic.GetByUniqueKey(rootTopic.GetUniqueKey() + ":" + attribute.RelativeTopicPath);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get values
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topics = GetTopics(
        rootTopic,
        attribute.AttributeKey,
        attribute.AttributeValue,
        allowedKeys
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Get values from repository
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var topic in topics) {

        var title = viewModel.TopicList.Any(t => t.Text == topic.Title)? $"{topic.Title} ({topic.Key})" : topic.Title;
        var value = getValue(topic);

        viewModel.TopicList.Add(
          new() {
            Value = value,
            Text = title,
            Selected = value == defaultValue
          }
        );

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Helper functions
      \-----------------------------------------------------------------------------------------------------------------------*/
      string getValue(QueryResultTopicViewModel topic) => ReplaceTokens(topic, "{" + attribute.ValueProperty + "}");

    }

    /*==========================================================================================================================
    | METHOD: GET TOPICS
    >---------------------------------------------------------------------------------------------------------------------------
    | Retrieves a collection of topics with optional control call filter properties Scope, AttributeName and AttributeValue.
    \-------------------------------------------------------------------------------------------------------------------------*/
    public static Collection<QueryResultTopicViewModel> GetTopics(
      Topic  topic              = null,
      string attributeKey       = null,
      string attributeValue     = null,
      string allowedKeys        = ""
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Swallow missing topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (topic is null) {
        return new();
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish query options
      \-----------------------------------------------------------------------------------------------------------------------*/
      var options               = new TopicQueryOptions() {
        AttributeName           = attributeKey,
        AttributeValue          = attributeValue,
        FlattenStructure        = false,
        IsRecursive             = false,
        ResultLimit             = 100,
        ShowRoot                = false
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Get query results
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicQueryService     = new TopicQueryService();
      var topicQueryResults     = topicQueryService.Query(topic, options);

      /*------------------------------------------------------------------------------------------------------------------------
      | Filter Topics selection list based on Content Types
      \-----------------------------------------------------------------------------------------------------------------------*/
      string[] allowedKeyList;
      if (!String.IsNullOrEmpty(allowedKeys)) {
        allowedKeyList = allowedKeys.Split(',');
        for (var i = 0; i < topicQueryResults.Count; i++) {
          var childTopic = topicQueryResults[i];
          if (Array.IndexOf(allowedKeyList, childTopic.Key) < 0) {
            topicQueryResults.RemoveAt(i);
            i--;
          }
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return Topics list
      \-----------------------------------------------------------------------------------------------------------------------*/
      return topicQueryResults;

    }

    /*==========================================================================================================================
    | REPLACE TOKENS
    >---------------------------------------------------------------------------------------------------------------------------
    | Replaces tokenized parameters (e.g., {Key}) in the source string based on the source Topic's properties.
    \-------------------------------------------------------------------------------------------------------------------------*/
    private static string ReplaceTokens(QueryResultTopicViewModel topic, string source) {
      if (topic is not null && !String.IsNullOrEmpty(source)) {
        source = source
          .Replace("{TopicId}", topic.Id.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
          .Replace("{Key}", topic.Key, StringComparison.OrdinalIgnoreCase)
          .Replace("{UniqueKey}", topic.UniqueKey, StringComparison.OrdinalIgnoreCase)
          .Replace("{WebPath}", topic.WebPath, StringComparison.OrdinalIgnoreCase)
          .Replace("{Title}", topic.Title, StringComparison.OrdinalIgnoreCase);
      }
      return source;
    }

  } // Class
} // Namespace