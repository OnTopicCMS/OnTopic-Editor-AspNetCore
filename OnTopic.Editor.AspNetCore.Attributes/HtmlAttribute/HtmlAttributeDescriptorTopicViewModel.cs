/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Metadata;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.HtmlAttribute {

  /*============================================================================================================================
  | CLASS: HTML ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="HtmlViewComponent"/>.
  /// </summary>
  public record HtmlAttributeDescriptorTopicViewModel: TextAreaAttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | ROWS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public override int? Rows { get; init; } = 20;

    /*==========================================================================================================================
    | HEIGHT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of pixels that the <see cref="HtmlViewComponent"/> should take up. Defaults to <see
    ///   cref="Rows"/> x 20.
    /// </summary>
    /// <remarks>
    ///   If set, this value overrides <see cref="Rows"/>.
    /// </remarks>
    public int? Height { get; init; }

  } //Class
} //Namespace

#nullable restore