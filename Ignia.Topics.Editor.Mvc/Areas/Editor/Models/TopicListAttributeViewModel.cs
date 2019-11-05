/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.Generic;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ignia.Topics.Editor.Mvc.Models {

  /*============================================================================================================================
  | CLASS: TOPIC LIST (ATTRIBUTE VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Extends the <see cref="AttributeViewModel"/> to include properties that are specific to the topic list view component.
  /// </summary>
  public class TopicListAttributeViewModel: AttributeViewModel<TopicListAttributeTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TopicListAttributeViewModel"/> class.
    /// </summary>
    public TopicListAttributeViewModel(
      EditingTopicViewModel currentTopic,
      TopicListAttributeTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ) : base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) {}

    /*==========================================================================================================================
    | TOPIC LIST
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of key/value pairs associated with the topic lookup.
    /// </summary>
    /// <remarks>
    ///   The key represents the value that will be persisted to the <see cref="Topic.Attributes"/> collection. The value
    ///   represents the label as it will be displayed in the interface. For instance, for a <see
    ///   cref="TopicListAttributeViewModel"/> representing countries, <c>US</c> might by the key associated with a <c>United
    ///   States</c> value.
    /// </remarks>
    public List<SelectListItem> TopicList { get; } = new List<SelectListItem>();

    /*==========================================================================================================================
    | ENABLE MODAL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   If a <see cref="TargetUrl"/> is supplied, and <see cref="EnableModal"/> is set to <c>true</c>, then the <see
    ///   cref="TargetUrl"/> will be loaded in a modal window. Otherwise, the <see cref="TargetUrl"/> will be loaded via a
    ///   redirect. The default is <c>false</c>.
    /// </summary>
    public bool? EnableModal { get; set; }

    /*==========================================================================================================================
    | TARGET URL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The <see cref="TargetUrl"/> allows the dropdown control to trigger the loading of a new page based on the value of
    ///   the dropdown box. The new page is loaded using the LoadPage event handler, and may optionally be handled as a redirect
    ///   (default) or a modal (based on the <see cref="TopicListAttributeTopicViewModel.EnableModal"/> boolean).
    /// </summary>
    public string TargetUrl { get; set; }

    /*==========================================================================================================================
    | ON MODAL CLOSE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   If supplied, sets a reference to a callback function to execute on close of the editor modal window.
    /// </summary>
    public string OnModalClose { get; set; }

  } // Class
} // Namespace
