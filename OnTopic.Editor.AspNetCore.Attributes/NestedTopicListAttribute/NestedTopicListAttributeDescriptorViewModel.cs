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
  ///   Provides access to attributes associated with the <see cref="NestedTopicListViewComponent"/>.
  /// </summary>
  public record NestedTopicListAttributeDescriptorViewModel: ContentTypeListAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="NestedTopicListAttributeDescriptorViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeValueDictionary"/> of attribute values.</param>
    public NestedTopicListAttributeDescriptorViewModel(AttributeValueDictionary attributes): base(attributes) {
      RegisterResources();
    }

    /// <summary>
    ///   Initializes a new instance of a <see cref="NestedTopicListAttributeDescriptorViewModel"/>
    /// </summary>
    public NestedTopicListAttributeDescriptorViewModel() {
      RegisterResources();
    }

    /*==========================================================================================================================
    | REGISTER RESOURCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Derived classes may optionally override this method in order to register resources, as an alternative to setting these
    ///   in the constructor.
    /// </summary>
    protected void RegisterResources() {
      Scripts.Register(GetNamespacedUri("/Shared/Scripts/NestedTopics.js"));
    }

  } //Class
} //Namespace