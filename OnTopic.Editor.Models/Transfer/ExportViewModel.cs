/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Data.Transfer.Interchange;

namespace OnTopic.Editor.Models.Transfer {

  /*============================================================================================================================
  | CLASS: EXPORT VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for soliciting export options from the user.
  /// </summary>
  public class ExportViewModel: EditorViewModel {

    /*==========================================================================================================================
    | EXPORT OPTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="ExportOptions"/> represent the user's preference for how to export the current topic.
    /// </summary>
    public ExportOptions ExportOptions { get; set; } = new();

  } // Class
} // Namespace