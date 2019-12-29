/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: TEXT AREA ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TextAreaViewComponentView"/>.
  /// </summary>
  public class TextAreaAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | COLUMNS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of columns (width) that the <see cref="HtmlViewComponent"/> should take up. Defaults to
    ///   <c>70</c>.
    /// </summary>
    public int? Columns { get; set; }

    /*==========================================================================================================================
    | ROWS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of rows (height) that the <see cref="HtmlViewComponent"/> should take up. Defaults to
    ///   <c>30</c>.
    /// </summary>
    public int? Rows { get; set; }

    /*==========================================================================================================================
    | MAXIMUM LENGTH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the maximimum length of the field.
    /// </summary>
    public int? MaximumLength { get; set; }

  } //Class
} //Namespace

#nullable restore