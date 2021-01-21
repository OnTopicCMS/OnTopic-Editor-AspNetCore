/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.ViewModels;

#nullable enable

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: TOPIC LIST ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TopicListViewComponent"/>.
  /// </summary>
  public record TopicListAttributeTopicViewModel: QueryableTopicListAttributeTopicViewModel {

    /*==========================================================================================================================
    | DEFAULT LABEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Sets the label name to be used for the placeholder option in the dropdown list, which will have an empty value.
    /// </summary>
    public string? DefaultLabel { get; init; } = "Select a Topic…";

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
    public string? RelativeTopicBase { get; init; }

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
    public string? RelativeTopicPath { get; init; }

    /*==========================================================================================================================
    | VALUE PROPERTY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines which topic attribute to bind the topic list to. By default, it is <see cref="TopicViewModel.Key"/>.
    /// </summary>
    /// <remarks>
    ///   The following are valid values for <see cref="ValueProperty"/>:
    ///   <list type="bullet">
    ///     <item><c>TopicId</c> (for <see cref="TopicViewModel.Id"/>)</item>
    ///     <item><c>Key</c> (for <see cref="TopicViewModel.Key"/>)</item>
    ///     <item><c>UniqueKey</c> (for <see cref="TopicViewModel.UniqueKey"/>)</item>
    ///     <item><c>WebPath</c> (for <see cref="TopicViewModel.WebPath"/>)</item>
    ///     <item><c>Title</c> (for <see cref="TopicViewModel.Title"/>)</item>
    ///   </list>
    /// </remarks>
    public string? ValueProperty { get; init; } = "Key";

  } //Class
} //Namespace

#nullable restore