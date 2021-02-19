/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models.Components;

namespace OnTopic.Editor.AspNetCore.Attributes.NestedTopicListAttribute {

  /*============================================================================================================================
  | CLASS: NESTED TOPIC LIST ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="NestedTopicListViewComponentView"/>.
  /// </summary>
  public record NestedTopicListAttributeDescriptorViewModel: ContentTypeListAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="NestedTopicListAttributeDescriptorViewModel"/>
    /// </summary>
    public NestedTopicListAttributeDescriptorViewModel() {
      Scripts.Register(GetNamespacedUri("/Shared/Scripts/NestedTopics.js"));
    }

  } //Class
} //Namespace