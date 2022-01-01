﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Mapping.Annotations;

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: EDITING TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for a <see cref="Topic"/>. Since attributes are handled via the <see cref="AttributeViewModel"/>, the
  ///   <see cref="EditingTopicViewModel"/> needn't account for them. It only accounts for items that will be exposed to the
  ///   general interface of the Topic Editor.
  /// </summary>
  public record EditingTopicViewModel: CoreTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditingTopicViewModel"/> class.
    /// </summary>
    public EditingTopicViewModel() : base() {}

    /*==========================================================================================================================
    | PROPERTY: PARENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to a <see cref="Topic.Parent"/>.
    /// </summary>
    [MapAs(typeof(CoreTopicViewModel))]
    public CoreTopicViewModel? Parent { get; init; }

    /*==========================================================================================================================
    | PROPERTY: BASE TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to a <see cref="Topic.BaseTopic"/>, if one exists.
    /// </summary>
    [MapAs(typeof(CoreTopicViewModel))]
    public CoreTopicViewModel? BaseTopic { get; init; }

    /*==========================================================================================================================
    | PROPERTY: VERSION HISTORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a collection of <see cref="DateTime"/> instances during which the represented <see cref="Topic"/> was
    ///   modified.
    /// </summary>
    public Collection<DateTime> VersionHistory { get; init; } = new();

    /*==========================================================================================================================
    | PROPERTY: LAST MODIFIED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="Topic.LastModified"/> date.
    /// </summary>
    public DateTime LastModified { get; init; }

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
    public bool IsHidden { get; init; }

    /*==========================================================================================================================
    | PROPERTY: IS DISABLED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the current topic is disabled or not.
    /// </summary>
    public bool IsDisabled { get; init; }

    /*==========================================================================================================================
    | PROPERTY: IS PROTECTED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the current topic part of the system.
    /// </summary>
    /// <remarks>
    ///   The <see cref="IsProtected"/> operates similar to <see cref="ContentTypeDescriptorViewModel.DisableDelete"/>, except
    ///   that it can be assigned to an individual <see cref="Topic"/>, and not <i>just</i> a <see cref="ContentTypeDescriptor"
    ///   />. If the <see cref="IsProtected"/> is set, then the topic cannot be deleted or moved directly via the editor—
    ///   though nothing prevents these operations from being performed programmatically.
    /// </remarks>
    public bool IsProtected { get; init; }

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