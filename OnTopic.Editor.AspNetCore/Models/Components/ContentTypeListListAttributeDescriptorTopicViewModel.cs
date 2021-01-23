/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using OnTopic.Editor.AspNetCore.Components;
using OnTopic.Editor.AspNetCore.Models.Metadata;
using OnTopic.Mapping.Annotations;
using OnTopic.Metadata;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: CONTENT TYPE LIST ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="ContentTypeListViewComponent"/>.
  /// </summary>
  public record ContentTypeListAttributeDescriptorTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: PERMITTED CONTENT TYPES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines which <see cref="ContentType"/>s, if any, are permitted to be created as part of the configured <see
    ///   cref="NestedTopicListAttributeViewComponent"/>.
    /// </summary>
    [Follow(Relationships.None)]
    [Relationship("ContentTypes", Type=RelationshipType.Relationship)]
    public Collection<ContentTypeDescriptorTopicViewModel> PermittedContentTypes { get; } = new();

    /*==============================================================================================================================
    | ENABLE MODAL
    \-----------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   By default, nested topics will open in a modal window. Optionally, this functionality can be disabled.
    /// </summary>
    /// <remarks>
    ///   This is useful in the scenario where a <see cref="ContentTypeDescriptor"/> has multiple levels of attributes implementing
    ///   <see cref="NestedTopicListViewComponent"/>. This is not a recommended configuration, but is occassionally necessary for
    ///   modeling especially complicated scenarios, such as cases where there are multiple dimensions for each topic (e.g.,
    ///   language, device type, &c.). In those cases, nested topics may be the best way to model the capabilities, but having
    ///   multiple levels of modal windows is a poor user experience.
    /// </remarks>
    public bool? EnableModal { get; init; } = true;

  } //Class
} //Namespace

#nullable restore