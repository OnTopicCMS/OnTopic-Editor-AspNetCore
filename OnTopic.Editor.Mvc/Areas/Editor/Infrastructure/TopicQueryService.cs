/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using OnTopic.Collections;
using System.Text.Json.Serialization;
using System.ComponentModel;
using OnTopic.Mapping;
using System.Threading.Tasks;

namespace OnTopic.Editor.Models.Queryable {

  /*============================================================================================================================
  | CLASS: TOPIC QUERY SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Constructs a hierarchy of <see cref="QueryResultTopicViewModel"/> objects based on a root <see cref="Topic"/> and a set
  ///   of options as specified in a <see cref="TopicQueryOptions"/> object.
  /// </summary>
  internal class TopicQueryService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TopicQueryService"/> class.
    /// </summary>
    public TopicQueryService() { }

    /*==========================================================================================================================
    | QUERY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Generates and returns a list of <see cref="QueryResultTopicViewModel"/> objects based on a root <see cref="Topic"/> as
    ///   well as a set of options as specified in a <see cref="TopicQueryOptions"/> object.
    /// </summary>
    public List<QueryResultTopicViewModel> Query(
      Topic rootTopic,
      TopicQueryOptions options,
      ReadOnlyTopicCollection<Topic> related = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish containers for mapped objects, tasks
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModels = new List<QueryResultTopicViewModel>();

      /*------------------------------------------------------------------------------------------------------------------------
      | Bootstrap mapping process
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (options.ShowRoot) {
        MapQueryResult(topicViewModels, rootTopic, options, related);
      }
      else {
        foreach (var topic in rootTopic.Children) {
          MapQueryResult(topicViewModels, topic, options, related);
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return results
      \-----------------------------------------------------------------------------------------------------------------------*/
      return topicViewModels;

    }

    /*==========================================================================================================================
    | MAP QUERY RESULT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Private helper function that maps a successfully validated <see cref="Topic"/> to a <see
    ///   cref="QueryResultTopicViewModel"/>.
    /// </summary>
    private void MapQueryResult(
      List<QueryResultTopicViewModel> topicList,
      Topic topic,
      TopicQueryOptions options,
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
        var mappedTopic = new QueryResultTopicViewModel(
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
          MapQueryResult(
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
    ///   Static method confirms whether a topic is valid based on the <see cref="TopicQueryOptions"/>.
    /// </summary>
    public static bool IsValidTopic(Topic topic, TopicQueryOptions options) {

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