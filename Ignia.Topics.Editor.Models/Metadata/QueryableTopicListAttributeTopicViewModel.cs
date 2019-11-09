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
  | CLASS: QUERYABLE TOPIC LIST (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a base class for exposing properties common to the <see cref="RelationshipAttributeTopicViewModel"/>, <see
  ///   cref="TopicListAttributeTopicViewModel"/>, <see cref="TokenizedTopicListAttributeTopicViewModel"/>, and any other view
  ///   components that allow querying of topics via the search API, or similar techniques.
  /// </summary>
  public class QueryableTopicListAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

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
    | PROPERTY: RELATIVE TOPIC BASE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the token representing where the base path should be evaluated from.
    /// </summary>
    /// <remarks>
    ///   Generally, the preferred way of setting the scope is to use the <see cref="RootTopic"/>. There are times, however,
    ///   that the target list is not fixed in the topic graph, but is instead relative to either the specific <see
    ///   cref="Topic"/> or its <see cref="ContentTypeDescriptor"/>. In these cases, the editor may be configured to use a
    ///   relative key (e.g., <c>CurrentTopic</c>, <c>ParentTopic</c>, <c>ContentTypeDescriptor</c>) as the base. This can then
    ///   be used in conjunction with <see cref="RelativeTopicPath"/> to set the place to look within that base, if appropriate.
    ///   For example, a <see cref="RelativeTopicBase"/> setting of <c>ContentTypeDescriptor</c> may be combined with a <see
    ///   cref="RelativeTopicPath"/> of <c>Views</c> to look within a <see cref="NestedTopicListViewComponent"/> named "Views"
    ///   for the list of target topics.
    /// </remarks>
    public string? RelativeTopicBase { get; set; }

    /*==========================================================================================================================
    | PROPERTY: RELATIVE TOPIC PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the token representing where within the <see cref="RelativeTopicBase"/> the topics should be retrieved.
    /// </summary>
    /// <remarks>
    ///   The <see cref="RelativeTopicPath"/> is used in conjunction with the <see cref="RelativeTopicBase"/>. If set, these
    ///   two properties will supercede any value that <see cref="RootTopic"/> is set to.
    /// </remarks>
    public string? RelativeTopicPath { get; set; }

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