/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Attributes.TokenizedTopicListAttribute;
using OnTopic.Metadata;

namespace OnTopic.Editor.AspNetCore.Attributes.TopicReferenceAttribute {

  /*============================================================================================================================
  | CLASS: TOPIC REFERENCE ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TopicReferenceViewComponent"/>.
  /// </summary>
  public record TopicReferenceAttributeDescriptorViewModel: TokenizedTopicListAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicReferenceAttributeDescriptorViewModel"/>
    /// </summary>
    public TopicReferenceAttributeDescriptorViewModel(): base() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Restrict token limit
      \-----------------------------------------------------------------------------------------------------------------------*/
      TokenLimit = 1;

    }

  } //Class
} //Namespace