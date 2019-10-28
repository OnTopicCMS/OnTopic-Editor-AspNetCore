/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.Generic;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components.Options;
using Ignia.Topics.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ignia.Topics.Editor.Mvc.Models {

  /*============================================================================================================================
  | CLASS: TOPIC LOOKUP (ATTRIBUTE VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Extends the <see cref="AttributeViewModel"/> to include properties that are specific to the topic lookup view component.
  /// </summary>
  public class TopicLookupAttributeViewModel: AttributeViewModel<TopicLookupOptions> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TopicLookupAttributeViewModel"/> class.
    /// </summary>
    public TopicLookupAttributeViewModel(
      AttributeDescriptorTopicViewModel attributeDescriptor,
      TopicLookupOptions options,
      string value = null,
      string inheritedValue = null
    ) : base(
      attributeDescriptor,
      options,
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
    ///   cref="TopicLookupAttributeViewModel"/> representing countries, <c>US</c> might by the key associated with a <c>United
    ///   States</c> value.
    /// </remarks>
    public List<SelectListItem> TopicList { get; } = new List<SelectListItem>();

  } // Class

} // Namespace
