/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Metadata;
using System;

namespace Ignia.Topics.Editor.Models.Components.ViewModels {

  /*============================================================================================================================
  | CLASS: DATE/TIME  ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="DateTimeViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public class DateTimeAttributeViewModel: AttributeViewModel<DateTimeAttributeTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="DateTimeSelectorAttributeViewModel"/> class.
    /// </summary>
    public DateTimeAttributeViewModel(
      DateTimeAttributeTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
      attributeDescriptor,
      value,
      inheritedValue
    ) {}

  } // Class

} // Namespace
