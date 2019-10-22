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
  | CLASS: TOKENIZED TOPIC LIST (OPTIONS)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Establishes options associated with the <see cref="TokenizedTopicListViewComponent"/>.
  /// </summary>
  public class TokenizedTopicListOptions: DefaultOptions {

    /*==========================================================================================================================
    | SCOPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the scope of the topic graph within which to search for results. E.g., <c>Root:Web:Configuration</c>.
    /// </summary>
    public string? Scope { get; set; }

    /*==========================================================================================================================
    | ATTRIBUTE NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the name of an attribute (e.g., <c>ContentType</c>) to filter the selectable token list by. If <see
    ///   cref="AttributeName"/> is defined, then <see cref="AttributeValue"/> should also be defined; otherwise, it will filter
    ///   by topics that have an empty value for the specified <see cref="AttributeName"/>.
    /// </summary>
    public string? AttributeName { get; set; }

    /*==========================================================================================================================
    | ATTRIBUTE VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the value of an attribute (e.g., <c>Page</c>) to filter the selectable token list by. If <see
    ///   cref="AttributeValue"/> is defined, then <see cref="AttributeName"/> should also be defined; otherwise, the filter
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
    | IS AUTO POSTBACK?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the form should automatically be submitted whenever a new value is selected. This is useful, in
    ///   particular, for the <see cref="TopicPointerViewComponent"/>, which provides a purpose-built wrapper for the <see
    ///   cref="TokenizedTopicViewComponent"/>.
    /// </summary>
    public bool? IsAutoPostBack { get; set; }

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

    /*==========================================================================================================================
    | SEARCH PROPERTY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines what attribute in <see cref="JsonTopicViewModel"/> to search against. This corresponds to TokenInput's
    ///   <c>propertyToSearch</c> setting.
    /// </summary>
    /// <remarks>
    ///   Keep in mind that this is looking at the serialized object graph of the topic tree, not the actual topic graph
    ///   itself. As a result, this will not have access to <i>all</i> attributes, only those explicitly included on the
    ///   <see cref="JsonTopicViewModel"/>. Further, because this is looking at the <c>serialized</c> version, the attribute
    ///   names may vary from <see cref="JsonTopicViewModel"/>'s property names; for example, they will be camel-cased, and
    ///   may be modified by serialization annotations.
    /// </remarks>
    public string? SearchProperty { get; set; }

    /*==========================================================================================================================
    | QUERY PARAMETER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the querystring parameter used to query the web service. This corresponds to TokenInput's <c>queryParam</c>
    ///   setting. Defaults to <c>AttributeValue</c>.
    /// </summary>
    public string? QueryParameter { get; set; }

  } // Class
} // Namespace

#nullable restore
