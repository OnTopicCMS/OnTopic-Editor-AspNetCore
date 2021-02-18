/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore.Models;

namespace OnTopic.Editor.AspNetCore.Attributes.InstructionAttribute {

  /*============================================================================================================================
  | CLASS: INSTRUCTION (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for an instruction attribute type.
  /// </summary>
  public class InstructionViewComponent: ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="InstructionViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic <see cref="NavigationTopicViewComponentBase{T}"/>.</returns>
    public InstructionViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="InstructionViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      InstructionAttributeDescriptorViewModel attribute,
      string htmlFieldPrefix
    ) {
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
      return View(new AttributeViewModel<InstructionAttributeDescriptorViewModel>(currentTopic, attribute));
    }

  } // Class
} // Namespace