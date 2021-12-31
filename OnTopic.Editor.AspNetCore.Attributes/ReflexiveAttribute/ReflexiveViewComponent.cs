/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.AspNetCore.Models.Metadata;
using OnTopic.Internal.Diagnostics;
using OnTopic.Mapping;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Attributes.ReflexiveAttribute {

  /*============================================================================================================================
  | CLASS: REFLEXIVE (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a reflexive attribute type.
  /// </summary>
  public class ReflexiveViewComponent : ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;
    private readonly            ITopicMappingService            _topicMappingService;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="ReflexiveViewComponent"/> with necessary dependencies.
    /// </summary>
    public ReflexiveViewComponent(ITopicRepository topicRepository, ITopicMappingService topicMappingService) : base() {
      _topicRepository = topicRepository;
      _topicMappingService = topicMappingService;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="ReflexiveViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      EditingTopicViewModel currentTopic,
      ReflexiveAttributeDescriptorViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));
      Contract.Requires(attribute, nameof(attribute));

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish snapshot of previously saved attribute descriptor
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topic                 = _topicRepository.Load(currentTopic.UniqueKey);
      var reflexiveViewModel    = (AttributeDescriptorViewModel?)null;

      if (topic?.ContentType.EndsWith("AttributeDescriptor", StringComparison.OrdinalIgnoreCase)?? false) {
        reflexiveViewModel      = (AttributeDescriptorViewModel?)await _topicMappingService.MapAsync(topic).ConfigureAwait(false);
      }

      if (reflexiveViewModel is null) {
        reflexiveViewModel      = new();
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish hybrid view model
      >-------------------------------------------------------------------------------------------------------------------------
      | The ParentAttributeDescriptor will be of the target type expected for the view component that will be executed. But it
      | should use the core AttributeDescriptor attributes of the current attribute, so it shows up in the same location with
      | the same title and description as defined for the ReflexiveAttributeDescriptorViewModel.
      \-----------------------------------------------------------------------------------------------------------------------*/
      reflexiveViewModel        = reflexiveViewModel with {
        Key                     = attribute.Key,
        Description             = attribute.Description,
        DisplayGroup            = attribute.DisplayGroup,
        DefaultValue            = attribute.DefaultValue,
        IsRequired              = attribute.IsRequired,
        SortOrder               = attribute.SortOrder,
        Title                   = attribute.Title
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel = new AttributeViewModel<AttributeDescriptorViewModel>(currentTopic, reflexiveViewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

    }

  } // Class
} // Namespace