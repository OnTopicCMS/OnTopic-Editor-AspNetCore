﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Editor.Models.Queryable;
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
        new SelectListItem {
          Value = "",
          Text = attribute.DefaultLabel
        }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Set default value
      \-----------------------------------------------------------------------------------------------------------------------*/
      var defaultValue = currentTopic.Attributes.ContainsKey(attribute.Key)? currentTopic.Attributes[attribute.Key] : null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Get values
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topics = (List<QueryResultTopicViewModel>)null;

      if (attribute.RelativeTopicBase != null) {
        var baseTopic             = _topicRepository.Load(currentTopic.UniqueKey);
        var rootTopic             = attribute.RelativeTopicBase switch {
          "CurrentTopic"          => baseTopic,
          "ParentTopic"           => baseTopic.Parent,
          "GrandparentTopic"      => (Topic)baseTopic.Parent?.Parent,
          "ContentTypeDescriptor" => (Topic)_topicRepository.GetContentTypeDescriptors().FirstOrDefault(t => t.Key.Equals(baseTopic.ContentType)),
          _ => baseTopic
        };
        topics = GetTopics(
          rootTopic,
          attribute.AttributeKey,
          attribute.AttributeValue,
          allowedKeys
        );
      }
      else {
        topics = GetTopics(
          _topicRepository.Load(attribute.RootTopic?.UniqueKey?? attribute.RootTopicKey),
          attribute.AttributeKey,
          attribute.AttributeValue,
          allowedKeys
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get values from repository
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var topic in topics) {

        var title = viewModel.TopicList.Any(t => t.Text == topic.Title)? $"{topic.Title} ({topic.Key})" : topic.Title;
        var value = getValue(topic);

        viewModel.TopicList.Add(
          new SelectListItem {
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
    public static List<QueryResultTopicViewModel> GetTopics(
      Topic  topic              = null,
      string attributeKey       = null,
      string attributeValue     = null,
      string allowedKeys        = ""
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Swallow missing topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (topic == null) {
        return new List<QueryResultTopicViewModel>();
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
      if (topic != null && !String.IsNullOrEmpty(source)) {
        source = source
          .Replace("{TopicId}", topic.Id.ToString(), StringComparison.InvariantCultureIgnoreCase)
          .Replace("{Key}", topic.Key, StringComparison.InvariantCultureIgnoreCase)
          .Replace("{UniqueKey}", topic.UniqueKey, StringComparison.InvariantCultureIgnoreCase)
          .Replace("{WebPath}", topic.WebPath, StringComparison.InvariantCultureIgnoreCase)
          .Replace("{Title}", topic.Title, StringComparison.InvariantCultureIgnoreCase);
      }
      return source;
    }

  } // Class
} // Namespace