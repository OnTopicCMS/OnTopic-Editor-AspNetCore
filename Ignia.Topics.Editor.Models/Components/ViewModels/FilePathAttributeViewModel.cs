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
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     string                          _defaultDate                    = null;
    private                     string                          _defaultTime                    = null;

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

    /*==========================================================================================================================
    | VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdocs />
    [RegularExpression(
      @"^([A-Za-z]+:)?(\/{0,2}|\\{0,2})(?:[0-9a-zA-Z _\-\.]+(\/|\\?))+$",
      ErrorMessage = "The image path specified is not a valid file path."
    )]
    public override string Value { get; set; }

  } // Class

} // Namespace
