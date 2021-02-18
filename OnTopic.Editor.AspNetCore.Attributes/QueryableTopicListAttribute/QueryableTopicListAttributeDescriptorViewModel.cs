/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Diagnostics.CodeAnalysis;
using OnTopic.Editor.AspNetCore.Models.Metadata;
using OnTopic.ViewModels;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.QueryableTopicListAttribute {

  /*============================================================================================================================
  | CLASS: QUERYABLE TOPIC LIST (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a base class for exposing properties common to the <see cref="RelationshipAttributeDescriptorViewModel"/>, <see
  ///   cref="TopicListAttributeDescriptorViewModel"/>, <see cref="TokenizedTopicListAttributeDescriptorViewModel"/>, and any
  ///   other view components that allow querying of topics via the search API, or similar techniques.
  /// </summary>
  public record QueryableTopicListAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | PROPERTY: ROOT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a <see cref="Topic.Id"/> representing the scope of <see cref="Topic"/>s to display to the user. This
    ///   allows relationships to be targeted to particular areas of the topic graph.
    /// </summary>
    [NotNull]
    public TopicViewModel? RootTopic { get; init; }

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
    public string? AttributeKey { get; init; }

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
    public string? AttributeValue { get; init; }

  } //Class
} //Namespace

#nullable restore