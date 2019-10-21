/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ignia.Topics.Mapping;
using Ignia.Topics.Models;
using Ignia.Topics.AspNetCore.Mvc.Models;
using Ignia.Topics.Editor.Models;
using System;
using Ignia.Topics.Editor.Models.Components.Options;

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE TYPE (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Defines a foundation for custom <see cref="ViewComponent"/> implementations that is based on an <see
  ///   cref="AttributeDescriptor"/> and a current <see cref="Topic"/> reference.
  ///   model.
  /// </summary>
  /// <remarks>
  ///   This class is intended to provide a foundation for concrete implementations. It is not a fully formed implementation
  ///   itself. As a result, it is marked as <c>abstract</c>.
  /// </remarks>
  public abstract class AttributeTypeViewComponentBase : ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRoutingService            _topicRoutingService            = null;
    private                     Topic                           _currentTopic                   = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="AttributeTypeViewComponentBase"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic <see cref="NavigationTopicViewComponentBase{T}"/>.</returns>
    protected AttributeTypeViewComponentBase(
      ITopicRoutingService topicRoutingService
    ) {
      _topicRoutingService = topicRoutingService;
    }

    /*==========================================================================================================================
    | CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the current topic associated with the request.
    /// </summary>
    /// <returns>The Topic associated with the current request.</returns>
    protected Topic CurrentTopic {
      get {
        if (_currentTopic == null) {
          _currentTopic = _topicRoutingService.GetCurrentTopic();
        }
        return _currentTopic;
      }
    }

    /*==========================================================================================================================
    | METHOD: GET ATTRIBUTE VIEW MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given an <see cref="AttributeDescriptorTopicViewModel"/>, creates a new <see cref="AttributeViewModel"/> and ensures
    ///   that the properties are properly set.
    /// </summary>
    /// <param name="attribute">
    ///   The <see cref="AttributeDescriptorTopicViewModel"/> to initialize a new <see cref="AttributeViewModel"/> with.
    /// </param>
    /// <returns>The Topic associated with the current request.</returns>
    public AttributeViewModel<DefaultOptions> GetAttributeViewModel(
      AttributeDescriptorTopicViewModel attribute,
      DefaultOptions options
    ) {
      var viewModel = new AttributeViewModel<DefaultOptions>(attribute, options);
      GetAttributeViewModel(viewModel);
      return viewModel;
    }

    /// <summary>
    ///   Ensures that the properties of the <see cref="AttributeViewModel"/> are properly set.
    /// </summary>
    /// <param name="viewModel">The <see cref="AttributeViewModel"/> to populate with values.</param>
    /// <returns>The Topic associated with the current request.</returns>
    public AttributeViewModel GetAttributeViewModel(AttributeViewModel viewModel = null) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set attribute
      \-----------------------------------------------------------------------------------------------------------------------*/
      var attribute             = viewModel.AttributeDescriptor;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set contextual values from current topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      viewModel.InheritedValue  = CurrentTopic.Parent.Attributes.GetValue(attribute.Key, true);
      viewModel.Value           = CurrentTopic.Attributes.GetValue(attribute.Key, attribute.DefaultValue, false, false);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set convenience pass-throughs to configuration
      \-----------------------------------------------------------------------------------------------------------------------*/
      viewModel.IsEnabled       = attribute.GetBooleanConfigurationValue(
        "IsEnabled",
        attribute.GetBooleanConfigurationValue("Enabled", true)
      );

      viewModel.CssClass      = attribute.GetConfigurationValue(
        "CssClass",
        attribute.GetConfigurationValue("CssClassField", null)
      );

      return viewModel;

    }

  } // Class
} // Namespace