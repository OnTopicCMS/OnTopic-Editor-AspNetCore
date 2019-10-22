/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Metadata;
using System;

#nullable enable

namespace Ignia.Topics.Editor.Models.Components.Options {

  /*============================================================================================================================
  | CLASS: WYSIWYG (OPTIONS)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Establishes options associated with the <see cref="BooleanViewComponent"/>.
  /// </summary>
  public class WysiwygOptions: DefaultOptions {


    /*==========================================================================================================================
    | COLUMNS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of columns (width) that the <see cref="WysiwygViewComponent"/> should take up. Defaults to
    ///   <c>70</c>.
    /// </summary>
    public int? Columns { get; set; }

    /*==========================================================================================================================
    | ROWS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of rows (height) that the <see cref="WysiwygViewComponent"/> should take up. Defaults to
    ///   <c>30</c>.
    /// </summary>
    public int? Rows { get; set; }

    /*==========================================================================================================================
    | HEIGHT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the number of pixels that the <see cref="WysiwygViewComponent"/> should take up. Defaults to <see
    ///   cref="Rows"/> x 20.
    /// </summary>
    /// <remarks>
    ///   If set, this value overrides <see cref="Rows"/>.
    /// </remarks>
    public int? Height { get; set; }


  } // Class
} // Namespace

#nullable restore