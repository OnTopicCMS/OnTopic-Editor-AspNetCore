/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnTopic.Collections;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Editor.Models.Queryable;
using OnTopic.Editor.Mvc.Models;
using OnTopic.Querying;
using OnTopic.Repositories;
using OnTopic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnTopic.Editor.Mvc.Components {

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
      string htmlFieldPrefix = null,
      IEnumerable<TopicViewModel> values = null,
      string targetUrl = null,
      bool? enableModal = null,
      string onClientClose = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.DefaultLabel    ??= attribute.GetConfigurationValue(            "Label",                "Select a Topic…");
      attribute.RootTopicKey    ??= attribute.GetConfigurationValue(            "Scope",                null);
      attribute.AttributeKey    ??= attribute.GetConfigurationValue(            "AttributeName",        null);
      attribute.AttributeValue  ??= attribute.GetConfigurationValue(            "AttributeValue",       null);
      attribute.ValueProperty   ??= attribute.GetConfigurationValue(            "ValueProperty",        null);
      var       allowedKeys       = attribute.GetConfigurationValue(            "AllowedKeys",          null);
                enableModal     ??= attribute.GetBooleanConfigurationValue(     "TargetPopup",          false);
                targetUrl       ??= attribute.GetConfigurationValue(            "TargetUrl",            null);
                onClientClose   ??= attribute.GetConfigurationValue(            "OnClientClose",        null);

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
          Value = null,
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

      //### HACK JJC20191031: Since Topic and TopicViewModel aren't intercompatible, and the remaining processing is based on
      //Topic, we're converting any preconfigured topics from TopicViewModel back to Topic by looking them up in the repository.
      //This, of course, assumes that the topic view models refer to existing topics in the repository.
      if (values != null && values.Count() > 0) {
        topics = new List<QueryResultTopicViewModel>();
        foreach (var topicViewModel in values.Where(t => !t.IsHidden)) {
          topics.Add(
            new QueryResultTopicViewModel(
              topicViewModel.Id,
              topicViewModel.Key,
              topicViewModel.Title,
              topicViewModel.UniqueKey,
              topicViewModel.WebPath
            )
          );
        }
      }
      else if (attribute.RelativeTopicBase != null) {
        var baseTopic             = _topicRepository.Load(currentTopic.UniqueKey);
        var rootTopic             = attribute.RelativeTopicBase switch {
          "CurrentTopic"          => baseTopic,
          "ParentTopic"           => baseTopic.Parent,
          "GrandparentTopic"      => (Topic)baseTopic.Parent?.Parent,
          "ContentTypeDescriptor" => (Topic)_topicRepository.GetContentTypeDescriptors().Where(t => t.Key.Equals(baseTopic.ContentType)),
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
      | Set navigation related properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      viewModel.TargetUrl       = targetUrl;
      viewModel.EnableModal     = enableModal;
      viewModel.OnModalClose    = onClientClose;

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Helper functions
      \-----------------------------------------------------------------------------------------------------------------------*/
      string getValue(QueryResultTopicViewModel topic) {
        if (!String.IsNullOrEmpty(targetUrl)) {
          // Add TopicID if not already available
          var uniqueTargetUrl = targetUrl;
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
    public List<QueryResultTopicViewModel> GetTopics(
      Topic  topic              = null,
      string attributeKey       = null,
      string attributeValue     = null,
      string allowedKeys        = ""
    ) {

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