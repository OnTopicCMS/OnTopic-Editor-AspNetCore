/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.AspNetCore.Models.Metadata;

namespace OnTopic.Editor.AspNetCore.Attributes.FileListAttribute {

  /*============================================================================================================================
  | CLASS: FILE ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="FileListViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorViewModel"/> as well as the instance values for that attribute from the currently selected
  ///   <see cref="Topic"/>.
  /// </summary>
  public record FileListAttributeViewModel: AttributeViewModel<FileListAttributeDescriptorViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="FileListViewComponent"/> class.
    /// </summary>
    public FileListAttributeViewModel(
      EditingTopicViewModel currentTopic,
      FileListAttributeDescriptorViewModel attributeDescriptor,
      string? value = null,
      string? inheritedValue = null
    ) : base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) {}

    /*==========================================================================================================================
    | FILES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of files associated with the lookup.
    /// </summary>
    /// <remarks>
    ///   The <see cref="SelectListItem.Value"/>represents the path that will be saved to the database; the <see
    ///   cref="SelectListItem.Text"/> represents simply the file name itself.
    /// </remarks>
    public Collection<SelectListItem> Files { get; } = new();

    /*==========================================================================================================================
    | ABSOLUTE PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the absolute path where files are being displayed from.
    /// </summary>
    /// <remarks>
    ///   While the <i>relative</i> file path can be retrieved from <see cref="FileListAttributeDescriptorViewModel.Path"
    ///   />, that doesn't include the base path of the web application. The <see cref="AbsolutePath"/> addresses this.
    /// </remarks>
    [Required, NotNull, DisallowNull]
    public string? AbsolutePath { get; set; }

  } // Class
} // Namespace