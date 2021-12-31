/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Collections.ObjectModel;

namespace OnTopic.Editor.AspNetCore.Models.ClientResources {

  /*============================================================================================================================
  | CLASS: SCRIPT COLLECTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a collection of <see cref="ScriptResource"/> instances.
  /// </summary>
  public class ScriptCollection: ClientResourceCollection<ScriptResource> {

    /*==========================================================================================================================
    | METHOD: REGISTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Registers a client-side script, optionally placing it <paramref name="inHeader"/> or making sure it is <paramref name=
    ///   "isDeferred"/>.
    /// </summary>
    /// <param name="uri">The relative or absolute URL of the client-side script.</param>
    /// <param name="inHeader">Specifies that the script should be placed in the HTML header, instead of the footer.</param>
    /// <param name="isDeferred">
    ///   Specifies that the script execution should be deferred, instead of executed immediately.
    /// </param>
    public void Register(Uri uri, bool inHeader, bool isDeferred = true) {
      if (!Contains(uri)) {
        Add(
          new() {
            Url                 = uri,
            InHeader            = inHeader,
            IsDeferred          = isDeferred
          }
        );
      }
    }

    /*==========================================================================================================================
    | METHOD: GET RESOURCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a collection of resources, optionally filtering by
    /// </summary>
    public ReadOnlyCollection<ScriptResource> GetResources(bool inHeader, bool? isDeferred = null) =>
      new(GetResources().Where(r => r.InHeader == inHeader && (isDeferred is null || r.IsDeferred == isDeferred)).ToList());

  }
}