/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Collections;
using Ignia.Topics.Metadata;
using Ignia.Topics.Repositories;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: EDITOR VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for interacting with the editor interface, including the established list of
  ///   <see cref="Ignia.Topics.Attribute"/> and their values.
  /// </summary>
  public class EditorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeValue"/> class, using the specified key/value pair.
    /// </summary>
    public EditorViewModel(
      Topic topic,
      ContentTypeDescriptor contentTypeDescriptor,
      ReadOnlyTopicCollection<ContentTypeDescriptor> permittedContentTypes,
      ITopicRepository topicRepository,
      bool isModal
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      Topic = topic;
      ContentTypeDescriptor = contentTypeDescriptor;
      PermittedContentTypes = permittedContentTypes;
      TopicRepository = topicRepository;
      IsModal = isModal;

    }

    /*==========================================================================================================================
    | TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Read-only reference to the current topic being edited, for familiar access to the full context.
    /// </summary>
    public Topic Topic {
      get;
    }

    /*==========================================================================================================================
    | CONTENT TYPE DESCRIPTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Read-only reference to the current <see cref="ContentTypeDescriptor"/> associated with the request.
    /// </summary>
    public ContentTypeDescriptor ContentTypeDescriptor {
      get;
    }

    /*==========================================================================================================================
    | PERMITTED CONTENT TYPES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Read-only reference to the full list of <see cref="ContentType"/> instances permitted as child topics.
    /// </summary>
    /// <remarks>
    ///   This is intended to be bound to the list of new content types available in the interface.
    /// </remarks>
    public ReadOnlyTopicCollection<ContentTypeDescriptor> PermittedContentTypes {
      get;
    }

    /*==========================================================================================================================
    | IS MODAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the page should be rendered as a modal (e.g., including the chrome or not).
    /// </summary>
    public bool IsModal {
      get;
    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="ITopicRepository"/> for dynamic access to the entire topic graph.
    /// </summary>
    public ITopicRepository TopicRepository {
      get;
    }

  } // Class

} // Namespace
