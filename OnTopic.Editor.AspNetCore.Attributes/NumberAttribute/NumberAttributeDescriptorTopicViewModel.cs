/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using OnTopic.Editor.Models.Metadata;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.NumberAttribute {

  /*============================================================================================================================
  | CLASS: NUMBER ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="NumberViewComponentView"/>.
  /// </summary>
  public record NumberAttributeDescriptorTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: MINIMUM VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the lower bound for acceptable values. Defaults to 0.
    /// </summary>
    public int MinimumValue { get; init; }

    /*==========================================================================================================================
    | PROPERTY: MAXIMUM VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the upper bound for acceptable values. Defaults to <see cref="Int32.Max"/>.
    /// </summary>
    public int MaximumValue { get; init; } = Int32.MaxValue;

  } //Class
} //Namespace

#nullable restore