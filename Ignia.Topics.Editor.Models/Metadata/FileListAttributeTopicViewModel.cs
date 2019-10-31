﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

namespace Ignia.Topics.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: FILE LIST ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="FileListViewComponent"/>.
  /// </summary>
  public class FileListAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the the directory path in which to find available files.
    /// </summary>
    public string? Path { get; set; }

    /*==========================================================================================================================
    | PROPERTY: EXTENSION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets which file extension to restrict the list of files.
    /// </summary>
    public string? Extension { get; set; }

    /*==========================================================================================================================
    | PROPERTY: FILTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the filter criteria by which to restrict the list of files.
    /// </summary>
    public string? Filter { get; set; }

    /*==========================================================================================================================
    | PROPERTY: INCLUDE SUBDIRECTORIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether to only include the specified directory, or also include files from all subdirectories.
    /// </summary>
    public bool? IncludeSubdirectories { get; set; }

  } //Class
} //Namespace

#nullable restore