/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Collections.Specialized;
using OnTopic.Editor.AspNetCore.Attributes.QueryableTopicListAttribute;
using OnTopic.Models;

namespace OnTopic.Editor.AspNetCore.Attributes.IncomingRelationshipAttribute {

  /*============================================================================================================================
  | CLASS: INCOMING RELATIONSHIP ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="IncomingRelationshipViewComponent"/>.
  /// </summary>
  public record IncomingRelationshipAttributeDescriptorViewModel: QueryableTopicListAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="IncomingRelationshipAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeValueDictionary"/> of attribute values.</param>
    public IncomingRelationshipAttributeDescriptorViewModel(AttributeValueDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      RelationshipKey           = attributes.GetValue(nameof(RelationshipKey));
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="IncomingRelationshipAttributeDescriptorViewModel"/> class.
    /// </summary>
    public IncomingRelationshipAttributeDescriptorViewModel() : base() { }

    /*==========================================================================================================================
    | RELATIONSHIP KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally provides the relationship key, which maps to the <see cref="KeyValuesPair{TKey, TValue}.Key"/> property.
    ///   Defaults to the <see cref="ICoreTopicViewModel.Key"/> if undefined.
    /// </summary>
    public string? RelationshipKey { get; init; }

  } //Class
} //Namespace