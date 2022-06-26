/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Components;
using OnTopic.Mapping.Annotations;

namespace OnTopic.Editor.AspNetCore.Models.Components {

  /*============================================================================================================================
  | CLASS: CONTENT TYPE LIST ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="ContentTypeListViewComponent"/>.
  /// </summary>
  public record ContentTypeListAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="ContentTypeListAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public ContentTypeListAttributeDescriptorViewModel(AttributeDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      EnableModal               = attributes.GetBoolean(nameof(EnableModal))?? EnableModal;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ContentTypeListAttributeDescriptorViewModel"/> class.
    /// </summary>
    public ContentTypeListAttributeDescriptorViewModel() : base() { }

    /*==========================================================================================================================
    | PROPERTY: PERMITTED CONTENT TYPES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines which <see cref="ContentTypeDescriptor"/>s, if any, are permitted to be created as part of the configured
    ///   <c>NestedTopicListAttributeViewComponent</c>.
    /// </summary>
    [MapAs(typeof(PermittedContentTypeViewModel))]
    [Collection("ContentTypes", Type=CollectionType.Relationship)]
    public Collection<PermittedContentTypeViewModel> PermittedContentTypes { get; } = new();

    /*==========================================================================================================================
    | ENABLE MODAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   By default, nested topics will open in a modal window. Optionally, this functionality can be disabled.
    /// </summary>
    /// <remarks>
    ///   This is useful in the scenario where a <see cref="ContentTypeDescriptor"/> has multiple levels of attributes
    ///   implementing <c>NestedTopicListViewComponent</c>. This is not a recommended configuration, but is occassionally
    ///   necessary for modeling especially complicated scenarios, such as cases where there are multiple dimensions for each
    ///   topic (e.g., language, device type, etc.). In those cases, nested topics may be the best way to model the capabilities,
    ///   but having multiple levels of modal windows is a poor user experience.
    /// </remarks>
    public bool? EnableModal { get; init; } = true;

  } //Class
} //Namespace