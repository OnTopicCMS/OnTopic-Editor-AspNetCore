﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.TextAttribute {

  /*============================================================================================================================
  | CLASS: TEXT ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TextViewComponent"/>.
  /// </summary>
  public record TextAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="TextAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public TextAttributeDescriptorViewModel(AttributeDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      MinimumLength             = attributes.GetInteger(nameof(MinimumLength));
      MaximumLength             = attributes.GetInteger(nameof(MaximumLength));
      InputType                 = attributes.GetValue(nameof(InputType));
      Pattern                   = attributes.GetValue(nameof(Pattern));
      ValidationMessage         = attributes.GetValue(nameof(ValidationMessage));
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextAttributeDescriptorViewModel"/> class.
    /// </summary>
    public TextAttributeDescriptorViewModel() : base() { }

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
    ///   Gets or sets the maximimum length of the field. Defaults to <see cref="Int32.MaxValue"/> if undefined.
    /// </summary>
    public int? MaximumLength { get; init; }

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