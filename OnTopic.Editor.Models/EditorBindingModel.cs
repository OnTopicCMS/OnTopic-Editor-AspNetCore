/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Collections;

namespace OnTopic.Editor.Models {

  /*============================================================================================================================
  | CLASS: EDITOR BINDING MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for retrieving data from the editor interface, primarily as a collection of
  ///   <see cref="EditorAttribute"/> instances, via the <see cref="EditorAttributeCollection"/>.
  /// </summary>
  public record EditorBindingModel {

    /*==========================================================================================================================
    | ATTRIBUTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Collection of attribute values extracted from the post.
    /// </summary>
    public EditorAttributeCollection Attributes { get; } = new();

  } // Class
} // Namespace