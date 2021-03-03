/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.ComponentModel.DataAnnotations;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.AspNetCore.Models.Metadata;

namespace OnTopic.Editor.AspNetCore.Attributes.FilePathAttribute {

  /*============================================================================================================================
  | CLASS: FILE PATH ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="FilePathViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public record FilePathAttributeViewModel: AttributeViewModel<FilePathAttributeDescriptorViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="FilePathAttributeViewModel"/> class.
    /// </summary>
    public FilePathAttributeViewModel(
      EditingTopicViewModel currentTopic,
      FilePathAttributeDescriptorViewModel attributeDescriptor,
      string? value = null,
      string? inheritedValue = null
    ): base(
      currentTopic,
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
    public override string? Value { get; init; }

  } // Class
} // Namespace