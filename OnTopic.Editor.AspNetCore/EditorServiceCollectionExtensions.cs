/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OnTopic.Editor.AspNetCore.Controllers;
using OnTopic.Editor.AspNetCore.Infrastructure;
using OnTopic.Internal.Diagnostics;

namespace OnTopic.Editor.AspNetCore {

  /*============================================================================================================================
  | CLASS: EDITOR SERVICE COLLECTION (EXTENSIONS)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Extension method to simplify configuration of the OnTopic Editor in an application.
  /// </summary>
  public static class EditorServiceCollectionExtensions {

    /*==========================================================================================================================
    | EXTENSION: ADD TOPIC EDITOR (IMVCBUILDER)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Configures the Razor engine to include model binders required by the OnTopic Editor.
    /// </summary>
    public static IMvcBuilder AddTopicEditor(this IMvcBuilder services) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(services, nameof(services));

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
    ///   Adds an MVC route for handling OnTopic Editor related requests, and maps it to the <see cref="EditorController"/>.
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