/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Threading.Tasks;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Mvc.Models;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ignia.Topics.AspNetCore.Mvc.Components {

  /*============================================================================================================================
  | CLASS: TOPIC LOOKUP (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic lookup attribute type.
  /// </summary>
  public class TopicLookupViewComponent : AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository                = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="TopicLookupViewComponent"/> with necessary dependencies.
    /// </summary>
    public TopicLookupViewComponent(
      ITopicRoutingService      topicRoutingService,
      ITopicRepository          topicRepository
    ) : base(topicRoutingService) {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="TopicLookupViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(AttributeDescriptorTopicViewModel attribute, string htmlFieldPrefix) {

      /*------------------------------------------------------------------------------------------------------------------------
      | DEFAULT PROCESSING
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
      var viewModel = (TopicLookupAttributeViewModel)GetAttributeViewModel(new TopicLookupAttributeViewModel(attribute));

      /*------------------------------------------------------------------------------------------------------------------------
      | SET DEFAULT OPTION
      \-----------------------------------------------------------------------------------------------------------------------*/
      var value = CurrentTopic.Attributes.GetValue(attribute.Key, attribute.DefaultValue, false, false);
      viewModel.Options.Add(
        new SelectListItem {
          Value = value,
          Text = value
        }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | SET OPTIONS
      \-----------------------------------------------------------------------------------------------------------------------*/
      //### TODO JJC20190324: Custom logic to lookup and set the TopicLookupAttributeViewModel.Options values.

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN VIEW MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace