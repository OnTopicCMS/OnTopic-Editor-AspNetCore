/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using OnTopic.Internal.Diagnostics;

namespace OnTopic.Editor.AspNetCore.Models.ClientResources {

  /*============================================================================================================================
  | CLASS: CLIENT RESOURCE COLLECTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a collection of <typeparamref name="T"/> instances, which are derived from <see cref="ClientResource"/>.
  /// </summary>
  public abstract class ClientResourceCollection<T>: KeyedCollection<Uri, T> where T: ClientResource, new() {

    /*==========================================================================================================================
    | METHOD: REGISTER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Registers a client-side resource.
    /// </summary>
    /// <param name="uri">The relative or absolute URL so the client-side resource.</param>
    public void Register(Uri uri) {
      if (!Contains(uri)) {
        Add(
          new() {
            Url                 = uri
          }
        );
      }
    }

    /*==========================================================================================================================
    | METHOD: GET RESOURCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a collection of resources.
    /// </summary>
    public virtual ReadOnlyCollection<T> GetResources() => new(Items);

    /*==========================================================================================================================
    | OVERRIDE: GET KEY FOR ITEM
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Method must be overridden for the <see cref="ClientResourceCollection{T}"/> to extract the keys from the <typeparamref
    ///   name="T"/> instances.
    /// </summary>
    /// <param name="item">The <typeparamref name="T"/> object from which to extract the key.</param>
    /// <returns>The key for the specified collection item.</returns>
    protected override Uri GetKeyForItem(T item) {
      Contract.Assume(item, "Assumes the item is available when deriving its key.");
      return item.Url;
    }

  }
}