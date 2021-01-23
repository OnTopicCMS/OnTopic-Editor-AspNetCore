/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Lookup;

namespace OnTopic.Editor.AspNetCore.Infrastructure {

  /*============================================================================================================================
  | CLASS: EDITOR TOPIC VIEW MODEL LOOKUP SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a mapping between string and class names to be used when mapping <see cref="Topic"/> to a <see
  ///   cref="TopicViewModel"/> or derived class.
  /// </summary>
  public class EditorViewModelLookupService : StaticTypeLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instantiates a new instance of the <see cref="EditorViewModelLookupService"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="EditorViewModelLookupService"/>.</returns>
    public EditorViewModelLookupService() : base() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Add Editor-specific view models
      \-----------------------------------------------------------------------------------------------------------------------*/
      Add(typeof(EditingTopicViewModel));
      Add(typeof(ContentTypeDescriptorTopicViewModel));
      Add(typeof(AttributeDescriptorTopicViewModel));
      Add(typeof(BooleanAttributeDescriptorTopicViewModel));
      Add(typeof(DateTimeAttributeDescriptorTopicViewModel));
      Add(typeof(FileListAttributeDescriptorTopicViewModel));
      Add(typeof(FilePathAttributeDescriptorTopicViewModel));
      Add(typeof(HtmlAttributeDescriptorTopicViewModel));
      Add(typeof(IncomingRelationshipAttributeDescriptorTopicViewModel));
      Add(typeof(InstructionAttributeDescriptorTopicViewModel));
      Add(typeof(LastModifiedAttributeDescriptorTopicViewModel));
      Add(typeof(LastModifiedByAttributeDescriptorTopicViewModel));
      Add(typeof(NestedTopicListAttributeDescriptorTopicViewModel));
      Add(typeof(NumberAttributeDescriptorTopicViewModel));
      Add(typeof(RelationshipAttributeDescriptorTopicViewModel));
      Add(typeof(TextAttributeDescriptorTopicViewModel));
      Add(typeof(TextAreaAttributeDescriptorTopicViewModel));
      Add(typeof(TokenizedTopicListAttributeDescriptorTopicViewModel));
      Add(typeof(TopicListAttributeDescriptorTopicViewModel));
      Add(typeof(TopicReferenceAttributeDescriptorTopicViewModel));

    }

  } //Class
} //Namespace