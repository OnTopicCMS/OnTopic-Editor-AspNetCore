/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models.Metadata;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ignia.Topics.Editor.Models.Components.ViewModels {

  /*============================================================================================================================
  | CLASS: FILE PATH ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="FilePathViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public class FilePathAttributeViewModel: AttributeViewModel<FilePathAttributeTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="FilePathAttributeViewModel"/> class.
    /// </summary>
    public FilePathAttributeViewModel(
      FilePathAttributeTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
      attributeDescriptor,
      value,
      inheritedValue
    ) { }

  } // Class

} // Namespace
