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
using System.Diagnostics.CodeAnalysis;
using Ignia.Topics.Editor.Models.Metadata;

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
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="AttributeTypeViewComponentBase"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic <see cref="NavigationTopicViewComponentBase{T}"/>.</returns>
    protected AttributeTypeViewComponentBase() {}

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
    public AttributeViewModel<AttributeDescriptorTopicViewModel> GetAttributeViewModel(
      EditingTopicViewModel topic,
      AttributeDescriptorTopicViewModel attribute
    ) {
      var viewModel = new AttributeViewModel<AttributeDescriptorTopicViewModel>(topic, attribute);
      GetAttributeViewModel(viewModel);
      return viewModel;
    }

    /// <summary>
    ///   Ensures that the properties of the <see cref="AttributeViewModel"/> are properly set.
    /// </summary>
    /// <param name="viewModel">The <see cref="AttributeViewModel"/> to populate with values.</param>
    /// <returns>The Topic associated with the current request.</returns>
    [return: NotNullIfNotNull("viewModel")]
    public AttributeViewModel GetAttributeViewModel(AttributeViewModel viewModel) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      var topic                = viewModel.CurrentTopic;
      var key                  = viewModel.AttributeDescriptor.Key;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set contextual values from current topic
      \-----------------------------------------------------------------------------------------------------------------------*/
      viewModel.TopicId         = topic.Id;
      viewModel.InheritedValue  = topic.InheritedAttributes.ContainsKey(key)? topic.InheritedAttributes[key] : null;
      viewModel.Value           = topic.Attributes.ContainsKey(key)? topic.Attributes[key] : null;

      /*------------------------------------------------------------------------------------------------------------------------
      | Return value
      \-----------------------------------------------------------------------------------------------------------------------*/
      return viewModel;

    }

  } // Class
} // Namespace