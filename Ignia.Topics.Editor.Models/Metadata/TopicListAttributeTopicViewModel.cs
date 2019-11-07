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