/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.Models.Components.BindingModels {

  /*============================================================================================================================
  | CLASS: FILE LIST ATTRIBUTE (BINDING MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a file editor attribute in the Topic Editor.
  /// </summary>
  public class FileListAttributeBindingModel : AttributeBindingModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="FileListAttributeBindingModel"/> class.
    /// </summary>
    public FileListAttributeBindingModel() : base() {
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
