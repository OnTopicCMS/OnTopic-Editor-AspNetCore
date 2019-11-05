/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components;
using Ignia.Topics.Editor.Models.Components.ViewModels;
using Ignia.Topics.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: NUMBER (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a numeric attribute type.
  /// </summary>
  public class NumberViewComponent: AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="NumberViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <returns>A <see cref="NumberViewComponent"/>.</returns>
    public NumberViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
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

      GetAttributeViewModel(viewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class

} // Namespace