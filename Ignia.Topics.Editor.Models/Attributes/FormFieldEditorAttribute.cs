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
  public class FormFieldEditorAttribute : EditorAttribute {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditorAttribute"/> class, using the specified key/value pair.
    /// </summary>
    public FormFieldEditorAttribute() : base() {
    }

    /*==========================================================================================================================
    | GET VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the value associated with the attribute.
    /// </summary>
    public override string GetValue() => Value;

  } // Class

} // Namespace
