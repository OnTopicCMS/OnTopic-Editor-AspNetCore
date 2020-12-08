/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnTopic.AspNetCore.Mvc;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Components.ViewModels;
using OnTopic.Editor.Models.Metadata;
using OnTopic.Internal.Diagnostics;
using OnTopic.Repositories;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Components {

  /*============================================================================================================================
  | CLASS: FILE PATH (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a file path attribute type.
  /// </summary>
  public class FilePathViewComponent: ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     Topic?                          _currentTopic;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FilePathViewComponent"/> with necessary dependencies.
    /// </summary>
    /// <remarks>
    ///   As with other attribute view components, the <see cref="FilePathViewComponent"/> receives a <see
    ///   cref="EditingTopicViewModel"/> via the <see cref="InvokeAsync(EditingTopicViewModel, FilePathAttributeTopicViewModel,
    ///   String)"/>. That view model, however, is not sufficient to handle the specialized inheritance logic required by the
    ///   <see cref="FilePathViewComponent"/>. As a result, it <i>also</i> requires an instance of a <see
    ///   cref="ITopicRoutingService"/> so that it can work directly off the current <see cref="Topic"/> and its parent tree.
    ///   The <see cref="EditingTopicViewModel"/> is still passed not only for consistency, but also to spare the overhead and
    ///   redundant logic of mapping it again, since this was already done in <see cref="Controllers.EditorController"/>.
    /// </remarks>
    public FilePathViewComponent(ITopicRepository topicRepository) : base() {
      Contract.Requires(topicRepository, nameof(topicRepository));
      TopicRepository = topicRepository;
    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="ITopicRepository"/> in order to allow the current topic to be identified based
    ///   on the route data.
    /// </summary>
    /// <returns>
    ///   The <see cref="ITopicRepository"/> associated with the <see cref="TopicViewComponentBase{T}"/>.
    /// </returns>
    protected ITopicRepository TopicRepository { get; }

    /*==========================================================================================================================
    | CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the current topic associated with the request.
    /// </summary>
    /// <returns>The Topic associated with the current request.</returns>
    protected Topic? CurrentTopic {
      get {
        if (_currentTopic is null) {
          _currentTopic = TopicRepository.Load(RouteData);
        }
        Contract.Assume(_currentTopic, nameof(_currentTopic));
        return _currentTopic;
      }
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="FilePathViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      FilePathAttributeTopicViewModel attribute,
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
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.BaseTopicPath           ??= attribute.GetConfigurationValue("TruncatePathAtTopic", "");
      attribute.InheritValue            ??= attribute.GetBooleanConfigurationValue("InheritValue", true);
      attribute.RelativeToTopicPath     ??= attribute.GetBooleanConfigurationValue("RelativeToTopicPath", true);
      attribute.IncludeCurrentTopic     ??= attribute.GetBooleanConfigurationValue("IncludeLeafNodes", true);

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new FilePathAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      model!.InheritedValue = GetInheritedValue(attribute.Key!, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

    /*==========================================================================================================================
    | METHOD: GET INHERITED VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Crawls up the tree to identify the source of inheritance (if available) and sets the value based on the base path
    ///   defined by the parent (assuming <see cref="InheritValue"/> is enabled) and the relative path between that topic and
    ///   the current topic (assuming <see cref="RelativeToParent"/> is enabled).
    /// </summary>
    public string GetInheritedValue(string attributeKey, FilePathAttributeTopicViewModel attribute) {

      var inheritedValue        = "";

      if (attribute is { InheritValue: true, RelativeToTopicPath: true }) {
        inheritedValue          = GetPath(attributeKey, attribute);
      }
      else if (attribute is { InheritValue: true }) {
        inheritedValue          = CurrentTopic?.Attributes.GetValue(attributeKey, true)?? "";
      }

      return inheritedValue;

    }

    /*==========================================================================================================================
    | METHOD: GET PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Static helper method that returns a constructed file path based on evaluation and processing of the parameter
    ///   values/settings passed to the method.
    /// </summary>
    /// <param name="topic">The topic object.</param>
    /// <param name="attributeKey">The attribute key.</param>
    /// <param name="includeLeafTopic">Boolean indicator as to whether to include the endpoint/leaf topic in the path.</param>
    /// <param name="truncatePathAtTopic">The assembled topic keys at which to end the path string.</param>
    /// <returns>A constructed file path.</returns>
    public string GetPath(string attributeKey, FilePathAttributeTopicViewModel options) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(options, nameof(options));

      /*------------------------------------------------------------------------------------------------------------------------
      | Build configured file path string base on values and settings parameters passed to the method
      \-----------------------------------------------------------------------------------------------------------------------*/
      var filePath              = "";
      var relativePath          = (string?)null;
      var startTopic            = CurrentTopic;
      var endTopic              = (options.IncludeCurrentTopic is null)? CurrentTopic: CurrentTopic?.Parent?? CurrentTopic;
      var truncatePathAtTopic   = options.BaseTopicPath?.Split(',').ToArray()?? Array.Empty<string>();

      /*------------------------------------------------------------------------------------------------------------------------
      | Only process the path if both topic and attribtueKey are provided
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (startTopic is null || attributeKey is null or { Length: 0 }) return "";

      /*------------------------------------------------------------------------------------------------------------------------
      | Crawl up the topics tree to find file path values set at a higher level
      \-----------------------------------------------------------------------------------------------------------------------*/
      while (String.IsNullOrEmpty(filePath) && startTopic is not null && startTopic.Parent is not null) {
        startTopic              = startTopic.Parent;
        if (startTopic is not null && !String.IsNullOrEmpty(attributeKey)) {
          filePath              = startTopic.Attributes.GetValue(attributeKey);
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Add topic keys (directory names) between the start topic and the end topic based on the topic's WebPath property
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (startTopic is not null) {
        if (startTopic.GetWebPath().Length > endTopic?.GetWebPath().Length) {
          throw new InvalidOperationException(
            $"The path of {startTopic.GetWebPath()} should be shorter than the length of {endTopic.GetWebPath()}."
          );
        }
        var startTopicWebPath   = startTopic.GetWebPath().Replace("/Root/", "/", StringComparison.OrdinalIgnoreCase);
        relativePath            = endTopic?.GetWebPath()[Math.Max(startTopicWebPath.Length-1, 0)..];
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Perform path truncation based on topics included in TruncatePathAtTopic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrWhiteSpace(options.BaseTopicPath)) {
        foreach (var truncationTopic in truncatePathAtTopic) {
          var truncateTopicLocation = relativePath?.IndexOf(truncationTopic, StringComparison.InvariantCultureIgnoreCase);
          if (truncateTopicLocation >= 0) {
            relativePath        = relativePath?.Substring(0, truncateTopicLocation.Value + truncationTopic.Length + 1);
          }
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Add resulting relative path to the original file path (based on starting topic)
      \-----------------------------------------------------------------------------------------------------------------------*/
      filePath                  += relativePath;

      /*------------------------------------------------------------------------------------------------------------------------
      | Replace path slashes with backslashes if the resulting file path value uses a UNC or basic file path format
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (filePath.Contains("\\", StringComparison.InvariantCulture)) {
        filePath                = filePath.Replace("/", "\\", StringComparison.Ordinal);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return resulting file path
      \-----------------------------------------------------------------------------------------------------------------------*/
      return filePath;

    }

  } // Class
} // Namespace

#nullable restore