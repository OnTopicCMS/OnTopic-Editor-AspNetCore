/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Mvc;
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Components;
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Editor.Models.Components.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: FILE PATH (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a file path attribute type.
  /// </summary>
  public class FilePathViewComponent: AttributeTypeViewComponentBase {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FilePathViewComponent"/> with necessary dependencies.
    /// </summary>
    public FilePathViewComponent(ITopicRoutingService topicRoutingService) : base(topicRoutingService) { }

    /*==========================================================================================================================
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
      FilePathAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.BaseTopicPath                   ??= attribute.GetConfigurationValue("TruncatePathAtTopic", "");
      attribute.InheritValue                    ??= attribute.GetBooleanConfigurationValue("InheritValue", true);
      attribute.RelativeToTopicPath             ??= attribute.GetBooleanConfigurationValue("RelativeToTopicPath", true);
      attribute.IncludeCurrentTopic             ??= attribute.GetBooleanConfigurationValue("IncludeLeafNodes", true);

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = GetAttributeViewModel(new FilePathAttributeViewModel(attribute)) as FilePathAttributeViewModel;

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

      var             inheritedValue          = "";

      if (attribute.InheritValue == true && attribute.RelativeToTopicPath == true) {
        inheritedValue                        = GetPath(attributeKey, attribute);
      }
      else if (attribute.InheritValue == true) {
        inheritedValue                        = CurrentTopic.Attributes.GetValue(attributeKey, true);
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
      | Build configured file path string base on values and settings parameters passed to the method
      \-----------------------------------------------------------------------------------------------------------------------*/
      var       filePath                = "";
      var       relativePath            = (string?)null;
      var       startTopic              = CurrentTopic;
      var       endTopic                = (options.IncludeCurrentTopic is null)? CurrentTopic: CurrentTopic.Parent?? CurrentTopic;
      var       truncatePathAtTopic     = options.BaseTopicPath?.Split(',').ToArray()?? new string[] { };

      /*------------------------------------------------------------------------------------------------------------------------
      | Only process the path if both topic and attribtueKey are provided
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (startTopic is null || attributeKey is null || attributeKey.Length.Equals(0)) return "";

      /*------------------------------------------------------------------------------------------------------------------------
      | Crawl up the topics tree to find file path values set at a higher level
      \-----------------------------------------------------------------------------------------------------------------------*/
      while (String.IsNullOrEmpty(filePath) && startTopic != null && startTopic.Parent != null) {
        startTopic                      = startTopic.Parent;
        if (startTopic != null && !String.IsNullOrEmpty(attributeKey)) {
          filePath                      = startTopic.Attributes.GetValue(attributeKey);
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Add topic keys (directory names) between the start topic and the end topic based on the topic's WebPath property
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (startTopic != null) {
        if (startTopic.GetWebPath().Length > endTopic.GetWebPath().Length) {
          throw new InvalidOperationException(
            $"The path of {startTopic.GetWebPath()} should be shorter than the length of {endTopic.GetWebPath()}."
          );
        }
        var startTopicWebPath           = startTopic.GetWebPath().Replace("/Root/", "/");
        relativePath                    = endTopic.GetWebPath().Substring(Math.Max(startTopicWebPath.Length-1,0));
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Perform path truncation based on topics included in TruncatePathAtTopic
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrWhiteSpace(options.BaseTopicPath)) {
        foreach (var truncationTopic in truncatePathAtTopic) {
          var truncateTopicLocation     = relativePath?.IndexOf(truncationTopic, StringComparison.InvariantCultureIgnoreCase);
          if (truncateTopicLocation >= 0) {
            relativePath                = relativePath?.Substring(0, truncateTopicLocation.Value + truncationTopic.Length + 1);
          }
        }
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Add resulting relative path to the original file path (based on starting topic)
      \-----------------------------------------------------------------------------------------------------------------------*/
      filePath                         += relativePath;

      /*------------------------------------------------------------------------------------------------------------------------
      | Replace path slashes with backslashes if the resulting file path value uses a UNC or basic file path format
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (filePath.IndexOf("\\", StringComparison.InvariantCulture) >= 0) {
        filePath                        = filePath.Replace("/", "\\");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return resulting file path
      \-----------------------------------------------------------------------------------------------------------------------*/
      return filePath;

    }

  } // Class
} // Namespace

#nullable restore
