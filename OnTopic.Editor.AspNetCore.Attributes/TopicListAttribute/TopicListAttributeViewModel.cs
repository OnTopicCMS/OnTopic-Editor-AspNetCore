/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTopic.Editor.AspNetCore.Models;

namespace OnTopic.Editor.AspNetCore.Attributes.TopicListAttribute {

  /*============================================================================================================================
  | CLASS: TOPIC LIST (ATTRIBUTE VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Extends the <see cref="AttributeViewModel"/> to include properties that are specific to the topic list view component.
  /// </summary>
  public record TopicListAttributeViewModel: AttributeViewModel<TopicListAttributeDescriptorViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TopicListAttributeViewModel"/> class.
    /// </summary>
    public TopicListAttributeViewModel(
      EditingTopicViewModel currentTopic,
      TopicListAttributeDescriptorViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ) : base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) {}

    /*==========================================================================================================================
    | TOPIC LIST
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of key/value pairs associated with the topic lookup.
    /// </summary>
    /// <remarks>
    ///   The key represents the value that will be persisted to the <see cref="Topic.Attributes"/> collection. The value
    ///   represents the label as it will be displayed in the interface. For instance, for a <see
    ///   cref="TopicListAttributeViewModel"/> representing countries, <c>US</c> might by the key associated with a <c>United
    ///   States</c> value.
    /// </remarks>
    public Collection<SelectListItem> TopicList { get; } = new();

  } // Class
} // Namespace