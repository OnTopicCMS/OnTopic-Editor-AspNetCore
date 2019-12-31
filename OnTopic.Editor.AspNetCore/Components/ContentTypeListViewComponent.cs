/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTopic.Attributes;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Components {

  /*============================================================================================================================
  | CLASS: CONTENT TYPE LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a list of <see cref="ContentTypeDescriptorTopicViewModel"/>s as a navigation element.
  /// </summary>
  /// <remarks>
  ///   The <see cref="ContentTypeListViewComponent"/> exposes a list of content types as a dropdown list. The list of
  ///   <see cref="ContentTypeDescriptorTopicViewModel"/>s is determined based on a source content type, either by displaying
  ///   the <see cref="ContentTypeDescriptorTopicViewModel.PermittedContentTypes"/>, or by displaying all content types that are
  ///   not hidden, and are implicitly permitted.
  /// </remarks>
  public class ContentTypeListViewComponent : ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITopicRepository                _topicRepository;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="ContentTypeListViewComponent"/> with necessary dependencies.
    /// </summary>
    public ContentTypeListViewComponent(ITopicRepository topicRepository): base() {
      _topicRepository          = topicRepository;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="ContentTypeListViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      NestedTopicListAttributeTopicViewModel attributeDescriptor = null,
      IEnumerable<ContentTypeDescriptorTopicViewModel> values = null,
      string onModalClose = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewModel             = new ContentTypeListViewModel() {
        CurrentTopic            = currentTopic,
        AttributeKey            = attributeDescriptor?.Key,
        EnableModal             = attributeDescriptor?.EnableModal?? false,
        OnModalClose            = onModalClose
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Set label
      \-----------------------------------------------------------------------------------------------------------------------*/
      viewModel.TopicList.Add(
        new SelectListItem {
          Value = null,
          Text = "Add a child topic…"
        }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Add values to view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var contentType in values.OrderBy(c => c.Title)) {
        viewModel.TopicList.Add(
          new SelectListItem {
            Value               = getValue(contentType.Key),
            Text                = contentType.Title
          }
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get implicit values
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypes          = _topicRepository.GetContentTypeDescriptors();
      var currentContentType    = contentTypes.GetTopic(currentTopic.ContentType);

      //If no permitted content types are explicitly set, then implicitly
      if (viewModel.TopicList.Count.Equals(1) && !currentContentType.DisableChildTopics) {
        viewModel.TopicList.AddRange(
          contentTypes
            .Where(c => currentContentType.Equals("Container") || c.Attributes.GetBoolean("ImplicitlyPermitted", false))
            .Where(c => !c.IsHidden)
            .OrderBy(c => c.Title)
            .Select(c =>
              new SelectListItem {
                Value           = getValue(c.Key),
                Text            = c.Title
              }
            )
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(viewModel);

      /*------------------------------------------------------------------------------------------------------------------------
      | Helper functions
      \-----------------------------------------------------------------------------------------------------------------------*/
      string getValue(string contentType) =>
        $"{Url.Action("Edit")}" +
        $"/{attributeDescriptor?.Key}" +
        $"?IsNew=true" +
        $"&IsModal={attributeDescriptor?.EnableModal?? false}" +
        $"&ContentType={contentType}";

    }

  } // Class
} // Namespace