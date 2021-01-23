/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OnTopic.Editor.AspNetCore.Models.Queryable {

  /*============================================================================================================================
  | CLASS: QUERY RESULT (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a view model for query results returned from the <see cref="TopicQueryService"/>.
  /// </summary>
  ///   The <see cref="QueryResultTopicViewModel"/> includes annotations that allow it to be serialized as JSON in compliance
  ///   with the expectations of the <see cref="EditorController.Json(TopicQueryOptions)"/> action.
  /// <remarks>
  /// </remarks>
  public record QueryResultTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="QueryResultTopicViewModel"/> class by specifying each of the property
    ///   values.
    /// </summary>
    public QueryResultTopicViewModel(
      int                       id,
      string                    key,
      string                    title,
      string                    uniqueKey,
      string                    webPath,
      bool?                     isChecked                       = null,
      bool                      isDraggable                     = true,
      bool                      isExpanded                      = false
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      Id                        = id;
      Key                       = key;
      Title                     = title;
      UniqueKey                 = uniqueKey;
      WebPath                   = webPath;
      IsDraggable               = isDraggable;
      IsExpanded                = isExpanded;

      if (isChecked.HasValue) {
        IsChecked               = isChecked.Value;
      }

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
    [JsonPropertyName("text")]
    public string Title {
      get;
    }

    /*==========================================================================================================================
    | UNIQUE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The fully-qualified, unique key for the topic, in OnTopic format.
    /// </summary>
    [JsonPropertyName("path")]
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
    [JsonPropertyName("checked")]
    public bool? IsChecked {
      get;
    }

    /*==========================================================================================================================
    | IS DRAGGABLE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the current topic is intended to be draggable or not.
    /// </summary>
    [JsonPropertyName("draggable")]
    [DefaultValue(true)]
    public bool IsDraggable {
      get;
    }

    /*==========================================================================================================================
    | IS LEAF?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the current topic is a leaf node or not.
    /// </summary>
    [JsonPropertyName("leaf")]
    public bool IsLeaf => Children.Count is 0;

    /*==========================================================================================================================
    | IS EXPANDED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the current node should be expanded, assuming it's not a <see cref="IsLead"/> node.
    /// </summary>
    /// <remarks>
    ///   The <c>TopicQueryService</c> will set this value based if a <see cref="TopicQueryOptions.RelatedTopicId"/> is
    ///   specified, and <see cref="TopicQueryOptions.MarkRelated"/> is set to <c>true</c>. Specifically, it will mark any item
    ///   whose <see cref="Topic.GetUniqueKey()"/> starts with the <see cref="Topic.GetUniqueKey()"/> of any <see cref="Topic.
    ///   Relationships"/> associated with the <see cref="TopicQueryOptions.RelatedTopicId"/>, thus ensuring that all selected
    ///   items are expanded.
    /// </remarks>
    [JsonPropertyName("expanded")]
    public bool IsExpanded {
      get;
    }

    /*==========================================================================================================================
    | CHILDREN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a collection of child objects.
    /// </summary>
    public Collection<QueryResultTopicViewModel> Children { get; } = new();

  } // Class
} // Namespace