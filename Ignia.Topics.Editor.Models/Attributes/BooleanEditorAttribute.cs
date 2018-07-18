/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace Ignia.Topics.Editor.Models.Attributes {

  /*============================================================================================================================
  | CLASS: BOOLEAN EDITOR ATTRIBUTE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a boolean attribute in the Topic Editor.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class BooleanEditorAttribute : EditorAttribute {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditorAttribute"/> class, using the specified key/value pair.
    /// </summary>
    public BooleanEditorAttribute() : base() {
    }

    /*==========================================================================================================================
    | GET VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the value associated with the attribute.
    /// </summary>
    /// <remarks>
    ///   Determines whether the value is checked and, if it is, returns "1"; otherwise returns "0".
    /// </remarks>
    public override string GetValue() => Value.Equals("True")? "1" : "0";

  } // Class

} // Namespace
