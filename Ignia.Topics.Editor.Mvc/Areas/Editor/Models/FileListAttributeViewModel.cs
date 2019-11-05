/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.Generic;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ignia.Topics.Editor.Mvc.Models {

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

  } // Class

} // Namespace
