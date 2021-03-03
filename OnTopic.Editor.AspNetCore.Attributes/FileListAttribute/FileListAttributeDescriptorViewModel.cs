/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models.Metadata;

namespace OnTopic.Editor.AspNetCore.Attributes.FileListAttribute {

  /*============================================================================================================================
  | CLASS: FILE LIST ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="FileListViewComponent"/>.
  /// </summary>
  public record FileListAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | PROPERTY: PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the the directory path in which to find available files.
    /// </summary>
    public string? Path { get; init; }

    /*==========================================================================================================================
    | PROPERTY: EXTENSION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets which file extension to restrict the list of files.
    /// </summary>
    public string? Extension { get; init; }

    /*==========================================================================================================================
    | PROPERTY: FILTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the filter criteria by which to restrict the list of files.
    /// </summary>
    public string? Filter { get; init; }

    /*==========================================================================================================================
    | PROPERTY: INCLUDE SUBDIRECTORIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether to only include the specified directory, or also include files from all subdirectories.
    /// </summary>
    public bool? IncludeSubdirectories { get; init; }

  } //Class
} //Namespace