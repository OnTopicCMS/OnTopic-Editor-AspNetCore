/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Threading.Tasks;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Mvc.Models;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ignia.Topics.AspNetCore.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOPIC LOOKUP (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic lookup attribute type.
  /// </summary>
  public class TopicLookupViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository                = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicLookupViewComponent"/> with necessary dependencies.
    /// </summary>
    public TopicLookupViewComponent(
      ITopicRoutingService      topicRoutingService,
      ITopicRepository          topicRepository
    ) : base(topicRoutingService) {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="TopicLookupViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(AttributeDescriptorTopicViewModel attribute, string htmlFieldPrefix) {

      /*------------------------------------------------------------------------------------------------------------------------
      | DEFAULT PROCESSING
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
      var viewModel = (TopicLookupAttributeViewModel)GetAttributeViewModel(new TopicLookupAttributeViewModel(attribute));

      /*------------------------------------------------------------------------------------------------------------------------
      | SET DEFAULT OPTION
      \-----------------------------------------------------------------------------------------------------------------------*/
      var value = CurrentTopic.Attributes.GetValue(attribute.Key, attribute.DefaultValue, false, false);
      viewModel.Options.Add(
        new SelectListItem {
          Value = value,
          Text = value
        }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | SET OPTIONS
      \-----------------------------------------------------------------------------------------------------------------------*/
      //### TODO JJC20190324: Custom logic to lookup and set the TopicLookupAttributeViewModel.Options values.

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

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