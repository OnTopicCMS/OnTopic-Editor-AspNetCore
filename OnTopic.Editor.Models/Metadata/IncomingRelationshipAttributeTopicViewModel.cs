/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Collections;
using OnTopic.ViewModels;

#nullable enable

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: INCOMING RELATIONSHIP ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="IncomingRelationshipViewComponent"/>.
  /// </summary>
  public class IncomingRelationshipAttributeTopicViewModel: QueryableTopicListAttributeTopicViewModel {

    /*==========================================================================================================================
    | RELATIONSHIP KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally provides the relationship key, which mapes to the <see cref="NamedTopicCollection.Name"/> property.
    ///   Defaults to the <see cref="TopicViewModel.Key"/> if undefined.
    /// </summary>
    public string? RelationshipKey { get; set; }

  } //Class
} //Namespace

#nullable restore