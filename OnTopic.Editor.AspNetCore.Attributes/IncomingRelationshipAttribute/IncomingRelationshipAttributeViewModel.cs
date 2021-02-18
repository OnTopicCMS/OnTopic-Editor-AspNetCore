/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.AspNetCore.Models.Metadata;
using OnTopic.ViewModels;

namespace OnTopic.Editor.AspNetCore.Attributes.IncomingRelationshipAttribute {

  /*============================================================================================================================
  | CLASS: INCOMING RELATIONSHIP ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="IncomingRelationshipViewComponent"/>. Additionally provides access to the
  ///   underlying <see cref="AttributeDescriptorViewModel"/> as well as the instance values for that attribute from the
  ///   currently selected <see cref="Topic"/>.
  /// </summary>
  public record IncomingRelationshipAttributeViewModel: AttributeViewModel<IncomingRelationshipAttributeDescriptorViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="IncomingRelationshipAttributeViewModel"/> class.
    /// </summary>
    public IncomingRelationshipAttributeViewModel(
      EditingTopicViewModel currentTopic,
      IncomingRelationshipAttributeDescriptorViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) { }

    /*==========================================================================================================================
    | PROPERTY: RELATED TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of <see cref="Topic"/>s which are related to the current <see cref="Topic"/> based on the specified
    ///   <see cref="NamedTopicCollection.Name"/>.
    /// </summary>
    public Collection<TopicViewModel> RelatedTopics { get; } = new();

  } // Class
} // Namespace