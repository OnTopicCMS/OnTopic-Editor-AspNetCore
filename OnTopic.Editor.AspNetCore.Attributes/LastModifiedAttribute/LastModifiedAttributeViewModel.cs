/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models;

namespace OnTopic.Editor.AspNetCore.Attributes.LastModifiedAttribute {

  /*============================================================================================================================
  | CLASS: LAST MODIFIED ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="LastModifiedViewComponent"/>. Additionally provides access to the
  ///   underlying <see cref="LastModifiedAttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute
  ///   from the currently selected <see cref="Topic"/>.
  /// </summary>
  public record LastModifiedAttributeViewModel: AttributeViewModel<LastModifiedAttributeDescriptorTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="LastModifiedAttributeViewModel"/> class.
    /// </summary>
    public LastModifiedAttributeViewModel(
      EditingTopicViewModel currentTopic,
      LastModifiedAttributeDescriptorTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) {}

    /*==========================================================================================================================
    | CURRENT VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the current value. Will be set to the newly generated value if unavailable.
    /// </summary>
    public string CurrentValue { get; set; }

  } // Class
} // Namespace