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
  | CLASS: TOPIC LIST (OPTIONS)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Establishes options associated with the <see cref="TopicListViewComponent"/>.
  /// </summary>
  public class TopicListOptions: DefaultOptions {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private string? _valueProperty = null;

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
    | TARGET URL
    >---------------------------------------------------------------------------------------------------------------------------
    | ### TODO JJC092313: Need to add support for {token} replacements in the TargetUrl.  Also, unclear what the current default
    | logic is doing; I don't believe this should be necessary.
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The TargetUrl allows the dropdown control to trigger the loading of a new page based on the value of the dropdown box.
    ///   The new page is loaded using the LoadPage event handler, and may optionally be handled as a redirect (default) or a
    ///   popup (based on the TargetPopup boolean).
    /// </summary>
    public string? TargetUrl { get; set; }

    /*==========================================================================================================================
    | TARGET POPUP
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   If a TargetUrl is supplied, and TargetPopup is set to true, then the TargetUrl will be loaded during the LoadPage
    ///   event as a popup window.  Otherwise, the TargetUrl will be loaded via a redirect.
    /// </summary>
    public bool? TargetPopup { get; set; }

    /*==========================================================================================================================
    | ON CLIENT CLOSE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   If supplied, sets a reference to a callback function to execute on close of the editor popup.
    /// </summary>
    public string? OnClientClose { get; set; }

    /*==========================================================================================================================
    | LABEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string? Label { get; set; }

    /*==========================================================================================================================
    | PROPERTY: USE UNIQUE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether to use a fully qualified key ("UniqueKey") or just the topic key. A UniqueKey makes it easier to
    ///   construct or retrieve the corresponding topic object without any knowledge of where that object exists. Further, under
    ///   certain circumstances, a UniqueKey may be necessary to guarantee uniqueness (for instance, if DataSource is overridden
    ///   with a collection of topics from multiple locations in the topic tree). That said, the topic key may be a preferred
    ///   value, particularly when not intended to provide a strongly-typed reference to particular topics (e.g., when the
    ///   LookupList is being used to simply provide a constrained list of known values, such as tags).
    /// </summary>
    public bool? UseUniqueKey { get; set; }

    /*==========================================================================================================================
    | VALUE PROPERTY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines what property to bind the TopicLookup/TopicList to.
    /// </summary>
    public string? ValueProperty {
      get {
        if (_valueProperty == null) {
          _valueProperty = UseUniqueKey is true ? "UniqueKey" : "Key";
        }
        return _valueProperty;
      }
      set {
        _valueProperty = value;
      }
    }

    /*==========================================================================================================================
    | ALLOWED KEYS
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string? AllowedKeys { get; set; }

  } // Class
} // Namespace

#nullable restore