/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnTopic.Editor.AspNetCore.Models.Components {

  /*============================================================================================================================
  | CLASS: CONTENT TYPE LIST (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a view model for displaying a navigable list of content types.
  /// </summary>
  public record ContentTypeListViewModel {

    /*==========================================================================================================================
    | PROPERTY: CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="EditingTopicViewModel"/> that the user is currently editing.
    /// </summary>
    public EditingTopicViewModel? CurrentTopic { get; init; }

    /*==========================================================================================================================
    | TOPIC LIST
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of key/value pairs associated with the topic lookup.
    /// </summary>
    /// <remarks>
    ///   The <see cref="SelectListItem.Value"/> represents the value that will be persisted to the <see cref="Topic.Attributes"
    ///   /> collection. The <see cref="SelectListItem.Text"/> represents the label as it will be displayed in the interface.
    ///   For instance, for a <c>TopicListAttributeViewModel</c> representing countries, <c>US</c> might by the value associated
    ///   with a <c>United States</c> label.
    /// </remarks>
    public Collection<SelectListItem> TopicList { get; } = new();

    /*==========================================================================================================================
    | ATTRIBUTE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Denotes the attribute associated with the content type list, if any. This is used to access the modal window and
    ///   ensure any elements are uniquely identified.
    /// </summary>
    public string? AttributeKey { get; init; }

    /*==========================================================================================================================
    | ENABLE MODAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   If <see cref="EnableModal"/> is set to <c>true</c>, then the <see cref="SelectListItem.Value"/>—which is assumed to be
    ///   a URL—will be loaded into a modal window. Otherwise, the <see cref="SelectListItem.Value"/> will be loaded via a
    ///   redirect. The default is <c>false</c>.
    /// </summary>
    public bool? EnableModal { get; init; }

    /*==========================================================================================================================
    | ON MODAL CLOSE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   If supplied, sets a reference to a callback function to execute on close of the editor modal window.
    /// </summary>
    public string? OnModalClose { get; init; }

  } // Class
} // Namespace