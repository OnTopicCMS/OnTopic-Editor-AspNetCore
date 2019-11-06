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

namespace Ignia.Topics.Editor.Models.Json {

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
    ///   Initializes a new instance of the <see cref="JsonTopicViewModel"/> class by specifying each of the property values.
    /// </summary>
    public JsonTopicViewModel(
      int                       id,
      string                    key,
      string                    title,
      string                    uniqueKey,
      string                    webPath,
      bool?                     isChecked                       = null,
      bool                      isDraggable                     = true
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
    public bool IsLeaf {
      get => Children.Count.Equals(0);
    }

    /*==========================================================================================================================
    | CHILDREN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a collection of child objects.
    /// </summary>
    public List<JsonTopicViewModel> Children { get; } = new List<JsonTopicViewModel>();

  } // Class
} // Namespace