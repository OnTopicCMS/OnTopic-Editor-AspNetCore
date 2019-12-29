/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnTopic.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: HTML (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a HTML attribute type.
  /// </summary>
  public class HtmlViewComponent : ViewComponent {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="HtmlViewComponent"/> with necessary dependencies.
    /// </summary>
    public HtmlViewComponent() : base() { }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="HtmlViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      HtmlAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.Columns         ??= attribute.GetIntegerConfigurationValue(     "Columns",              70);
      attribute.Rows            ??= attribute.GetIntegerConfigurationValue(     "Rows",                 20);
      attribute.Height          ??= attribute.GetIntegerConfigurationValue(     "Height",               0);
      attribute.CssClass        ??= attribute.GetConfigurationValue(            "CssClass",             "FormField Field");

      if (attribute.Height == null || attribute.Height == 0 && attribute.Rows != null) {
        attribute.Height = attribute.Rows * 20;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<HtmlAttributeTopicViewModel>(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace