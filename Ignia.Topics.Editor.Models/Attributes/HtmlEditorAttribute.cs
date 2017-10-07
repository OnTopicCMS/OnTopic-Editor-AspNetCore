/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;

namespace Ignia.Topics.Editor.Models.Attributes {

  /*============================================================================================================================
  | CLASS: HTML EDITOR ATTRIBUTE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of an HTML attribute in the Topic Editor.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class HtmlEditorAttribute : EditorAttribute {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     string                          _key                            = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditorAttribute"/> class, using the specified key/value pair.
    /// </summary>
    public HtmlEditorAttribute() : base() {
    }

    /*==========================================================================================================================
    | GET VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the value associated with the attribute.
    /// </summary>
    /// <remarks>
    ///   The CkEditor that Ignia uses for the frontend will occasionally inject extraneous line breaks. Prior to saving, this
    ///   strips any extra line breaks from the body.
    /// </remarks>
    public override string GetValue() {
      return Value.Replace("<p>&nbsp;</p>", "").Replace("<p>&amp;nbsp;</p>", "").Replace("<p></p>", "").Replace("\r\n", "");
    }

  } //Class

} //Namespace
