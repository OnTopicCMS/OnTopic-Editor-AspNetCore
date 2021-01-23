/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using OnTopic.Editor.Models.Metadata;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.TextAreaAttribute {

  /*============================================================================================================================
  | CLASS: TEXT AREA ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TextAreaViewComponentView"/>.
  /// </summary>
  public record TextAreaAttributeDescriptorTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | ROWS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of rows (height) that the <see cref="HtmlViewComponent"/> should take up. Defaults to
    ///   <c>20</c>.
    /// </summary>
    public virtual int? Rows { get; init; } = 5;

    /*==========================================================================================================================
    | MINIMUM LENGTH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the length length of the field. Defaults to <c>0</c> if undefined.
    /// </summary>
    public int? MinimumLength { get; init; } = 10;

    /*==========================================================================================================================
    | MAXIMUM LENGTH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the maximimum length of the field.
    /// </summary>
    public int? MaximumLength { get; init; } = Int32.MaxValue;

  } //Class
} //Namespace

#nullable restore