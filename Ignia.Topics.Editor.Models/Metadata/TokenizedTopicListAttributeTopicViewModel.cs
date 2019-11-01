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
  public class TokenizedTopicListAttributeTopicViewModel: QueryableTopicListAttributeTopicViewModel {

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