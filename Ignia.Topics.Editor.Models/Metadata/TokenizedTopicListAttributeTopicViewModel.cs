/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Mapping;
using Ignia.Topics.ViewModels;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable

namespace Ignia.Topics.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: TOKENIZED TOPIC LIST ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TokenizedTopicListViewComponent"/>.
  /// </summary>
  public class TokenizedTopicListAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: ROOT TOPIC KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a <see cref="Topic.GetUniqueKey"/> path representing the <see cref="RootTopic"/> to display to the
    ///   user. This allows the <see cref="TokenizedTopicListViewComponent"/> to be limited to particular areas of the topic
    ///   graph.
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
    ///   allows <see cref="TokenizedTopicListViewComponent"/> to be limited to particular areas of the topic graph.
    /// </summary>
    [AttributeKey("RootTopicId")]
    [NotNull]
    public TopicViewModel? RootTopic { get; set; }

    /*==========================================================================================================================
    | ATTRIBUTE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the key of an attribute (e.g., <c>ContentType</c>) to filter the selectable token list by. If <see
    ///   cref="AttributeKey"/> is defined, then <see cref="AttributeValue"/> should also be defined; otherwise, it will filter
    ///   by topics that have an empty value for the specified <see cref="AttributeKey"/>.
    /// </summary>
    public string? AttributeKey { get; set; }

    /*==========================================================================================================================
    | ATTRIBUTE VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the value of an attribute (e.g., <c>Page</c>) to filter the selectable token list by. If <see
    ///   cref="AttributeValue"/> is defined, then <see cref="AttributeKey"/> should also be defined; otherwise, the filter
    ///   will not function.
    /// </summary>
    public string? AttributeValue { get; set; }

    /*==========================================================================================================================
    | RESULT LIMIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the maximum number of <see cref="Topic"/> results to pull from the web service.
    /// </summary>
    public int? ResultLimit { get; set; }

    /*==========================================================================================================================
    | TOKEN LIMIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the maximum number of tokens allowed to be selected by the user. Maps to TokenInput's <c>tokenLimit</c>
    ///   setting.
    /// </summary>
    /// <remarks>
    ///   This is especially useful if an attribute should be limited to a single entry, such as a topic reference, but it is
    ///   preferred to display a searchable list rather than a predefined dropdown list using <see
    ///   cref="TopicLookupViewComponent"/>.
    /// </remarks>
    public int? TokenLimit { get; set; }

    /*==========================================================================================================================
    | AUTO POST BACK?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the form should automatically be submitted whenever a new value is selected. This is useful, in
    ///   particular, for the <see cref="TopicPointerViewComponent"/>, which provides a purpose-built wrapper for the <see
    ///   cref="TokenizedTopicViewComponent"/>.
    /// </summary>
    public bool? AutoPostBack { get; set; }

    /*==========================================================================================================================
    | AS RELATIONSHIP
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether to utilize the control as a replacement for (or equivalent to) <see
    ///   cref="RelationshipsViewComponent"/>.
    /// </summary>
    /// <remarks>
    ///   Relationships are modeled using <see cref="Topic.Relationships"/> instead of <see cref="Topic.Attributes"/>, and most
    ///   implementations of <see cref="ITopicRepository"/> will likely store relationships separate from attributes.
    /// </remarks>
    public bool? AsRelationship { get; set; }

  } //Class
} //Namespace

#nullable restore