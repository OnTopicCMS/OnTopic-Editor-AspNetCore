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
  | CLASS: TOPIC LIST ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TopicListViewComponent"/>.
  /// </summary>
  public class TopicListAttributeTopicViewModel: QueryableTopicListAttributeTopicViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     string?             _valueProperty                  = null;

    /*==========================================================================================================================
    | DEFAULT LABEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Sets the label name to be used for the placeholder option in the dropdown list, which will have an empty value.
    /// </summary>
    public string? DefaultLabel { get; set; }

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
    | PROPERTY: STORE UNIQUE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether to use a fully qualified <see cref="TopicViewModel.UniqueKey"/> or just the <see
    ///   cref="TopicViewModel.Key"/> when saving the value to the database.
    /// </summary>
    /// <remarks>
    ///   A <see cref="TopicViewModel.UniqueKey"/> makes it easier to construct or retrieve the corresponding topic object
    ///   without any knowledge of where that object exists. Further, under certain circumstances, a <see
    ///   cref="TopicViewModel.UniqueKey"/> may be necessary to guarantee uniqueness (for instance, if the <v>values</v> are
    ///   overridden with a collection of topics from multiple locations in the topic tree). That said, the <see
    ///   cref="TopicViewModel.Key"/> may be a preferred value, particularly when not intended to provide a strongly-typed
    ///   reference to particular topics (e.g., when the <see cref="LookupListViewComponent"/> is being used to simply provide a
    ///   constrained list of known values, such as tags).
    /// </remarks>
    public bool? StoreUniqueKey { get; set; }

    /*==========================================================================================================================
    | VALUE TOKEN
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines what token to bind the topic list to. By default, it is <see cref="TopicViewModel.Key"/>, unless <see
    ///   cref="StoreUniqueKey"/> is selected, in which case it becomes <see cref="TopicViewModel.UniqueKey"/>. These defaults
    ///   can be overwritten using the <see cref="ValueToken"/>.
    /// </summary>
    /// <remarks>
    ///   Not just any attribute or property may be used. The <see cref="TopicListViewComponent"/> contains a list of tokens
    ///   that it knows how to replace. These include:
    ///   <list type="bullet">
    ///     <item><c>Topic</c> (for <see cref="TopicViewModel.Key"/>)</item>
    ///     <item><c>TopicId</c> (for <see cref="TopicViewModel.Id"/>)</item>
    ///     <item><c>Name</c> (for <see cref="TopicViewModel.Key"/>)</item>
    ///     <item><c>Key</c> (for <see cref="TopicViewModel.Key"/>)</item>
    ///     <item><c>FullName</c> (for <see cref="TopicViewModel.UniqueKey"/>)</item>
    ///     <item><c>UniqueKey</c> (for <see cref="TopicViewModel.UniqueKey"/>)</item>
    ///     <item><c>Title</c> (for <see cref="TopicViewModel.Title"/>)</item>
    ///     <item><c>Parent</c> (for <see cref="TopicViewModel.Parent.UniqueKey"/>)</item>
    ///     <item><c>GrandParent</c> (for <see cref="TopicViewModel.Parent.Parent.UniqueKey"/>)</item>
    ///     <item><c>GrandParentId</c> (for <see cref="TopicViewModel.Parent.Parent.Id"/>)</item>
    ///   </list>
    /// </remarks>
    public string? ValueToken {
      get {
        if (_valueProperty == null) {
          _valueProperty = StoreUniqueKey is true ? "UniqueKey" : "Key";
        }
        return _valueProperty;
      }
      set => _valueProperty = value;
    }

    /*==========================================================================================================================
    | ALLOWED KEYS
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string? AllowedKeys { get; set; }

  } //Class
} //Namespace

#nullable restore