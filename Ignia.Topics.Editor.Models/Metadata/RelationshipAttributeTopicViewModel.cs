/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

using Ignia.Topics.Mapping;
using Ignia.Topics.ViewModels;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Ignia.Topics.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: RELATIONSHIP ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="RelationshipViewComponent"/>.
  /// </summary>
  public class RelationshipAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: ROOT TOPIC KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a <see cref="Topic.GetUniqueKey"/> path representing the <see cref="RootTopic"/> to display to the
    ///   user. This allows relationships to be targeted to particular areas of the topic graph.
    /// </summary>
    [Obsolete(
      "This property is exposed exlusively for backward compatibility with the DefaultConfiguration's Scope property. New " +
      "attributes should instead use the RootTopic property. The RootTopicKey property will be removed in the future.",
      false
    )]
    public string? RootTopicKey { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ROOT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a <see cref="Topic.Id"/> representing the scope of <see cref="Topic"/>s to display to the user. This
    ///   allows relationships to be targeted to particular areas of the topic graph.
    /// </summary>
    [AttributeKey("RootTopicId")]
    [NotNull]
    public TopicViewModel? RootTopic { get; set; }

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
    | PROPERTY: ATTRIBUTE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally defines an attribute name to filter the list of displayed <see cref="Topic"/>s by. Must be accompanied by
    ///   a <see cref="AttributeValue"/>.
    /// </summary>
    /// <remarks>
    ///   If the <see cref="AttributeKey"/> and <see cref="AttributeValue"/> are defined, then any <see cref="Topic"/>s listed
    ///   under a <see cref="Topic"/> that is excluded by the filter will <i>also</i> be excluded. As such, this option should
    ///   be used with care.
    /// </remarks>
    public string? AttributeKey { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTE VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Optionally defines an attribute value to filter the list of displayed <see cref="Topic"/>s by. Must be accompanied by
    ///   a <see cref="AttributeKey"/>.
    /// </summary>
    /// <remarks>
    ///   If the <see cref="AttributeKey"/> and <see cref="AttributeValue"/> are defined, then any <see cref="Topic"/>s listed
    ///   under a <see cref="Topic"/> that is excluded by the filter will <i>also</i> be excluded. As such, this option should
    ///   be used with care.
    /// </remarks>
    public string? AttributeValue { get; set; }

  } //Class
} //Namespace

#nullable restore