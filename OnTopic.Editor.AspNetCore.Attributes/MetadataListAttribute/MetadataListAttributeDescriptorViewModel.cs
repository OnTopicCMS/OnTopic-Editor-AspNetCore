/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Attributes.TopicListAttribute;

namespace OnTopic.Editor.AspNetCore.Attributes.MetadataListAttribute {

  /*============================================================================================================================
  | CLASS: METADATA LIST ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="MetadataListViewComponent"/>.
  /// </summary>
  public record MetadataListAttributeDescriptorViewModel: TopicListAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="MetadataListAttributeDescriptorViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeValueDictionary"/> of attribute values.</param>
    public MetadataListAttributeDescriptorViewModel(AttributeValueDictionary attributes): base(attributes) {
      RelativeTopicPath         = "LookupList";
    }

    /// <summary>
    ///   Initializes a new instance of a <see cref="MetadataListAttributeDescriptorViewModel"/>
    /// </summary>
    public MetadataListAttributeDescriptorViewModel(): base() {
      RelativeTopicPath         = "LookupList";
    }

  } //Class
} //Namespace