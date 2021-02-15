/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using System.Linq;

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
    ///   Registers a client-side resource.
    /// </summary>
    public void Register(Uri url, bool inHeader = false, bool isDeferred = true) {
      if (!Contains(url)) {
        Add(
          new() {
            Url                 = url,
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