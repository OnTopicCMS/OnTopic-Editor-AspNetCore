/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Microsoft.Extensions.DependencyInjection;

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
    | METHOD: ADD TOPIC EDITOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Ensures that dependencies of the topic editor are configured.
    /// </summary>
    public static void AddTopicEditor(this IServiceCollection services) {
      services.ConfigureOptions(typeof(EditorConfigureOptions));
    }

  } // Class
} // Namespace