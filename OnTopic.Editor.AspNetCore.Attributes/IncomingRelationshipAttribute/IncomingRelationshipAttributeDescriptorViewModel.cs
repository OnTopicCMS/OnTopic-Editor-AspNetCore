/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Attributes.QueryableTopicListAttribute;
using OnTopic.ViewModels;

namespace OnTopic.Editor.AspNetCore.Attributes.IncomingRelationshipAttribute {

  /*============================================================================================================================
  | CLASS: INCOMING RELATIONSHIP ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="IncomingRelationshipViewComponent"/>.
  /// </summary>
  public record IncomingRelationshipAttributeDescriptorViewModel: QueryableTopicListAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | RELATIONSHIP KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally provides the relationship key, which mapes to the <see cref="NamedTopicCollection.Name"/> property.
    ///   Defaults to the <see cref="TopicViewModel.Key"/> if undefined.
    /// </summary>
    public string? RelationshipKey { get; init; }

  } //Class
} //Namespace