/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: DISPLAY OPTIONS (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a display options attribute type.
  /// </summary>
  public class DisplayOptionsViewComponent: DefaultAttributeTypeViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="DisplayOptionsViewComponent"/> with necessary dependencies.
    /// </summary>
    public DisplayOptionsViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

  } // Class
} // Namespace