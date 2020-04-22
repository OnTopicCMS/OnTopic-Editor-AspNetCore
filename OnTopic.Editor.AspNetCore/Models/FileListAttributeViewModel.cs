/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: FILE ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="FileViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public class FileListAttributeViewModel: AttributeViewModel<FileListAttributeTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="FileAttributeViewModel"/> class.
    /// </summary>
    public FileListAttributeViewModel(
      EditingTopicViewModel currentTopic,
      FileListAttributeTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
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
    public List<SelectListItem> Files { get; set; } = new List<SelectListItem>();

    /*==========================================================================================================================
    | ABSOLUTE PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the absolute path where files are being displayed from.
    /// </summary>
    /// <remarks>
    ///   While the <i>relative</i> file path can be retrieved from <see cref="FileListAttributeTopicViewModel.Path"/>, that
    ///   doesn't include the base path of the web application. The <see cref="AbsolutePath"/> addresses this.
    /// </remarks>
    public string AbsolutePath { get; set; }

  } // Class
} // Namespace