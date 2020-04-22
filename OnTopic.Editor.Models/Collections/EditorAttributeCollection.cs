/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using OnTopic.Editor.Models.Components.BindingModels;

namespace OnTopic.Editor.Models.Collections {

  /*============================================================================================================================
  | CLASS: EDITOR ATTRIBUTE COLLECTION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a keyed collection of <see cref="AttributeBindingModel"/> instances.
  /// </summary>
  public class EditorAttributeCollection : KeyedCollection<string, AttributeBindingModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditorAttributeCollection"/> class.
    /// </summary>
    public EditorAttributeCollection() : base(StringComparer.InvariantCultureIgnoreCase) {
    }

    /*==========================================================================================================================
    | OVERRIDE: GET KEY FOR ITEM
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Method must be overridden for the EntityCollection to extract the keys from the items.
    /// </summary>
    /// <param name="item">The <see cref="Topic"/> object from which to extract the key.</param>
    /// <returns>The key for the specified collection item.</returns>
    protected override string GetKeyForItem(AttributeBindingModel item) {
      Contract.Assume(item != null, "Assumes the item is available when deriving its key.");
      return item.Key;
    }

  } // Class
} // Namespace