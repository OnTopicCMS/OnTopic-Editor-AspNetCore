/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: HTML ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="HtmlViewComponent"/>.
  /// </summary>
  public class HtmlAttributeTopicViewModel: TextAreaAttributeTopicViewModel {

    /*==========================================================================================================================
    | ROWS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc/>
    public override int? Rows { get; set; } = 20;

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
    public int? Height { get; set; }

  } //Class
} //Namespace

#nullable restore