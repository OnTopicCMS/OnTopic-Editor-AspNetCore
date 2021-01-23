/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTopic.Editor.Models;
using OnTopic.Internal.Diagnostics;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.FileListAttribute {

  /*============================================================================================================================
  | CLASS: FILE (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a file attribute type.
  /// </summary>
  public class FileListViewComponent: ViewComponent {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            IWebHostEnvironment             _webHostEnvironment;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="FileListViewComponent"/> with necessary dependencies.
    /// </summary>
    public FileListViewComponent(IWebHostEnvironment webHostEnvironment): base() {
      _webHostEnvironment = webHostEnvironment;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="FileListViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      FileListAttributeDescriptorTopicViewModel attribute,
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
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new FileListAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var file in GetFiles(model.InheritedValue, attribute, model.AbsolutePath)) {
        model.Files.Add(file);
      };
      model.AbsolutePath        = _webHostEnvironment.ContentRootPath + attribute.Path;

      /*------------------------------------------------------------------------------------------------------------------------
      | Return view with view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      return View(model);

    }

    /*==========================================================================================================================
    | METHOD: GET FILES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a collection of files in a directory, given the provided <see cref="Path"/>.
    /// </summary>
    public static Collection<SelectListItem> GetFiles(
      string inheritedValue,
      FileListAttributeDescriptorTopicViewModel attribute,
      string absolutePath
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(attribute, nameof(attribute));

      /*------------------------------------------------------------------------------------------------------------------------
      | INSTANTIATE OBJECTS
      \-----------------------------------------------------------------------------------------------------------------------*/
      var files                 = new Collection<SelectListItem>();
      var searchPattern         = "*";
      var searchOption          = attribute.IncludeSubdirectories is true? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (String.IsNullOrEmpty(attribute.Path)) return files;
      if (!Directory.Exists(absolutePath)) return files;

      /*------------------------------------------------------------------------------------------------------------------------
      | Filter file list based on extension
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (attribute.Extension is not null) {
        searchPattern = searchPattern + "." + attribute.Extension;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Filter file list based on filter criteria
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (attribute.Filter is not null) {
        searchPattern = attribute.Filter + searchPattern;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | GET ALL FILES
      \-----------------------------------------------------------------------------------------------------------------------*/
      var foundFiles = Directory.GetFiles(absolutePath, searchPattern, searchOption);

      if (!String.IsNullOrEmpty(inheritedValue)) {
        files.Add(new("", inheritedValue));
      }
      foreach (var foundFile in foundFiles) {
        var fileName = foundFile.Replace(absolutePath, "", StringComparison.OrdinalIgnoreCase);
        var fileNameKey = fileName.Replace("." + attribute.Extension, "", StringComparison.OrdinalIgnoreCase);
        files.Add(new(fileNameKey, fileName));
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN FILE LIST
      \-----------------------------------------------------------------------------------------------------------------------*/
      return files;

    }

  } // Class
} // Namespace

#nullable restore