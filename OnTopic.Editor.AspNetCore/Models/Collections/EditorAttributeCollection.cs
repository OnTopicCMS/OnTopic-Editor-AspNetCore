/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Internal.Diagnostics;

namespace OnTopic.Editor.AspNetCore.Models.Collections {

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
    | METHOD: GET VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the attribute with a given <paramref name="attributeKey"/>, if present; otherwise returns the <paramref name
    ///   ="defaultValue"/>.
    /// </summary>
    /// <param name="attributeKey">The <see cref="AttributeBindingModel.Key"/> to retrieve.</param>
    /// <param name="defaultValue">
    ///   The default value to return if the <see cref="AttributeBindingModel"/> cannot be found.
    /// </param>
    public string GetValue(string attributeKey, string defaultValue = null) {
      if (Contains(attributeKey)) {
        return this[attributeKey].Value;
      }
      return defaultValue;
    }

    /*==========================================================================================================================
    | METHOD: GET INTEGER VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the attribute with a given <paramref name="attributeKey"/>, if present; otherwise returns the <paramref name
    ///   ="defaultValue"/>.
    /// </summary>
    /// <param name="attributeKey">The <see cref="AttributeBindingModel.Key"/> to retrieve.</param>
    /// <param name="defaultValue">
    ///   The default value to return if the <see cref="AttributeBindingModel"/> cannot be found.
    /// </param>
    public int? GetInteger(string attributeKey, int? defaultValue = null) {
      var stringValue = GetValue(attributeKey);
      if (!String.IsNullOrEmpty(stringValue) && Int32.TryParse(stringValue, out int result)) {
        return result;
      }
      return defaultValue;
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
      Contract.Assume(item, "Assumes the item is available when deriving its key.");
      return item.Key;
    }

  } // Class
} // Namespace