﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Attributes.HtmlAttribute;

namespace OnTopic.Editor.AspNetCore.Attributes.TextAreaAttribute {

  /*============================================================================================================================
  | CLASS: TEXT AREA ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TextAreaViewComponent"/>.
  /// </summary>
  public record TextAreaAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="TextAreaAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public TextAreaAttributeDescriptorViewModel(AttributeDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      Rows                      = attributes.GetInteger(nameof(Rows))?? Rows;
      MinimumLength             = attributes.GetInteger(nameof(MinimumLength))?? MinimumLength;
      MaximumLength             = attributes.GetInteger(nameof(MaximumLength))?? MaximumLength;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextAreaAttributeDescriptorViewModel"/> class.
    /// </summary>
    public TextAreaAttributeDescriptorViewModel() : base() { }

    /*==========================================================================================================================
    | ROWS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of rows (height) that the <see cref="HtmlViewComponent"/> should take up. Defaults to
    ///   <c>5</c>.
    /// </summary>
    public int? Rows { get; init; } = 5;

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