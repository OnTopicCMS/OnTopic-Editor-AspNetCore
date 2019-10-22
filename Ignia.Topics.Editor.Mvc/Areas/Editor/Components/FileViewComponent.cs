/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

using Ignia.Topics.Editor.Models.Components.Options;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: FILE (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   A simple pass-through to <see cref="FileListViewComponent"/>, intended exclusively for backward compatibility.
  /// </summary>
  /// <remarks>
  ///   The <see cref="FileViewComponent"/> was recently renamed to <see cref="FileListViewComponent"/>. Legacy databases,
  ///   however, may still be using the previous name (i.e., <c>File</c>, not <c>FileList</c>). As such, this pass-through is
  ///   being maintained for a short period to aid in the migration.
  /// </remarks>
  [Obsolete("The 'FileViewComponent' is deprecated. Please use 'FileListViewComponent' instead.", false)]
  public class FileViewComponent : FileListViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="HtmlViewComponent"/> with necessary dependencies.
    /// </summary>
    public FileViewComponent(
      ITopicRoutingService topicRoutingService,
      IWebHostEnvironment webHostEnvironment
    ) : base(
      topicRoutingService,
      webHostEnvironment
    ) { }

  } // Class
} // Namespace