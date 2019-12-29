/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Components;
using OnTopic.Editor.Models.Components.ViewModels;
using OnTopic.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnTopic.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: NUMBER (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a numeric attribute type.
  /// </summary>
  public class NumberViewComponent: ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="NumberViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A <see cref="NumberViewComponent"/>.</returns>
    public NumberViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      NumberAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<NumberAttributeTopicViewModel>(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class

} // Namespace