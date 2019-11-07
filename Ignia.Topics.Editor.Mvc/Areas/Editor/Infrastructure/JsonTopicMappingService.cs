/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using Ignia.Topics.Collections;
using System.Text.Json.Serialization;
using System.ComponentModel;
using Ignia.Topics.Mapping;
using System.Threading.Tasks;

namespace Ignia.Topics.Editor.Models.Json {

  /*============================================================================================================================
  | CLASS: JSON TOPIC MAPPING SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Constructs a hierarchy of <see cref="JsonTopicViewModel"/> instances based on a root <see cref="Topic"/> and a set of
  ///   <see cref="JsonTopicViewModelOptions"/>.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class JsonTopicMappingService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="JsonTopicMappingService"/> class.
    /// </summary>
    public JsonTopicMappingService() { }

    /*==========================================================================================================================
    | MAP GRAPH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Generates and returns a list of <see cref="JsonTopicViewModel"/> objects based on a root <see cref="Topic"/> as well
    ///   as a set of <see cref="JsonTopicViewModelOptions"/>.
    /// </summary>
    public List<JsonTopicViewModel> MapGraph(
      Topic rootTopic,
      JsonTopicViewModelOptions options,
      ReadOnlyTopicCollection<Topic> related = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish containers for mapped objects, tasks
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModels = new List<JsonTopicViewModel>();

      /*------------------------------------------------------------------------------------------------------------------------
      | Bootstrap mapping process
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (options.ShowRoot) {
        MapTopic(topicViewModels, rootTopic, options, related);
      }
      else {
        foreach (var topic in rootTopic.Children) {
          MapTopic(topicViewModels, topic, options, related);
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return results
      \-----------------------------------------------------------------------------------------------------------------------*/
      return topicViewModels;

    }

    /*==========================================================================================================================
    | MAP TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Private helper function that
    /// </summary>
    private void MapTopic(
      List<JsonTopicViewModel> topicList,
      Topic topic,
      JsonTopicViewModelOptions options,
      ReadOnlyTopicCollection<Topic> related = null
    )
    {

      /*------------------------------------------------------------------------------------------------------------------------
      | Loop through children
      \-----------------------------------------------------------------------------------------------------------------------*/
      var isValid = IsValidTopic(topic, options);

      /*------------------------------------------------------------------------------------------------------------------------
      | Map topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (isValid) {

        //Decrement counter
        options.ResultLimit--;

        //Map topic
        var mappedTopic = new JsonTopicViewModel(
          topic.Id,
          topic.Key,
          options.UseKeyAsText ? topic.Key : topic.Title,
          topic.GetUniqueKey(),
          topic.GetWebPath(),
          options.EnableCheckboxes ? (options.MarkRelated ? related.Contains(topic) : true) : new bool?(),
          topic.Attributes.GetValue("DisableDelete", "0").Equals("0")
        );

        //Add topic to topic list
        topicList.Add(mappedTopic);

        //Handle recursion, if appropriate
        topicList = options.FlattenStructure ? topicList : mappedTopic.Children;

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Loop through children (asynchronously)
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (isValid && options.IsRecursive || options.FlattenStructure ) {
        foreach (var childTopic in topic.Children) {
          MapTopic(
            topicList,
            childTopic,
            options,
            related
          );
        }
      }

    }

    /*==========================================================================================================================
    | IS VALID TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Static method confirms whether a topic is valid based on the <see cref="JsonTopicViewModelOptions"/>.
    /// </summary>
    public static bool IsValidTopic(Topic topic, JsonTopicViewModelOptions options) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var searchTerms = (options.Query ?? "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate basic properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!options.ShowAll && !topic.IsVisible()) return false;
      if (!options.ShowNestedTopics && topic.ContentType.Equals("List")) return false;
      if (options.ResultLimit.Equals(0)) return false;

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate filtered attribute
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrEmpty(options.AttributeName) && !String.IsNullOrEmpty(options.AttributeName)) {
        if (options.UsePartialMatch) {
          if (topic.Attributes.GetValue(options.AttributeName, "").IndexOf(options.AttributeValue) == -1) {
            return false;
          }
        }
        if (!topic.Attributes.GetValue(options.AttributeName, "").Equals(options.AttributeValue)) {
          return false;
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate search results
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (searchTerms.Count > 0) {
        if (!searchTerms.All(
          searchTerm => topic.Attributes.Any(
            a => a.Value.IndexOf(searchTerm, 0, StringComparison.InvariantCultureIgnoreCase) >= 0
          )
        )) {
          return false;
        }
      }

      return true;

    }

  } // Class
} // Namespace