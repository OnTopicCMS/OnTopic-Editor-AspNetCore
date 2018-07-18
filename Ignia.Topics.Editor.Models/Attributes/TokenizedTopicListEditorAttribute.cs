/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace Ignia.Topics.Editor.Models.Attributes {

  /*============================================================================================================================
  | CLASS: TOKENIZED TOPIC LIST EDITOR ATTRIBUTE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a tokenized topic list attribute in the Topic Editor.
  /// </summary>
  public class TokenizedTopicListEditorAttribute : EditorAttribute {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditorAttribute"/> class, using the specified key/value pair.
    /// </summary>
    public TokenizedTopicListEditorAttribute() : base() {
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
