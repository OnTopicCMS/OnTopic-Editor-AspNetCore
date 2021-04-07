/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models.Metadata;

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: EDITOR VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for interacting with the editor interface.
  /// </summary>
  public record EditorViewModel {

    /*==========================================================================================================================
    | TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="EditingTopicViewModel"/> representing the core properties of the currently selected <see
    ///   cref="Topic"/>.
    /// </summary>
    public EditingTopicViewModel Topic { get; init; } = default!;

    /*==========================================================================================================================
    | CONTENT TYPE DESCRIPTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="ContentTypeDescriptorViewModel"/> representing the core properties of the <see cref="Topic"/>'s <see
    ///   cref="ContentTypeDescriptor"/>.
    /// </summary>
    public ContentTypeDescriptorViewModel ContentTypeDescriptor { get; init; } = default!;

    /*==========================================================================================================================
    | IS MODAL?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the page should be rendered as a modal (e.g., including the chrome or not).
    /// </summary>
    public bool IsModal { get; init; }

    /*==========================================================================================================================
    | IS NEW?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the page is being newly created.
    /// </summary>
    public bool IsNew { get; init; }

    /*==========================================================================================================================
    | IS FULLY LOADED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the <see cref="Topic.References"/> and <see cref="Topic.Relationships"/> are both <c>IsFullyLoaded</c>.
    ///   If not, a warning should be presented.
    /// </summary>
    public bool IsFullyLoaded { get; init; } = true;

  } // Class
} // Namespace