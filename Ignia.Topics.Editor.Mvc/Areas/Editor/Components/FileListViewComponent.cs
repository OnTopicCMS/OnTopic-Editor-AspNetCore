/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models;
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Editor.Mvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#nullable enable

namespace Ignia.Topics.Editor.Mvc.Components {

  /*============================================================================================================================
  | CLASS: FILE (VIEW COMPONENT)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Delivers a view model for a file attribute type.
  /// </summary>
  public class FileListViewComponent: AttributeTypeViewComponentBase {

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
    | METHOD: INVOKE (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Assembles the view model for the <see cref="DefaultAttributeTypeViewComponent"/>.
    /// </summary>
    public async Task<IViewComponentResult> InvokeAsync(
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

      GetAttributeViewModel(model);

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      model!.Files              = GetFiles(model.InheritedValue, attribute);

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
    public List<SelectListItem> GetFiles(string inheritedValue, FileListAttributeTopicViewModel attribute) {

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
      string[] foundFiles = Directory.GetFiles(_webHostEnvironment.WebRootPath + attribute.Path, searchPattern, searchOption);

      if (!String.IsNullOrEmpty(inheritedValue)) {
        string inheritedValueKey = inheritedValue.Replace("." + attribute.Extension, "");
        files.Add(new SelectListItem("", inheritedValue));
      }
      foreach (string foundFile in foundFiles) {
        string fileName = foundFile.Replace(_webHostEnvironment.WebRootPath + attribute.Path, "");
        string fileNameKey = fileName.Replace("." + attribute.Extension, "");
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
