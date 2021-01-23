/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using System.Linq;
using OnTopic.Collections;
using OnTopic.Internal.Diagnostics;

namespace OnTopic.Editor.AspNetCore.Models.Queryable {

  /*============================================================================================================================
  | CLASS: TOPIC QUERY SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Constructs a hierarchy of <see cref="QueryResultTopicViewModel"/> objects based on a root <see cref="Topic"/> and a set
  ///   of options as specified in a <see cref="TopicQueryOptions"/> object.
  /// </summary>
  public class TopicQueryService {

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
    public Collection<QueryResultTopicViewModel> Query(
      Topic rootTopic,
      TopicQueryOptions options,
      ReadOnlyTopicCollection related = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(rootTopic, nameof(rootTopic));
      Contract.Requires(options, nameof(options));

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish containers for mapped objects, tasks
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topicViewModels = new Collection<QueryResultTopicViewModel>();

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish counter
      \-----------------------------------------------------------------------------------------------------------------------*/
      var remainingResults = options.ResultLimit;

      /*------------------------------------------------------------------------------------------------------------------------
      | Bootstrap mapping process
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (options.ShowRoot) {
        MapQueryResult(topicViewModels, rootTopic, options, ref remainingResults, related);
      }
      else {
        foreach (var topic in rootTopic.Children) {
          MapQueryResult(topicViewModels, topic, options, ref remainingResults, related);
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
      Collection<QueryResultTopicViewModel> topicList,
      Topic topic,
      TopicQueryOptions options,
      ref int remainingResults,
      ReadOnlyTopicCollection related = null
    )
    {

      /*------------------------------------------------------------------------------------------------------------------------
      | Loop through children
      \-----------------------------------------------------------------------------------------------------------------------*/
      var isValid = IsValidTopic(topic, options, remainingResults);

      /*------------------------------------------------------------------------------------------------------------------------
      | Map topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (isValid) {

        //Decrement counter
        remainingResults--;

        //Map topic
        var mappedTopic = new QueryResultTopicViewModel(
          topic.Id,
          topic.Key,
          options.UseKeyAsText ? topic.Key : topic.Title,
          topic.GetUniqueKey(),
          topic.GetWebPath(),
          options.EnableCheckboxes ? (!options.MarkRelated || related.Contains(topic)) : new bool?(),
          topic.Attributes.GetValue("DisableDelete", "0") is "0",
          options.ExpandRelated && related.Any(r => r.GetUniqueKey().StartsWith(topic.GetUniqueKey(), StringComparison.Ordinal))
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
            ref remainingResults,
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
    public static bool IsValidTopic(Topic topic, TopicQueryOptions options, int remainingResults) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(topic, nameof(topic));
      Contract.Requires(options, nameof(options));

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var searchTerms = (options.Query ?? "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate basic properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!options.ShowAll && !topic.IsVisible()) return false;
      if (!options.ShowNestedTopics && topic.ContentType is "List") return false;
      if (remainingResults is 0) return false;

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate filtered attribute
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrEmpty(options.AttributeName)) {
        var attributeValue = topic.Attributes.GetValue(options.AttributeName, "");
        if (options.AttributeName is "ContentType") {
          attributeValue = topic.ContentType;
        }
        if (options.UsePartialMatch) {
          if (attributeValue.IndexOf(options.AttributeValue, StringComparison.Ordinal) is -1) {
            return false;
          }
        }
        if (!attributeValue.Equals(options.AttributeValue, StringComparison.Ordinal)) {
          return false;
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate search results
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (searchTerms.Count > 0) {
        if (!searchTerms.All(
          searchTerm => topic.Attributes.Any(
            a => a.Value.IndexOf(searchTerm, 0, StringComparison.OrdinalIgnoreCase) >= 0
          )
        )) {
          return false;
        }
      }

      return true;

    }

  } // Class
} // Namespace