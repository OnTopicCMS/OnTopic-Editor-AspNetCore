/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using OnTopic.Metadata;

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE DESCRIPTOR TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides core properties from a <see cref="AttributeDescriptor"/> to a view component.
  /// </summary>
  public record AttributeDescriptorTopicViewModel: ViewModels.TopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: DESCRIPTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a friendly description for the <see cref="AttributeDescriptor"/>, intended as documentation for users of the
    ///   editor.
    /// </summary>
    public string Description { get; init; }

    /*==========================================================================================================================
    | PROPERTY: MODEL TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines how the attribute is modeled in terms of the object-oriented code (e.g., as a relationship? An attribute?).
    /// </summary>
    public virtual ModelType ModelType { get; init; }

    /*==========================================================================================================================
    | PROPERTY: EDITOR TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the editor type to display for the attribute.
    /// </summary>
    /// <remarks>
    ///   In OnTopic 4.0.0+, the <see cref="EditorType"/> corresponds to the <see cref="AttributeTypeDescriptor"/> subtype name,
    ///   such as <see cref="BooleanAttribute"/>. This can be used by the editor to determine the appropriate view component to
    ///   display.
    /// </remarks>
    public virtual string EditorType { get; init; }

    /*==========================================================================================================================
    | PROPERTY: DISPLAY GROUP
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines what group of attributes to associate the current attribute with.
    /// </summary>
    public string DisplayGroup { get; init; }

    /*==========================================================================================================================
    | PROPERTY: IS REQUIRED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the attribute should be considered required or not.
    /// </summary>
    public bool IsRequired { get; init; }

    /*==========================================================================================================================
    | PROPERTY: DEFAULT VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines a default value for new topics.
    /// </summary>
    /// <remarks>
    ///   Assuming a topic is new, and isn't derived from another topic, the <see cref="DefaultValue"/> should be treated as the
    ///   value. This helps establish a logical default, but also prevents values from being inherited from e.g. parent topics;
    ///   as such, this should be used with caution. The value used here will be stored locally on a per-topic basis.
    /// </remarks>
    public string DefaultValue { get; init; }

    /*==========================================================================================================================
    | PROPERTY: IMPLICIT VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines the value that the code is expected to assign if an attribute's value is left empty.
    /// </summary>
    /// <remarks>
    ///   The <see cref="ImplicitValue"/> is not actually used by code to set the value. It is simply a way of communicating to
    ///   editors what value it is <i>expected</i> code will use. In practice, code may set any default it wishes, and may even
    ///   set different defaults in different contexts (e.g., based on different views). The <see cref="ImplicitValue"/> will be
    ///   exposed to editors as an HTML placeholder on input fields that support it.
    /// </remarks>
    public string ImplicitValue { get; init; }

    /*==========================================================================================================================
    | PROPERTY: SORT ORDER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the attribute's prioritization in the page order.
    /// </summary>
    public int SortOrder { get; init; }

    /*==========================================================================================================================
    | IS ENABLED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the field should be enabled, as defined on the <see cref="AttributeValue"/> instance.
    /// </summary>
    public bool IsEnabled { get; init; } = true;

  } //Class
} //Namespace