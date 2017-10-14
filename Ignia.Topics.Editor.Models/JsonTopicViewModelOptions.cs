/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using Ignia.Topics.Collections;
using Ignia.Topics.Repositories;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: JSON VIEW MODEL OPTIONS
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Tracks options for the <see cref="JsonTopicViewModel"/> class.
  /// </summary>
  public class JsonTopicViewModelOptions {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="JsonTopicViewModelOptions"/> class.
    /// </summary>
    public JsonTopicViewModelOptions() {

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

    }

    /*==========================================================================================================================
    | SHOW ROOT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determine whether or not the root passed should be displayed in the tree view, or if it should only display children.
    /// </summary>
    public bool ShowRoot {
      get;
      set;
    }

    /*==========================================================================================================================
    | SHOW ALL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determine whether or not hidden and inactive children should be displayed; typically used exclusively for the editor.
    /// </summary>
    public bool ShowAll {
      get;
      set;
    }

    /*==========================================================================================================================
    | USE KEY AS TEXT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the name should be displayed using the Title (if available) or the Key.  Defaults to Title.
    /// </summary>
    public bool UseKeyAsText {
      get;
      set;
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
      set;
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
      set;
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
      set;
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
      set;
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
      set;
    }

    /*==========================================================================================================================
    | ATTRIBUTE NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   May optionally filter out topics based on attribute values; if so, this property defines the attribute name.
    /// </summary>
    public string AttributeName {
      get;
      set;
    }

    /*==========================================================================================================================
    | ATTRIBUTE VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   May optionally filter out topics based on attribute values; if so, this property defines the attribute value.
    /// </summary>
    public string AttributeValue {
      get;
      set;
    }

    /*==========================================================================================================================
    | QUERY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   May optionally filter out topics based on attribute values; if so, this property defines the attribute value.
    /// </summary>
    public string Query {
      get;
      set;
    }

  } //Class

} //Namespace
