﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Data.Transfer.Interchange;

namespace OnTopic.Editor.AspNetCore.Models.Transfer {

  /*============================================================================================================================
  | CLASS: EXPORT VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for soliciting export options from the user.
  /// </summary>
  public record ExportViewModel: EditorViewModel {

    /*==========================================================================================================================
    | EXPORT OPTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="ExportOptions"/> represent the user's preference for how to export the current topic.
    /// </summary>
    public ExportOptions ExportOptions { get; init; } = new() {
      IncludeChildTopics        = true,
      IncludeNestedTopics       = true
    };

  } // Class
} // Namespace