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

    /*==========================================================================================================================
    | USE CURRENT CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Specifies that the underlying <see cref="TokenizedTopicListViewComponent"/> should be filtered by the <see cref="Topic
    ///   .ContentType"/> on the current <see cref="Topic"/>.
    /// </summary>
    /// <remarks>
    ///   This is useful for scenarios where the topic reference must target the same <see cref="ContentTypeDescriptor"/> as the
    ///   current <see cref="Topic"/>, as is the case on <see cref="Topic.BaseTopic"/>.
    /// </remarks>
    public bool UseCurrentContentType { get; init; }

  } //Class
} //Namespace