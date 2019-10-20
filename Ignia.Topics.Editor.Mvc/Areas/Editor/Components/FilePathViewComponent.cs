/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: FILE PATH (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a file path attribute type.
  /// </summary>
  public class FilePathViewComponent: DefaultAttributeTypeViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FilePathViewComponent"/> with necessary dependencies.
    /// </summary>
    public FilePathViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

  } // Class
} // Namespace