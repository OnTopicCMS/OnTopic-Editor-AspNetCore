/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using Ignia.Topics.Collections;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: EDITOR VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for interacting with the editor interface, including the established list of
  ///   <see cref="Ignia.Topics.Attribute"/> and their values.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class EditorViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeValue"/> class, using the specified key/value pair.
    /// </summary>
    public EditorViewModel(Topic topic, ContentType contentType, TopicCollection<ContentType> contentTypes) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      Topic = topic;
      ContentType = contentType;
      ContentTypes = contentTypes;

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
    | CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Read-only reference to the current <see cref="ContentType"/> associated with the request.
    /// </summary>
    public ContentType ContentType {
      get;
    }

    /*==========================================================================================================================
    | CONTENT TYPES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Read-only reference to the full list of <see cref="ContentType"/> instances available, in order to be bound to the
    ///   list of new content types available.
    /// </summary>
    public TopicCollection<ContentType> ContentTypes {
      get;
    }

  } //Class

} //Namespace
