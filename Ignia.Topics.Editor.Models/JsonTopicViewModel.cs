/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using Ignia.Topics.Collections;
using Ignia.Topics.Repositories;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: JSON VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a <see cref="Topic"/> in a format optimized for JSON serialization.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class JsonTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="JsonTopicViewModel"/> class using a <see cref="Topic"/> to seed the
    ///   values.
    /// </summary>
    public JsonTopicViewModel(Topic topic, JsonTopicViewModelOptions options) : this(topic, null, options) { }

    public JsonTopicViewModel(
      Topic topic,
      ReadOnlyTopicCollection<Topic> related,
      JsonTopicViewModelOptions options
    ) : this(
      topic.Id,
      topic.Key,
      options.UseKeyAsText? topic.Key : topic.Title,
      topic.UniqueKey,
      options.MarkRelated? related.Contains(topic) : true,
      topic.Attributes.GetValue("DisableDelete", "0").Equals("1")
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle options
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (options.ResultLimit > 0) {
        options.ResultLimit--;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Initialize collection
      \-----------------------------------------------------------------------------------------------------------------------*/
      Children = new List<JsonTopicViewModel>();

      /*------------------------------------------------------------------------------------------------------------------------
      | Populate children
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (options.IsRecursive) {
        foreach (var child in topic.Children) {
          if (JsonTopicViewModel.IsValidTopic(child, options)) {
            Children.Add(new JsonTopicViewModel(child, related, options));
          }
        }
      }

    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="JsonTopicViewModel"/> class by specifying each of the property values.
    /// </summary>
    public JsonTopicViewModel(
      int                       id,
      string                    key,
      string                    title,
      string                    uniqueKey,
      bool                      isChecked                       = false,
      bool                      isDraggable                     = true
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      Id = id;
      Key = key;
      Title = title;
      UniqueKey = uniqueKey;
      WebPath = "/" + UniqueKey.Replace(':', '/');
      IsChecked = isChecked;
      IsDraggable = isDraggable;

      /*------------------------------------------------------------------------------------------------------------------------
      | Initialize collection
      \-----------------------------------------------------------------------------------------------------------------------*/
      Children = new List<JsonTopicViewModel>();

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

    /*==========================================================================================================================
    | ID
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The unique identifier as set in the database as the primary key.
    /// </summary>
    public int Id {
      get;
    }

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The key property of the topic.
    /// </summary>
    public string Key {
      get;
    }

    /*==========================================================================================================================
    | TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The title of the topic, which will fall back to <see cref="Key"/> if not set.
    /// </summary>
    public string Title {
      get;
    }

    /*==========================================================================================================================
    | UNIQUE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The fully-qualified, unique key for the topic, in OnTopic format.
    /// </summary>
    public string UniqueKey {
      get;
    }

    /*==========================================================================================================================
    | WEB PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The fully-qualified, unique path to the topic, in HTTP format.
    /// </summary>
    public string WebPath {
      get;
    }

    /*==========================================================================================================================
    | IS CHECKED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the current topic is selected or not.
    /// </summary>
    public bool IsChecked {
      get;
    }

    /*==========================================================================================================================
    | IS DRAGGABLE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the current topic is intended to be draggable or not.
    /// </summary>
    public bool IsDraggable {
      get;
    }

    /*==========================================================================================================================
    | IS LEAF?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the current topic is a leaf node or not.
    /// </summary>
    public bool IsLeaf {
      get => Children.Count.Equals(0);
    }

    /*==========================================================================================================================
    | CHILDREN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a collection of child objects.
    /// </summary>
    public List<JsonTopicViewModel> Children {
      get;
    }

    /*==========================================================================================================================
    | AS FLAT STRUCTURE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a flat list of the current <see cref="JsonTopicViewModel"/>.
    /// </summary>
    public List<JsonTopicViewModel> AsFlatStructure(List<JsonTopicViewModel> output = null) {
      output = output?? new List<JsonTopicViewModel>();
      output.Add(this);
      foreach (var topic in Children) {
        output = topic.AsFlatStructure(output);
      }
      Children.Clear();
      return output;
    }

  } //Class

} //Namespace
