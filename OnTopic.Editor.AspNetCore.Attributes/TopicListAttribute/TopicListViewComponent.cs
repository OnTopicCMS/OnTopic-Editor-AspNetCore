﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using OnTopic.Attributes;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.AspNetCore.Models.Queryable;
using OnTopic.Internal.Diagnostics;
using OnTopic.Querying;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Attributes.TopicListAttribute {

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
      TopicListAttributeDescriptorViewModel attribute,
      string? htmlFieldPrefix = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));
      Contract.Requires(attribute, nameof(attribute));
      Contract.Requires(currentTopic.ContentType, nameof(currentTopic.ContentType));
      Contract.Requires(attribute.Key, nameof(attribute.Key));

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new TopicListAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set default value
      \-----------------------------------------------------------------------------------------------------------------------*/
      var defaultValue = currentTopic.Attributes.ContainsKey(attribute.Key)? currentTopic.Attributes[attribute.Key] : null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Get root topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      var rootTopic             = (Topic?)null;

      if (attribute.RelativeTopicBase is not null) {
        var baseTopic             = _topicRepository.Load(currentTopic.UniqueKey);
        Contract.Assume(baseTopic, $"The topic with the key '{currentTopic.UniqueKey}' could not be located.");
        if (String.IsNullOrEmpty(currentTopic.Key)) {
          baseTopic               = TopicFactory.Create("NewTopic", currentTopic.ContentType, baseTopic);
          baseTopic.Parent?.Children.Remove(baseTopic);
        }
        rootTopic                 = attribute.RelativeTopicBase switch {
          "CurrentTopic"          => baseTopic,
          "ParentTopic"           => baseTopic.Parent,
          "GrandparentTopic"      => (Topic?)baseTopic.Parent?.Parent,
          "ContentTypeDescriptor" => (Topic?)_topicRepository.GetContentTypeDescriptors().FirstOrDefault(t => t.Key.Equals(baseTopic.ContentType, StringComparison.Ordinal)),
          _ => baseTopic
        };
      }
      else if (attribute.RootTopic is not null) {
        rootTopic = _topicRepository.Load(attribute.RootTopic.UniqueKey);
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
        attribute.AttributeValue
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Set label
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrEmpty(viewModel.InheritedValue)) {
        setLabel(viewModel.InheritedValue, "inherited value");
      }
      else if (!String.IsNullOrEmpty(viewModel.AttributeDescriptor.DefaultValue)) {
        setLabel(viewModel.AttributeDescriptor.DefaultValue, "default value");
      }
      else if (!String.IsNullOrEmpty(viewModel.AttributeDescriptor.ImplicitValue)) {
        setLabel(viewModel.AttributeDescriptor.ImplicitValue, "implicit default");
      }
      else {
        setLabel(attribute.DefaultLabel?? "Select an option…");
      }

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
      | Function: Get Value
      \-----------------------------------------------------------------------------------------------------------------------*/
      string getValue(QueryResultTopicViewModel topic) => ReplaceTokens(topic, "{" + attribute.ValueProperty + "}");

      /*------------------------------------------------------------------------------------------------------------------------
      | Function: Set Label
      \-----------------------------------------------------------------------------------------------------------------------*/
      void setLabel(string value, string? contextualLabel = null) {
        var inheritedTopic = topics.Where(t => t.Key == value).FirstOrDefault();
        var label = inheritedTopic?.Title ?? value;
        if (contextualLabel is not null) {
          label += " (" + contextualLabel + ")";
        }
        viewModel?.TopicList.Add(
          new() {
            Value = "",
            Text = label
          }
        );
      }

    }

    /*==========================================================================================================================
    | METHOD: GET TOPICS
    >---------------------------------------------------------------------------------------------------------------------------
    | Retrieves a collection of topics with optional control call filter properties Scope, AttributeName and AttributeValue.
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a list of <see cref="QueryResultTopicViewModel"/>s that represent topics under the <paramref name="topic"/>
    ///   which have an <see cref="AttributeRecord"/> with <paramref name="attributeKey"/> and <paramref name="attributeValue"
    ///   />.
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="attributeKey"></param>
    /// <param name="attributeValue"></param>
    /// <returns></returns>
    private static Collection<QueryResultTopicViewModel> GetTopics(
      Topic?  topic             = null,
      string? attributeKey      = null,
      string? attributeValue    = null
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
        ResultLimit             = 250,
        ShowRoot                = false
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Get query results
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicQueryService     = new TopicQueryService();
      var topicQueryResults     = topicQueryService.Query(topic, options);

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