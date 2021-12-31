/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

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
      FileListAttributeDescriptorViewModel attribute,
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
      var model                 = new FileListAttributeViewModel(currentTopic, attribute) {
        AbsolutePath            = _webHostEnvironment.ContentRootPath + attribute.Path
      };

      /*------------------------------------------------------------------------------------------------------------------------
      | Set model values
      \-----------------------------------------------------------------------------------------------------------------------*/
      foreach (var file in GetFiles(model)) {
        model.Files.Add(file);
      };

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
    public static Collection<SelectListItem> GetFiles(FileListAttributeViewModel viewModel) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(viewModel, nameof(viewModel));

      /*------------------------------------------------------------------------------------------------------------------------
      | INSTANTIATE OBJECTS
      \-----------------------------------------------------------------------------------------------------------------------*/
      var attribute             = viewModel.AttributeDescriptor;
      var absolutePath          = viewModel.AbsolutePath;
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

      /*------------------------------------------------------------------------------------------------------------------------
      | Set label
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (!String.IsNullOrEmpty(viewModel.InheritedValue)) {
        setLabel(viewModel.InheritedValue, "inherited value");
      }
      else if (!String.IsNullOrEmpty(viewModel.AttributeDescriptor.DefaultValue)) {
        setLabel(viewModel.AttributeDescriptor.DefaultValue, "default value");
      }
      else if (!String.IsNullOrEmpty(viewModel.AttributeDescriptor.ImplicitValue)) {
        setLabel(viewModel.AttributeDescriptor.ImplicitValue, "implicit default");
      }
      else {
        setLabel("Select a file…");
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

      /*------------------------------------------------------------------------------------------------------------------------
      | Function: Set Label
      \-----------------------------------------------------------------------------------------------------------------------*/
      void setLabel(string value, string? contextualLabel = null) {
        var label = value;
        if (contextualLabel is not null) {
          label += " (" + contextualLabel + ")";
        }
        viewModel.Files.Add(
          new() {
            Value = "",
            Text = label
          }
        );
      }

    }

  } // Class
} // Namespace