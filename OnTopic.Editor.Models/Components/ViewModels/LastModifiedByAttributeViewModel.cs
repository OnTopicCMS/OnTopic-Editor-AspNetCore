﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Metadata;

namespace OnTopic.Editor.Models.Components.ViewModels {

  /*============================================================================================================================
  | CLASS: LAST MODIFIED BY ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="LastModifiedByViewComponent"/>. Additionally provides access to the
  ///   underlying <see cref="LastModifiedByAttributeDescriptorTopicViewModel"/> as well as the instance values for that
  ///   attribute from the currently selected <see cref="Topic"/>.
  /// </summary>
  public record LastModifiedByAttributeViewModel: AttributeViewModel<LastModifiedByAttributeDescriptorTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="LastModifiedByAttributeViewModel"/> class.
    /// </summary>
    public LastModifiedByAttributeViewModel(
      EditingTopicViewModel currentTopic,
      LastModifiedByAttributeDescriptorTopicViewModel attributeDescriptor,
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