/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOKENIZED TOPIC LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a tokenized topic list attribute type.
  /// </summary>
  public class TokenizedTopicListViewComponent : DefaultAttributeTypeViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TokenizedTopicListViewComponent"/> with necessary dependencies.
    /// </summary>
    public TokenizedTopicListViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

  } // Class
} // Namespace