/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Mapping.Annotations;
using OnTopic.Models;

namespace OnTopic.Editor.AspNetCore.Models.Metadata {

  /*============================================================================================================================
  | CLASS: PERMITTED CONTENT TYPE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides core properties from a <see cref="ContentTypeDescriptor"/> to provide to the editor interface. Specifically,
  ///   the <see cref="ContentTypeDescriptorViewModel"/> is critical in providing the schema of attributes to be presented.
  /// </summary>
  public record PermittedContentTypeViewModel {

    /*==========================================================================================================================
    | PROPERTY: KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="ICoreTopicViewModel.Key"/>
    [Include(AssociationTypes.Relationships | AssociationTypes.References)]
    public string Key { get; init; } = default!;

    /*==========================================================================================================================
    | PROPERTY: TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc cref="ITopicViewModel.Title"/>
    public string? Title { get; init; }

  } //Class
} //Namespace