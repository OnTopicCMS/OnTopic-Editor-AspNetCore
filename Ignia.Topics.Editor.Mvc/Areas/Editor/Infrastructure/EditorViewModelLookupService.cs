/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Metadata;

namespace Ignia.Topics.Editor.Mvc.Infrastructure {

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
      Add(typeof(LastModifiedAttributeTopicViewModel));
      Add(typeof(NestedTopicListAttributeTopicViewModel));
      Add(typeof(NumberAttributeTopicViewModel));
      Add(typeof(RelationshipAttributeTopicViewModel));
      Add(typeof(TextAttributeTopicViewModel));
      Add(typeof(TokenizedTopicListAttributeTopicViewModel));
      Add(typeof(TopicListAttributeTopicViewModel));
      Add(typeof(TopicReferenceAttributeTopicViewModel));

    }

  } //Class
} //Namespace
