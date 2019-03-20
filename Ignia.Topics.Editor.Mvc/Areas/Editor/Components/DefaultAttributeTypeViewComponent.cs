/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Threading.Tasks;
using Ignia.Topics.Editor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ignia.Topics.AspNetCore.Mvc.Components {

  /*============================================================================================================================
  | CLASS: DEFAULT ATTRIBUTE TYPE (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a default implementation of the <see cref="AttributeTypeViewComponentBase"/>. Creates a standard <see
  ///   cref="AttributeViewModel"/>.
  /// </summary>
  /// <remarks>
  ///   The <see cref="AttributeTypeViewComponentBase"/> class provides a foundation for all attribute editors. That said, most
  ///   attribute editors won't need to deliver a custom <see cref="AttributeViewModel"/> or provide any customized logic. For
  ///   those, the <see cref="DefaultAttributeTypeViewComponent"/> provides a default implementation of the <see
  ///   cref="InvokeAsync(AttributeDescriptorTopicViewModel, string)"/> method that should satisfy most requirements. It is
  ///   still expected that derived classes be created, but by deriving from <see cref="DefaultAttributeTypeViewComponent"/>,
  ///   they do not need to implement their own <see cref="InvokeAsync(AttributeDescriptorTopicViewModel, string)"/> method.
  /// </remarks>
  public class DefaultAttributeTypeViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="DefaultAttributeTypeViewComponent"/> with necessary dependencies.
    /// </summary>
    public DefaultAttributeTypeViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(AttributeDescriptorTopicViewModel attribute, string htmlFieldPrefix) {
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
      return View(GetAttributeViewModel(attribute));
    }

  } // Class
} // Namespace