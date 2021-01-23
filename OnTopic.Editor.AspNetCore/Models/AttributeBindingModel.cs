/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE (BINDING MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a generic attribute in the Topic Editor.
  /// </summary>
  public record AttributeBindingModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeBindingModel"/> class.
    /// </summary>
    public AttributeBindingModel() : this("") {}

    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeBindingModel"/> class.
    /// </summary>
    /// <param name="editorType">Optionally defines the type of attribute.</param>
    public AttributeBindingModel(string editorType) {
      if (String.IsNullOrWhiteSpace(editorType)) {
        EditorType = GetType().Name.Replace("AttributeBindingModel", "", StringComparison.Ordinal);
      }
      else {
        EditorType = editorType;
      }
    }

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The unique name associated with the specified attribute.
    /// </summary>
    public string Key {
      get;
      init;
    }

    /*==========================================================================================================================
    | EDITOR TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The editor type associated with the attribute.
    /// </summary>
    public string EditorType {
      get;
      init;
    }

    /*==========================================================================================================================
    | VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The value associated with the attribute.
    /// </summary>
    public string Value {
      get;
      init;
    }

    /*==========================================================================================================================
    | GET VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the value associated with the attribute.
    /// </summary>
    /// <remarks>
    ///   Unlike the <see cref="Value"/> property, which simply returns the literal value associated with the attribute, the
    ///   <see cref="GetValue()"/> method is intended to be overwritten by derived versions of the <see cref="AttributeBindingModel"/>
    ///   class, in order to provide specific serialization instructions.
    /// </remarks>
    public virtual string GetValue() => Value;

  } // Class
} // Namespace