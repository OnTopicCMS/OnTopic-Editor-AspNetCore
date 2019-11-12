/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Collections;
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Metadata;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: EDITOR VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for interacting with the editor interface.
  /// </summary>
  public class EditorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditorViewModel"/> class.
    /// </summary>
    public EditorViewModel(
      EditingTopicViewModel topic,
      ContentTypeDescriptorTopicViewModel contentTypeDescriptor,
      bool isNew,
      bool isModal
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      Topic                     = topic;
      ContentTypeDescriptor     = contentTypeDescriptor;
      IsNew                     = isNew;
      IsModal                   = isModal;

    }

    /*==========================================================================================================================
    | TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="EditingTopicViewModel"/> representing the core properties of the currently selected <see
    ///   cref="Topic"/>.
    /// </summary>
    public EditingTopicViewModel Topic {
      get;
    }

    /*==========================================================================================================================
    | CONTENT TYPE DESCRIPTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="ContentTypeDescriptorTopicViewModel"/> representing the core properties of the <see cref="Topic"/>'s
    ///   <see cref="ContentTypeDescriptor"/>.
    /// </summary>
    public ContentTypeDescriptorTopicViewModel ContentTypeDescriptor {
      get;
    }

    /*==========================================================================================================================
    | IS MODAL?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the page should be rendered as a modal (e.g., including the chrome or not).
    /// </summary>
    public bool IsModal {
      get;
    }

    /*==========================================================================================================================
    | IS NEW?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the page is being newly created.
    /// </summary>
    public bool IsNew {
      get;
    }

  } // Class
} // Namespace
