/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using Ignia.Topics.Editor.Mvc.Infrastructure;
using Ignia.Topics.Internal.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ignia.Topics.Editor.Mvc {

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

  } // Class
} // Namespace