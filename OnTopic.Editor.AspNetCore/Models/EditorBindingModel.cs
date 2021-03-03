/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models.Collections;

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: EDITOR BINDING MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for retrieving data from the editor interface, primarily as a collection of <see cref="
  ///   AttributeBindingModel"/> instances, via the <see cref="Attributes"/> property.
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