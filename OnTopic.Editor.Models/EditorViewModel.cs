/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Metadata;

namespace OnTopic.Editor.Models {

  /*============================================================================================================================
  | CLASS: EDITOR VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for interacting with the editor interface.
  /// </summary>
  public class EditorViewModel {

    /*==========================================================================================================================
    | TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="EditingTopicViewModel"/> representing the core properties of the currently selected <see
    ///   cref="Topic"/>.
    /// </summary>
    public EditingTopicViewModel Topic { get; set; }

    /*==========================================================================================================================
    | CONTENT TYPE DESCRIPTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="ContentTypeDescriptorTopicViewModel"/> representing the core properties of the <see cref="Topic"/>'s
    ///   <see cref="ContentTypeDescriptor"/>.
    /// </summary>
    public ContentTypeDescriptorTopicViewModel ContentTypeDescriptor { get; set; }

    /*==========================================================================================================================
    | IS MODAL?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the page should be rendered as a modal (e.g., including the chrome or not).
    /// </summary>
    public bool IsModal { get; set; }

    /*==========================================================================================================================
    | IS NEW?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the page is being newly created.
    /// </summary>
    public bool IsNew { get; set; }

  } // Class
} // Namespace