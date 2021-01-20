/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: RELATIONSHIP ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="RelationshipViewComponent"/>.
  /// </summary>
  public record RelationshipAttributeTopicViewModel: QueryableTopicListAttributeTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: SHOW ROOT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given the <see cref="Scope"/>, determines whether the root node is displayed, or only the children.  The default is
    ///   false.
    /// </summary>
    public bool? ShowRoot { get; init; }

    /*==========================================================================================================================
    | PROPERTY: EXPAND RELATED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the tree panel should be expanded to ensure visibility of any related (checked)
    ///   relationships upon load. Defaults to <c>true</c>.
    /// </summary>
    public bool? ExpandRelated { get; init; }

    /*==========================================================================================================================
    | PROPERTY: CHECK ASCENDANTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   When a <see cref="Topic"/> is selected as a relationship, determines if the client should automatically select all
    ///   descendent topics. The default is false.
    /// </summary>
    public bool? CheckAscendants { get; init; }

  } //Class
} //Namespace

#nullable restore