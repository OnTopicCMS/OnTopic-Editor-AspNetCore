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
  | CLASS: FORM FIELD (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a form field attribute type.
  /// </summary>
  public class FormFieldViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FormFieldViewComponent"/> with necessary dependencies.
    /// </summary>
    public FormFieldViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="FormFieldViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(AttributeDescriptorTopicViewModel attribute, string Id) =>
      View(GetAttributeViewModel(attribute));

  } // Class
} // Namespace