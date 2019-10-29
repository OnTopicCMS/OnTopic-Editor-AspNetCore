/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Metadata;
using System;

#nullable enable

namespace Ignia.Topics.Editor.Models.Components.Options {

  /*============================================================================================================================
  | CLASS: RELATIONSHIP (OPTIONS)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Establishes options associated with the <see cref="RelationshipViewComponent"/>.
  /// </summary>
  public class RelationshipOptions: DefaultOptions {

    /*==========================================================================================================================
    | PROPERTY: SCOPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a <see cref="Topic.GetUniqueKey"/> path representing the scope of <see cref="Topic"/>s to display to the
    ///   user. This allows relationships to be targeted to particular areas of the topic graph.
    /// </summary>
    public string? Scope { get; set; }

    /*==========================================================================================================================
    | PROPERTY: SHOW ROOT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given the <see cref="Scope"/>, determines whether the root node is displayed, or only the children.  The default is
    ///   false.
    /// </summary>
    public bool? ShowRoot { get; set; }

    /*==========================================================================================================================
    | PROPERTY: CHECK ASCENDANTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   When a <see cref="Topic"/> is selected as a relationship, determines if the client should automatically select all
    ///   descendent topics. The default is false.
    /// </summary>
    public bool? CheckAscendants { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTE NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally defines an attribute name to filter the list of displayed <see cref="Topic"/>s by. Must be accompanied by
    ///   a <see cref="AttributeValue"/>.
    /// </summary>
    /// <remarks>
    ///   If the <see cref="AttributeName"/> and <see cref="AttributeValue"/> are defined, then any <see cref="Topic"/>s listed
    ///   under a <see cref="Topic"/> that is excluded by the filter will <i>also</i> be excluded. As such, this option should
    ///   be used with care.
    /// </remarks>
    public string? AttributeName { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTE VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally defines an attribute value to filter the list of displayed <see cref="Topic"/>s by. Must be accompanied by
    ///   a <see cref="AttributeName"/>.
    /// </summary>
    /// <remarks>
    ///   If the <see cref="AttributeName"/> and <see cref="AttributeValue"/> are defined, then any <see cref="Topic"/>s listed
    ///   under a <see cref="Topic"/> that is excluded by the filter will <i>also</i> be excluded. As such, this option should
    ///   be used with care.
    /// </remarks>
    public string? AttributeValue { get; set; }

    /*==========================================================================================================================
    | PROPERTY: NAMESPACE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the name space to save the relationship under. Any topic may have multiple relationships, so the namespace
    ///   helps distinguish them from one another.
    /// </summary>
    public string? Namespace { get; set; }

  } // Class
} // Namespace

#nullable restore
