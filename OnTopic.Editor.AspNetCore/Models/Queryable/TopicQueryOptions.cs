﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Models.Queryable {

  /*============================================================================================================================
  | CLASS: TOPIC QUERY OPTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Establishes options for the <see cref="TopicQueryService"/> class.
  /// </summary>
  public record TopicQueryOptions {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    bool                        _markRelated;
    bool                        _expandRelated                  = true;
    bool                        _showCheckboxes;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TopicQueryOptions"/> class.
    /// </summary>
    public TopicQueryOptions() {

      /*--------------------------------------------------------------------------------------------------------------------------
      | Set default values
      \-------------------------------------------------------------------------------------------------------------------------*/
      ShowRoot                  = false;
      ShowAll                   = true;
      UseKeyAsText              = false;
      IsRecursive               = true;
      FlattenStructure          = false;
      ShowNestedTopics          = false;
      UsePartialMatch           = false;
      ResultLimit               = -1;
      AttributeName             = null;
      AttributeValue            = null;
      Query                     = null;
      MarkRelated               = false;
      ExpandRelated             = true;
      RelatedTopicId            = -1;
      RelatedNamespace          = null;
      EnableCheckboxes          = false;

    }

    /*==========================================================================================================================
    | SHOW ROOT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determine whether or not the root passed should be displayed in the tree view, or if it should only display children.
    /// </summary>
    public bool ShowRoot {
      get;
      init;
    }

    /*==========================================================================================================================
    | SHOW ALL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determine whether or not hidden and inactive children should be displayed; typically used exclusively for the editor.
    /// </summary>
    public bool ShowAll {
      get;
      init;
    }

    /*==========================================================================================================================
    | USE KEY AS TEXT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the name should be displayed using the Title (if available) or the Key.  Defaults to Title.
    /// </summary>
    public bool UseKeyAsText {
      get;
      init;
    }

    /*==========================================================================================================================
    | IS RECURSIVE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determine whether grandchildren of the RootTopic should be displayed, or whether the tree should only show one tier of
    ///   Topics.  Defaults to recursive.
    /// </summary>
    public bool IsRecursive {
      get;
      init;
    }

    /*==========================================================================================================================
    | FLATTEN STRUCTURE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determine whether all Topics should be added to the output at the same (top) level, or whether sub-tiers of Topics
    ///   should be added to the output under a "children" array (of the parent).
    /// </summary>
    public bool FlattenStructure {
      get;
      init;
    }

    /*==========================================================================================================================
    | SHOW NESTED TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determine whether or not nested topics (i.e., topics within List ContentTypes) should be displayed or not.  By default,
    ///   it is assumed that they should not be displayed.
    /// </summary>
    public bool ShowNestedTopics {
      get;
      init;
    }

    /*==========================================================================================================================
    | USE PARTIAL MATCH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   If set, changes the FilterChildren query to find Topics based on a partial match against the specified AttributeName's
    ///   AttributeValue, if both are present; otherwise, the query returns only exact matches.
    /// </summary>
    public bool UsePartialMatch {
      get;
      init;
    }

    /*==========================================================================================================================
    | RESULT LIMIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   If set, should limit the number of Topics loaded/output to the JSON. Includes setter in order to be decremented along
    ///   with resultLimit in AddNodeToOutput().
    /// </summary>
    public int ResultLimit {
      get;
      init;
    }

    /*==========================================================================================================================
    | ATTRIBUTE NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   May optionally filter out topics based on attribute values; if so, this property defines the attribute name.
    /// </summary>
    public string? AttributeName {
      get;
      init;
    }

    /*==========================================================================================================================
    | ATTRIBUTE VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   May optionally filter out topics based on attribute values; if so, this property defines the attribute value.
    /// </summary>
    public string? AttributeValue {
      get;
      init;
    }

    /*==========================================================================================================================
    | QUERY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   May optionally filter out topics based on attribute values; if so, this property defines the attribute value.
    /// </summary>
    public string? Query {
      get;
      init;
    }

    /*==========================================================================================================================
    | ENABLE CHECKBOXES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not to display checkboxes by including the <see cref="QueryResultTopicViewModel.IsChecked"/> value.
    /// </summary>
    /// <remarks>
    ///   This will automatically be set to true is <see cref="MarkRelated"/> is set to <c>true</c>. Otherwise, it defaults to
    ///   false.
    /// </remarks>
    public bool EnableCheckboxes {
      get => (MarkRelated || _showCheckboxes);
      set => _showCheckboxes = value;
    }

    /*==========================================================================================================================
    | MARK RELATED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether <see cref="QueryResultTopicViewModel"/>s should be marked as <see cref="QueryResultTopicViewModel.
    ///   IsChecked"/> based on their presence in related topics.
    /// </summary>
    /// <remarks>
    ///   This will automatically be set to true if <see cref="RelatedTopicId"/> or <see cref="RelatedNamespace"/> are set. If
    ///   <see cref="RelatedTopicId"/> is <i>not</i> set, then the current <see cref="Topic"/> should be assumed.
    /// </remarks>
    public bool MarkRelated {
      get => (RelatedTopicId > 0 || !String.IsNullOrEmpty(RelatedNamespace) || _markRelated);
      set => _markRelated = value;
    }

    /*==========================================================================================================================
    | EXPAND RELATED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether <see cref="QueryResultTopicViewModel"/>s should be marked ascendants as nodes marked as <see
    ///   cref="QueryResultTopicViewModel.IsChecked"/> as <see cref="QueryResultTopicViewModel.IsExpanded"/>.
    /// </summary>
    /// <remarks>
    ///   This will automatically be set to true if <see cref="MarkRelated"/> is set to true.
    /// </remarks>
    public bool ExpandRelated {
      get => (MarkRelated && _expandRelated);
      set => _expandRelated = value;
    }

    /*==========================================================================================================================
    | RELATED TOPIC ID
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The identifier of the topic to use to determine if items are marked as checked, based on its relationships.
    /// </summary>
    public int RelatedTopicId {
      get;
      init;
    }

    /*==========================================================================================================================
    | RELATED NAMESPACE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The name of the relationship scope to use with the <see cref="RelatedTopicId"/>.
    /// </summary>
    public string? RelatedNamespace {
      get;
      init;
    }

  } // Class
} // Namespace