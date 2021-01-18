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
  public class EditorViewModelLookupService : ViewModels.TopicViewModelLookupService {

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
      Add(typeof(BooleanAttributeTopicViewModel));
      Add(typeof(DateTimeAttributeTopicViewModel));
      Add(typeof(FileListAttributeTopicViewModel));
      Add(typeof(FilePathAttributeTopicViewModel));
      Add(typeof(HtmlAttributeTopicViewModel));
      Add(typeof(IncomingRelationshipAttributeTopicViewModel));
      Add(typeof(InstructionAttributeTopicViewModel));
      Add(typeof(LastModifiedAttributeTopicViewModel));
      Add(typeof(LastModifiedByAttributeTopicViewModel));
      Add(typeof(NestedTopicListAttributeTopicViewModel));
      Add(typeof(NumberAttributeTopicViewModel));
      Add(typeof(RelationshipAttributeTopicViewModel));
      Add(typeof(TextAttributeTopicViewModel));
      Add(typeof(TextAreaAttributeTopicViewModel));
      Add(typeof(TokenizedTopicListAttributeTopicViewModel));
      Add(typeof(TopicListAttributeTopicViewModel));
      Add(typeof(TopicReferenceAttributeTopicViewModel));

    }

  } //Class
} //Namespace