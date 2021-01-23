/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using OnTopic.Editor.Models.Metadata;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.TextAttribute {

  /*============================================================================================================================
  | CLASS: TEXT ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TextViewComponentView"/>.
  /// </summary>
  public record TextAttributeDescriptorTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | MINIMUM LENGTH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the length length of the field. Defaults to <c>0</c> if undefined.
    /// </summary>
    public int? MinimumLength { get; init; }

    /*==========================================================================================================================
    | MAXIMUM LENGTH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the maximimum length of the field. Defaults to <c>500</c> if undefined.
    /// </summary>
    public int? MaximumLength { get; init; } = Int32.MaxValue;

    /*==========================================================================================================================
    | INPUT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the HTML input type of the field. Defaults to <c>text</c> if undefined.
    /// </summary>
    public string? InputType { get; init; } = "text";

    /*==========================================================================================================================
    | PATTERN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a regular expression which should be used to validate the input.
    /// </summary>
    public string? Pattern { get; init; }

    /*==========================================================================================================================
    | VALIDATION MESSAGE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a hint to provide if validation fails.
    /// </summary>
    public string? ValidationMessage { get; init; }

  } //Class
} //Namespace

#nullable restore