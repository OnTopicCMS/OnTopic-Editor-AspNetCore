/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Attributes;
using OnTopic.Editor.AspNetCore.Models.ClientResources;
using OnTopic.Mapping.Annotations;

namespace OnTopic.Editor.AspNetCore.Models.Metadata {

  /*============================================================================================================================
  | CLASS: CONTENT TYPE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides core properties from a <see cref="ContentTypeDescriptor"/> to provide to the editor interface. Specifically,
  ///   the <see cref="ContentTypeDescriptorViewModel"/> is critical in providing the schema of attributes to be presented.
  /// </summary>
  public record ContentTypeDescriptorViewModel: EditingTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="ContentTypeDescriptorViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeValueDictionary"/> of attribute values.</param>
    public ContentTypeDescriptorViewModel(AttributeValueDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      Description               = attributes.GetValue(nameof(Description));
      DisableChildTopics        = attributes.GetBoolean(nameof(DisableChildTopics))?? false;
      DisableDelete             = attributes.GetBoolean(nameof(DisableDelete))?? false;
      ImplicitlyPermitted       = attributes.GetBoolean(nameof(ImplicitlyPermitted))?? false;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ContentTypeDescriptorViewModel"/> class.
    /// </summary>
    public ContentTypeDescriptorViewModel(): base() {}

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTE DESCRIPTORS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A list of <see cref="AttributeDescriptorViewModel"/> instances representing each of the <see
    ///   cref="AttributeDescriptor"/> permitted by the underlying <see cref="ContentTypeDescriptor"/>.
    /// </summary>
    [Include(AssociationTypes.Relationships | AssociationTypes.References)]
    public Collection<AttributeDescriptorViewModel> AttributeDescriptors { get; } = new();

    /*==========================================================================================================================
    | PROPERTY: DESCRIPTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a friendly description for the <see cref="ContentTypeDescriptor"/>, intended as documentation for users of
    ///   the OnTopic Editor.
    /// </summary>
    public string? Description { get; init; }

    /*==========================================================================================================================
    | PROPERTY: DISABLE CHILD TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether child topics are permitted to be created under the scope of the represented <see cref="Topic"/>.
    /// </summary>
    public bool DisableChildTopics { get; init; }

    /*==========================================================================================================================
    | PROPERTY: DISABLE DELETE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether a topic is permitted to be deleted via the user interface. This is disabled for certain out-of-the-
    ///   box topics, such as <c>Root</c> and <c>Configuration</c>.
    /// </summary>
    public bool DisableDelete { get; init; }

    /*==========================================================================================================================
    | PROPERTY: PERMITTED CONTENT TYPES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines which <see cref="ContentTypeDescriptor"/>s, if any, are permitted to be created under <see cref="Topic"/>s
    ///   of the current <see cref="ContentTypeDescriptor"/>.
    /// </summary>
    [MapAs(typeof(PermittedContentTypeViewModel))]
    public Collection<PermittedContentTypeViewModel> PermittedContentTypes { get; } = new();

    /*==========================================================================================================================
    | PROPERTY: IMPLICITLY PERMITTED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Content types that are marked as implicitly permitted can be created anywhere. Implicitly permitted content types are
    ///   always superceded by explicitly defined <see cref="PermittedContentTypes"/>.
    /// </summary>
    public bool ImplicitlyPermitted { get; init; }

    /*==========================================================================================================================
    | METHOD: GET DISPLAY GROUPS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves an alphabetized list of display groups associated with this <see cref="ContentTypeDescriptor"/>'s <see
    ///   cref="AttributeDescriptors"/> collection.
    /// </summary>
    public Collection<string> GetDisplayGroups() =>
      new(
        AttributeDescriptors
          .Where(a => !a.IsHidden)
          .Select(a => a.DisplayGroup)
          .Distinct()
          .OrderByDescending(a => a is "Content")
          .ThenBy(a => a is "Settings")
          .ThenBy(a => a is "Navigation")
          .ThenBy(a => a)
          .ToList()
      );

    /*==========================================================================================================================
    | METHOD: GET ATTRIBUTE DESCRIPTORS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a prioritized list of <see cref="AttributeDescriptorViewModel"/> based for a given display group,
    ///   ordered by <see cref="AttributeDescriptorViewModel.SortOrder"/>.
    /// </summary>
    public Collection<AttributeDescriptorViewModel> GetAttributeDescriptors(string displayGroup) =>
      new(AttributeDescriptors
        .Where(a => a.DisplayGroup.Equals(displayGroup, StringComparison.OrdinalIgnoreCase) && !a.IsHidden)
        .OrderBy(a => a.SortOrder).ToList());

    /*==========================================================================================================================
    | METHOD: GET STYLE SHEETS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a collection of <see cref="StyleSheetResource"/>s from the underlying <see cref="AttributeDescriptors"/>
    /// </summary>
    public ReadOnlyCollection<StyleSheetResource> GetStyleSheets() =>
      new(AttributeDescriptors.SelectMany(a => a.StyleSheets.GetResources()).Distinct().ToList());

    /*==========================================================================================================================
    | METHOD: GET SCRIPTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a collection of <see cref="ScriptResource"/>s from the underlying <see cref="AttributeDescriptors"/>
    /// </summary>
    public ReadOnlyCollection<ScriptResource> GetScripts(bool inHeader = false, bool? isDeferred = null) =>
      new(AttributeDescriptors.SelectMany(a => a.Scripts.GetResources(inHeader, isDeferred)).Distinct().ToList());

  } //Class
} //Namespace