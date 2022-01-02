/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTopic.Attributes;
using OnTopic.Editor.AspNetCore.Models.Components;
using OnTopic.Repositories;

namespace OnTopic.Editor.AspNetCore.Components {

  /*============================================================================================================================
  | CLASS: CONTENT TYPE LIST (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a list of <see cref="ContentTypeDescriptorViewModel"/>s as a navigation element.
  /// </summary>
  /// <remarks>
  ///   The <see cref="ContentTypeListViewComponent"/> exposes a list of content types as a dropdown list. The list of <see cref
  ///   ="ContentTypeDescriptorViewModel"/>s is determined based on a source content type, either by displaying the <see cref="
  ///   ContentTypeDescriptorViewModel.PermittedContentTypes"/>, or by displaying all content types that are not hidden, and are
  ///   implicitly permitted.
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
      ContentTypeListAttributeDescriptorViewModel? attributeDescriptor = null,
      IEnumerable<PermittedContentTypeViewModel>? values = null,
      string? onModalClose = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));

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
        new() {
          Value = null,
          Text = "Add a child topic…"
        }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Add values to view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var contentType in values.OrderBy(c => c.Title)) {
        viewModel.TopicList.Add(
          new() {
            Value               = getValue(contentType.Key!),
            Text                = contentType.Title
          }
        );
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get content type
      >-------------------------------------------------------------------------------------------------------------------------
      | If the database is uninitialized, the content type won't be found. In that case, return an empty view, which will
      | effectively hide the component. If the topic cannot be found, assume it is a new topic and attempt to load the parent
      | for context.
      \-----------------------------------------------------------------------------------------------------------------------*/
      var contentTypes          = _topicRepository.GetContentTypeDescriptors();
      var actualTopic           = _topicRepository.Load(currentTopic.Id)?? _topicRepository.Load(currentTopic.Parent?.Id?? Int32.MinValue);
      var actualContentType     = contentTypes.GetValue(currentTopic.ContentType!);

      if (actualContentType is null || actualTopic is null) {
        return View(viewModel);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get permitted content types for container
      >-------------------------------------------------------------------------------------------------------------------------
      | Containers provide special rules allowing the permitted content types to be defined—or even expanded—on the topic
      | instance itself, instead of exclusively on the content type descriptor. This is useful since often containers are meant
      | to organize a specific type of content. For example, a Container called "Forms" might be used exclusively to organized
      | Form topics.
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (actualContentType.Key.Equals("Container", StringComparison.OrdinalIgnoreCase)) {
        var permittedContentTypes = actualTopic
          .Relationships
          .GetValues("ContentTypes")
          .Select(c =>
            new SelectListItem() {
              Value             = getValue(c.Key),
              Text              = c.Title
            }
          );
        foreach (var contentType in permittedContentTypes) {
          viewModel.TopicList.Add(contentType);
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Get implicit values
      >-------------------------------------------------------------------------------------------------------------------------
      | If no permitted content types are explicitly defined on the content type—or the topic, in the case of a container—then
      | we instead load a generic list of content types. We don't want to display all content types, however, as many make
      | little sense outside of specific contexts. For instance, most content types that derive from Items only make sense as
      | nested topics. Similarly, specialized types like Home might only make sense in one location. To facilitate this, only
      | content types explicitly annotated as "implicitly permitted", and which are not marked as hidden are displayed. This
      | typically include the Page content type, and popular derivatives of it, such as Content List.
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (viewModel.TopicList.Count is 1 && !actualContentType.DisableChildTopics) {

        var implicitValues = contentTypes
          .Where(c => actualContentType.Equals("Container") || c.Attributes.GetBoolean("ImplicitlyPermitted", false))
          .Where(c => !c.IsHidden)
          .OrderBy(c => c.Title)
          .Select(c =>
            new SelectListItem {
              Value             = getValue(c.Key),
              Text              = c.Title
            }
          );
        foreach (var contentType in implicitValues) {
          viewModel.TopicList.Add(contentType);
        }
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