/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ignia.Topics.Collections;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Editor.Mvc.Models;
using Ignia.Topics.Querying;
using Ignia.Topics.Repositories;
using Ignia.Topics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOPIC LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic list attribute type.
  /// </summary>
  public class TopicListViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository                = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicListViewComponent"/> with necessary dependencies.
    /// </summary>
    public TopicListViewComponent(
      ITopicRoutingService      topicRoutingService,
      ITopicRepository          topicRepository
    ) : base(topicRoutingService) {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="TopicListViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      TopicListAttributeTopicViewModel attribute,
      string htmlFieldPrefix = null,
      IEnumerable<TopicViewModel> values = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.Label           ??= attribute.GetConfigurationValue(            "Label",                "Select a Topic…");
      attribute.Scope           ??= attribute.GetConfigurationValue(            "Scope",                null);
      attribute.AttributeName   ??= attribute.GetConfigurationValue(            "AttributeName",        null);
      attribute.AttributeValue  ??= attribute.GetConfigurationValue(            "AttributeValue",       null);
      attribute.AllowedKeys     ??= attribute.GetConfigurationValue(            "AllowedKeys",          null);
      attribute.UseUniqueKey    ??= attribute.GetBooleanConfigurationValue(     "UseUniqueKey",         false);
      attribute.ValueProperty   ??= attribute.GetConfigurationValue(            "ValueProperty",        null);
      attribute.TargetPopup     ??= attribute.GetBooleanConfigurationValue(     "TargetPopup",          false);
      attribute.TargetUrl       ??= attribute.GetConfigurationValue(            "TargetUrl",            null);
      attribute.OnClientClose   ??= attribute.GetConfigurationValue(            "OnClientClose",        null);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new TopicListAttributeViewModel(attribute);

      GetAttributeViewModel(viewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set label
      \-----------------------------------------------------------------------------------------------------------------------*/
      viewModel.TopicList.Add(
        new SelectListItem {
          Value = null,
          Text = attribute.Label
        }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Set default value
      \-----------------------------------------------------------------------------------------------------------------------*/
      var defaultValue = CurrentTopic.Attributes.GetValue(attribute.Key, attribute.DefaultValue, false, false);

      /*------------------------------------------------------------------------------------------------------------------------
      | Get values
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topics = (TopicCollection<Topic>)null;

      //### HACK JJC20191031: Since Topic and TopicViewModel aren't intercompatible, and the remaining processing is based on
      //Topic, we're converting any preconfigured topics from TopicViewModel back to Topic by looking them up in the repository.
      //This, of course, assumes that the topic view models refer to existing topics in the repository.
      if (values != null) {
        topics = new TopicCollection<Topic>();
        foreach (var topicViewModel in values) {
          var topic = _topicRepository.Load(topicViewModel.Id);
          if (topic != null) {
            topics.Add(topic);
          }
        }
      }
      else {
        topics = GetTopics(attribute.Scope, attribute.AttributeName, attribute.AttributeValue, attribute.AllowedKeys);
      }


      /*------------------------------------------------------------------------------------------------------------------------
      | Get values from repository
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var topic in topics) {

        string title = viewModel.TopicList.Any(t => t.Text == topic.Title)? $"{topic.Title} ({topic.Key})" : topic.Title;
        string value = getValue(topic);

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
      string getValue(Topic topic) {
        if (!String.IsNullOrEmpty(attribute.TargetUrl)) {
          // Add TopicID if not already available
          var uniqueTargetUrl = attribute.TargetUrl;
          if (
            uniqueTargetUrl.IndexOf("?") >= 0 &&
            uniqueTargetUrl.IndexOf("TopicID", StringComparison.InvariantCultureIgnoreCase) < 0
          ) {
            uniqueTargetUrl     += "&TopicID=" + topic.Id.ToString();
          }
          return ReplaceTokens(topic, uniqueTargetUrl);
        }
        return ReplaceTokens(topic, "{" + attribute.ValueProperty + "}");
      }

    }

    /*==========================================================================================================================
    | METHOD: GET TOPICS
    >---------------------------------------------------------------------------------------------------------------------------
    | Retrieves a collection of topics with optional control call filter properties Scope, AttributeName and AttributeValue.
    \-------------------------------------------------------------------------------------------------------------------------*/
    public TopicCollection<Topic> GetTopics(
      string scope = null,
      string attributeName = null,
      string attributeValue = null,
      string allowedKeys = ""
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Instantiate object
      \-----------------------------------------------------------------------------------------------------------------------*/
      TopicCollection<Topic> topics = new TopicCollection<Topic>();
      Topic topic = null;

      if (scope != null) {
        topic = _topicRepository.Load(scope);
      }

      // Use RootTopic if Scope is available but does not return a topic object
      if (topic == null) {
        topic = _topicRepository.Load();
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Filter Topics selection list by AttributeName/AttributeValue
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (attributeName != null && attributeValue != null) {

        var readOnlyTopics = topic.FindAllByAttribute(attributeName, attributeValue);
        foreach (Topics.Topic readOnlyTopic in readOnlyTopics) {
          if (!topics.Contains(readOnlyTopic.Key)) {
            topics.Add(readOnlyTopic);
          }
        }

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get all Topics under RootTopic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (topics.Count == 0) {
        foreach (Topic childTopic in topic.Children) {
          if (!topics.Contains(childTopic)) {
            topics.Add(childTopic);
          }
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Filter Topics selection list based on Content Types
      \-----------------------------------------------------------------------------------------------------------------------*/
      string[] allowedKeyList = null;
      if (!String.IsNullOrEmpty(allowedKeys)) {
        allowedKeyList = allowedKeys.Split(',');
        for (int i = 0; i < topics.Count; i++) {
          Topic childTopic = topics[i];
          if (Array.IndexOf(allowedKeyList, childTopic.Key) < 0) {
            topics.RemoveAt(i);
            i--;
          }
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return Topics list
      \-----------------------------------------------------------------------------------------------------------------------*/
      return topics;

    }

    /*==========================================================================================================================
    | REPLACE TOKENS
    >---------------------------------------------------------------------------------------------------------------------------
    | Replaces tokenized parameters (e.g., {Key}) in the source string based on the source Topic's properties.
    \-------------------------------------------------------------------------------------------------------------------------*/
    private string ReplaceTokens(Topic topic, string source, string defaultValue = null) {
      if (topic != null && !String.IsNullOrEmpty(source)) {
        source = source
          .Replace("{Topic}", topic.Key)
          .Replace("{TopicId}", topic.Id.ToString())
          .Replace("{Name}", topic.Key)
          .Replace("{FullName}", topic.GetUniqueKey())
          .Replace("{Key}", topic.Key)
          .Replace("{UniqueKey}", topic.GetUniqueKey())
          .Replace("{WebPath}", topic.GetWebPath())
          .Replace("{Title}", topic.Title)
          .Replace("{Parent}", topic.Parent.GetUniqueKey())
          .Replace("{ParentId}", topic.Parent.Id.ToString())
          .Replace("{GrandParent}", topic.Parent?.Parent?.GetUniqueKey())
          .Replace("{GrandParentId}", topic.Parent?.Parent?.Id.ToString());
      }
      return source;
    }

  } // Class
} // Namespace