/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Metadata;
using OnTopic.Editor.AspNetCore.Attributes.QueryableTopicListAttribute;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.TokenizedTopicListAttribute {

  /*============================================================================================================================
  | CLASS: TOKENIZED TOPIC LIST ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TokenizedTopicListViewComponent"/>.
  /// </summary>
  public record TokenizedTopicListAttributeDescriptorTopicViewModel: QueryableTopicListAttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: MODEL TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <inheritdoc />
    public override ModelType ModelType {
      get => (AsRelationship ?? false) ? ModelType.Relationship : ModelType.ScalarValue;
      init {}
    }

    /*==========================================================================================================================
    | RESULT LIMIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the maximum number of <see cref="Topic"/> results to pull from the web service.
    /// </summary>
    public int? ResultLimit { get; init; } = 100;

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
    public int? TokenLimit { get; init; } = 100;

    /*==========================================================================================================================
    | AUTO POST BACK?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the form should automatically be submitted whenever a new value is selected. This is useful, in
    ///   particular, for the <see cref="TopicPointerViewComponent"/>, which provides a purpose-built wrapper for the <see
    ///   cref="TokenizedTopicViewComponent"/>.
    /// </summary>
    public bool? AutoPostBack { get; init; }

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
    public bool? AsRelationship { get; init; }

  } //Class
} //Namespace

#nullable restore