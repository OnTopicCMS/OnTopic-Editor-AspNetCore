/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OnTopic.ViewModels;

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: EDITING TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for a <see cref="Topic"/>. Since attributes are handled via the <see cref="AttributeViewModel"/>, the
  ///   <see cref="EditingTopicViewModel"/> needn't account for them. It only accounts for items that will be exposed to the
  ///   general interface of the Topic Editor.
  /// </summary>
  public record EditingTopicViewModel: ViewModels.TopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditingTopicViewModel"/> class.
    /// </summary>
    public EditingTopicViewModel() : base() {}

    /*==========================================================================================================================
    | PROPERTY: VERSION HISTORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a collection of <see cref="DateTime"/> instances during which the represented <see cref="Topic"/> was
    ///   modified.
    /// </summary>
    public Collection<DateTime> VersionHistory { get; init; } = new();

    /*==========================================================================================================================
    | PROPERTY: BASE TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to a base topic, if one exists.
    /// </summary>
    public TopicViewModel? BaseTopic { get; init; }

    /*==========================================================================================================================
    | PROPERTY: NO INDEX?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the current topic should be indexed by search engines or not.
    /// </summary>
    public bool NoIndex { get; init; }

    /*==========================================================================================================================
    | PROPERTY: IS HIDDEN?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the current topic is hidden or not.
    /// </summary>
    public new bool IsHidden { get; init; }

    /*==========================================================================================================================
    | PROPERTY: IS DISABLED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the current topic is disabled or not.
    /// </summary>
    public bool IsDisabled { get; init; }

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A collection of attribute values, as assigned directly to the topic. Does not include inherited or derived values.
    /// </summary>
    public Dictionary<string, string?> Attributes { get; } = new();

    /*==========================================================================================================================
    | PROPERTY: INHERITED ATTRIBUTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A collection of inherited attribute values, as inferred from derived or upstream topics.
    /// </summary>
    public Dictionary<string, string?> InheritedAttributes { get; } = new();

  } // Class
} // Namespace