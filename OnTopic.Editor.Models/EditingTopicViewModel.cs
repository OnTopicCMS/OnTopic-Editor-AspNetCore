/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OnTopic.Mapping.Annotations;
using OnTopic.ViewModels;

namespace OnTopic.Editor.Models {

  /*============================================================================================================================
  | CLASS: EDITING TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for a <see cref="Topic"/>. Since attributes are handled via the <see cref="AttributeViewModel"/>, the
  ///   <see cref="EditingTopicViewModel"/> needn't account for them. It only accounts for items that will be exposed to the
  ///   general interface of the Topic Editor.
  /// </summary>
  public class EditingTopicViewModel: ViewModels.TopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeValue"/> class, using the specified key/value pair.
    /// </summary>
    public EditingTopicViewModel() : base() {}

    /*==========================================================================================================================
    | PROPERTY: VERSION HISTORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a collection of <see cref="DateTime"/> instances during which the represented <see cref="Topic"/> was
    ///   modified.
    /// </summary>
    #pragma warning disable CA2227 // Collection properties should be read only
    public Collection<DateTime> VersionHistory { get; set; } = new();
    #pragma warning restore CA2227 // Collection properties should be read only

    /*==========================================================================================================================
    | PROPERTY: DERIVED TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to a derived topic, if one exists.
    /// </summary>
    [AttributeKey("TopicID")]
    public TopicViewModel DerivedTopic { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A collection of attribute values, as assigned directly to the topic. Does not include inherited or derived values.
    /// </summary>
    public Dictionary<string, string> Attributes { get; } = new();

    /*==========================================================================================================================
    | PROPERTY: INHERITED ATTRIBUTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A collection of inherited attribute values, as inferred from derived or upstream topics.
    /// </summary>
    public Dictionary<string, string> InheritedAttributes { get; } = new();

  } // Class
} // Namespace