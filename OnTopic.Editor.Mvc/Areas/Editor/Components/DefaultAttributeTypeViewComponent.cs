/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Threading.Tasks;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace OnTopic.Editor.Mvc.Components {

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
  public class DefaultAttributeTypeViewComponent : ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="DefaultAttributeTypeViewComponent"/> with necessary dependencies.
    /// </summary>
    public DefaultAttributeTypeViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      AttributeDescriptorTopicViewModel attribute,
      string htmlFieldPrefix
    ) {
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
      var viewModel = new AttributeViewModel<AttributeDescriptorTopicViewModel>(currentTopic, attribute);
      return View(viewModel);
    }

  } // Class
} // Namespace