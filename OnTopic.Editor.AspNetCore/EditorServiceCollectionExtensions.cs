/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OnTopic.Editor.AspNetCore.Infrastructure;
using OnTopic.Internal.Diagnostics;

namespace OnTopic.Editor.AspNetCore {

  /*============================================================================================================================
  | CLASS: EDITOR SERVICE COLLECTION (EXTENSIONS)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Extension method to simplify configuration of the editor in an application. Automatically sets up e.g. <see
  ///   cref="EditorConfigureOptions"/>.
  /// </summary>
  public static class EditorServiceCollectionExtensions {

    /*==========================================================================================================================
    | EXTENSION: ADD TOPIC EDITOR (IMVCBUILDER)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Configures the Razor engine to include OnTopic model binders.
    /// </summary>
    public static IMvcBuilder AddTopicEditor(this IMvcBuilder services) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires<ArgumentNullException>(services != null, nameof(services));

      /*------------------------------------------------------------------------------------------------------------------------
      | Register services
      \-----------------------------------------------------------------------------------------------------------------------*/
      services.AddMvcOptions(options =>
        options.ModelBinderProviders.Insert(0, new AttributeBindingModelBinderProvider())
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Return services for fluent API
      \-----------------------------------------------------------------------------------------------------------------------*/
      return services;
    }

    /*==========================================================================================================================
    | EXTENSION: MAP TOPIC ROUTE (IENDPOINTROUTEBUILDER)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Adds an MVC route for handling OnTopic related requests, and maps it to the <see cref="EditorController"/> by default.
    /// </summary>
    public static ControllerActionEndpointConventionBuilder MapTopicEditorRoute(
      this IEndpointRouteBuilder routes
    ) =>
      routes.MapAreaControllerRoute(
        name: "TopicEditor",
        areaName: "Editor",
        pattern: "OnTopic/{action}/{**path}",
        defaults: new { controller = "Editor", action="Edit", path = "Root" }
      );

  } // Class
} // Namespace