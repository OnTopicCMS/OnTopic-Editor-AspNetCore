/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;

namespace OnTopic.Editor.AspNetCore.Components {

  /*============================================================================================================================
  | CLASS: DISPLAY OPTIONS (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a display options attribute type.
  /// </summary>
  [Obsolete(
    "The DisplayOptions view component is no longer supported and will be removed in the next major version of OnTopic. Use " +
    "the TopicList view component instead, which can be bound to a LookupList content type to provide a list of options."
  )]
  public class DisplayOptionsViewComponent: DefaultAttributeTypeViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="DisplayOptionsViewComponent"/> with necessary dependencies.
    /// </summary>
    public DisplayOptionsViewComponent() : base() { }

  } // Class
} // Namespace