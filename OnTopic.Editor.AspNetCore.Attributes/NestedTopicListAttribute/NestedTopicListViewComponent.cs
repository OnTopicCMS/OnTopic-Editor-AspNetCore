/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Internal.Diagnostics;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Attributes.NestedTopicListAttribute {

  /*============================================================================================================================
  | CLASS: NESTED TOPIC LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a topic list attribute type.
  /// </summary>
  public class NestedTopicListViewComponent : ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="NestedTopicListViewComponent"/> with necessary dependencies.
    /// </summary>
    public NestedTopicListViewComponent(ITopicRepository topicRepository) : base() {
      _topicRepository = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="NestedTopicListViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      NestedTopicListAttributeDescriptorViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));
      Contract.Requires(attribute, nameof(attribute));
      Contract.Requires(attribute.Key, nameof(attribute.Key));

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new NestedTopicListAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (HttpContext.Request.Query.TryGetValue("IsNew", out var action)) {
        viewModel.IsNew         = action.FirstOrDefault().Equals("true", StringComparison.OrdinalIgnoreCase);
      }
      viewModel.UniqueKey       = currentTopic.UniqueKey;
      viewModel.WebPath         = currentTopic.WebPath;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish nested topic container, if needed
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!viewModel.IsNew) {
        var topic = _topicRepository.Load(viewModel.UniqueKey);
        Contract.Assume(topic, $"The topic with the unique key '{viewModel.UniqueKey}' could not be found.");
        if (!topic.Children.Contains(attribute.Key)) {
          var topicContainer = TopicFactory.Create(attribute.Key, "List", topic);
          _topicRepository.Save(topicContainer);
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace