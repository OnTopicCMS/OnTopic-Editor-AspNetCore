/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Metadata;
using OnTopic.Data.Transfer.Interchange;

namespace OnTopic.Editor.Models.Transfer {

  /*============================================================================================================================
  | CLASS: IMPORT VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for soliciting import options from the user.
  /// </summary>
  public class ImportViewModel: EditorViewModel {

    /*==========================================================================================================================
    | IMPORT OPTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="ImportOptions"/> represent the user's preference for how to import a serialized JSON file.
    /// </summary>
    public ImportOptions ImportOptions { get; set; } = new ImportOptions();

    /*==========================================================================================================================
    | IS IMPORTED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="IsImported"/> property is set after the file is successfully imported.
    /// </summary>
    public bool IsImported { get; set; } = false;

  } // Class
} // Namespace