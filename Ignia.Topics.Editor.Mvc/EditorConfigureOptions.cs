/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using Ignia.Topics.Internal.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Ignia.Topics.Editor.Mvc {

  /*============================================================================================================================
  | CLASS: EDITOR (CONFIGURE OPTIONS)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Used to configure an MVC application to use embedded resources from this Razor Class Library (RCL).
  /// </summary>
  public class EditorConfigureOptions : IPostConfigureOptions<StaticFileOptions> {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            IHostingEnvironment             _environment                    = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="EditorConfigureOptions"/> with necessary dependencies.
    /// </summary>
    /// <returns>A topic controller for loading OnTopic views.</returns>
    public EditorConfigureOptions(IHostingEnvironment environment) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(environment != null, "A concrete implementation of an IHostingEnvironment is required.");

      /*------------------------------------------------------------------------------------------------------------------------
      | Set values locally
      \-----------------------------------------------------------------------------------------------------------------------*/
      _environment = environment;

    }

    /*==========================================================================================================================
    | METHOD: POST CONFIGURE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Extends the existing configuration with options specific to the editor.
    /// </summary>
    public void PostConfigure(string name, StaticFileOptions options) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate dependencies
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (options.FileProvider == null && _environment.WebRootFileProvider == null) {
        throw new InvalidOperationException("Missing FileProvider.");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Ensure basic options are configured
      \-----------------------------------------------------------------------------------------------------------------------*/
      options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
      options.FileProvider = options.FileProvider ?? _environment.WebRootFileProvider;

      /*------------------------------------------------------------------------------------------------------------------------
      | Add provider for embedded resources
      \-----------------------------------------------------------------------------------------------------------------------*/
      var fileProvider = new ManifestEmbeddedFileProvider(this.GetType().Assembly, "Resources");
      options.FileProvider = new CompositeFileProvider(options.FileProvider, fileProvider);

    }

  } // Class
} // Namespace