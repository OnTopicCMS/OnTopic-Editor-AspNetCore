/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.Models;
using OnTopic.Editor.Models.Metadata;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Components {

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
    ///   Initializes a new instance of a <see cref="FileViewComponent"/> with necessary dependencies.
    /// </summary>
    public FileListViewComponent(IWebHostEnvironment webHostEnvironment): base() {
      _webHostEnvironment = webHostEnvironment;
    }

    /*==========================================================================================================================
    | METHOD: INVOKE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public IViewComponentResult Invoke(
      EditingTopicViewModel currentTopic,
      FileListAttributeTopicViewModel attribute,
      string htmlFieldPrefix
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set HTML prefix
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set configuration values
      \-----------------------------------------------------------------------------------------------------------------------*/
      attribute.Path                    ??= attribute.GetConfigurationValue("Path", null);
      attribute.Extension               ??= attribute.GetConfigurationValue("Extension", null);
      attribute.Filter                  ??= attribute.GetConfigurationValue("Filter", null);
      attribute.IncludeSubdirectories   ??= attribute.GetBooleanConfigurationValue("IncludeSubdirectories", false);

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish view model
      \-----------------------------------------------------------------------------------------------------------------------*/
      var model = new FileListAttributeViewModel(currentTopic, attribute);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      model.AbsolutePath        = _webHostEnvironment.ContentRootPath + attribute.Path;
      model.Files               = GetFiles(model.InheritedValue, attribute, model.AbsolutePath);

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
    public List<SelectListItem> GetFiles(
      string inheritedValue,
      FileListAttributeTopicViewModel attribute,
      string absolutePath
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | INSTANTIATE OBJECTS
      \-----------------------------------------------------------------------------------------------------------------------*/
      var files                 = new List<SelectListItem>();
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
      if (attribute.Extension != null) {
        searchPattern = searchPattern + "." + attribute.Extension;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Filter file list based on filter criteria
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (attribute.Filter != null) {
        searchPattern = attribute.Filter + searchPattern;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | GET ALL FILES
      \-----------------------------------------------------------------------------------------------------------------------*/
      var foundFiles = Directory.GetFiles(absolutePath, searchPattern, searchOption);

      if (!String.IsNullOrEmpty(inheritedValue)) {
        files.Add(new SelectListItem("", inheritedValue));
      }
      foreach (var foundFile in foundFiles) {
        var fileName = foundFile.Replace(absolutePath, "");
        var fileNameKey = fileName.Replace("." + attribute.Extension, "");
        files.Add(new SelectListItem(fileNameKey, fileName));
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN FILE LIST
      \-----------------------------------------------------------------------------------------------------------------------*/
      return files;

    }

  } // Class
} // Namespace

#nullable restore
