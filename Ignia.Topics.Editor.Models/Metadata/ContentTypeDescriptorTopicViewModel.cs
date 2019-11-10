/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.Generic;
using System.Linq;
using Ignia.Topics.Mapping;
using Ignia.Topics.Metadata;

namespace Ignia.Topics.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: CONTENT TYPE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides core properties from a <see cref="ContentTypeDescriptor"/> to provide to the editor interface. Specifically,
  ///   the <see cref="ContentTypeDescriptorTopicViewModel"/> is critical in providing the schema of attributes to be presented.
  /// </summary>
  public class ContentTypeDescriptorTopicViewModel: EditingTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="ContentTypeDescriptorTopicViewModel"/> class.
    /// </summary>
    public ContentTypeDescriptorTopicViewModel(): base() {}

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTE DESCRIPTORS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   A list of <see cref="AttributeDescriptorTopicViewModel"/> instances representing each of the <see
    ///   cref="AttributeDescriptor"/> permitted by the underlying <see cref="ContentTypeDescriptor"/>.
    /// </summary>
    [Follow(Relationships.Relationships|Relationships.References)]
    public List<AttributeDescriptorTopicViewModel> AttributeDescriptors { get; } = new List<AttributeDescriptorTopicViewModel>();

    /*==========================================================================================================================
    | PROPERTY: DESCRIPTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a friendly description for the <see cref="ContentType"/>, intended as documentation for users of the editor.
    /// </summary>
    public string Description { get; set; }

    /*==========================================================================================================================
    | PROPERTY: DISABLE CHILD TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether child topics are permitted to be created under the scope of the represented <see cref="Topic"/>.
    /// </summary>
    public bool DisableChildTopics { get; set; }

    /*==========================================================================================================================
    | PROPERTY: DISABLE DELETE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether a topic is permitted to be deleted via the user interface. This is disabled for certain out-of-the-
    ///   box topics, such as <c>Root</c> and <c>Configuration</c>.
    /// </summary>
    public bool DisableDelete { get; set; }

    /*==========================================================================================================================
    | PROPERTY: PERMITTED CONTENT TYPES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines which <see cref="ContentType"/>s, if any, are permitted to be created under <see cref="Topic"/>s of the
    ///   current <see cref="ContentType"/>.
    /// </summary>
    public List<ContentTypeDescriptorTopicViewModel> PermittedContentTypes { get; set; }

    /*==========================================================================================================================
    | METHOD: GET DISPLAY GROUPS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves an alphabetized list of display groups associated with this <see cref="ContentTypeDescriptor"/>'s <see
    ///   cref="AttributeDescriptors"/> collection.
    /// </summary>
    public List<string> GetDisplayGroups() =>
      AttributeDescriptors.Where(a => !a.IsHidden).Select(a => a.DisplayGroup).Distinct().OrderBy(a => a).ToList();

    /*==========================================================================================================================
    | METHOD: GET ATTRIBUTE DESCRIPTORS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a prioritized list of <see cref="AttributeDescriptorTopicViewModel"/> based for a given display group,
    ///   ordered by <see cref="AttributeDescriptorTopicViewModel.SortOrder"/>.
    /// </summary>
    public List<AttributeDescriptorTopicViewModel> GetAttributeDescriptors(string displayGroup) =>
      AttributeDescriptors.Where(a => a.DisplayGroup.Equals(displayGroup) && !a.IsHidden).OrderBy(a => a.SortOrder).ToList();

  } //Class
} //Namespace