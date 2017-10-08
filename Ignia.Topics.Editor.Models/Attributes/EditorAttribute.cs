/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;

namespace Ignia.Topics.Editor.Models.Attributes {

  /*============================================================================================================================
  | CLASS: EDITOR ATTRIBUTE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a generic attribute in the Topic Editor.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class EditorAttribute {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="EditorAttribute"/> class, using the specified key/value pair.
    /// </summary>
    public EditorAttribute() {
    }

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The unique name associated with the specified attribute.
    /// </summary>
    public string Key {
      get;
      set;
    }

    /*==========================================================================================================================
    | VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The value associated with the attribute.
    /// </summary>
    public string Value {
      get;
      set;
    }

    /*==========================================================================================================================
    | GET VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the value associated with the attribute.
    /// </summary>
    /// <remarks>
    ///   Unlike the <see cref="Value"/> property, which simply returns the literal value associated with the attribute, the
    ///   <see cref="GetValue()"/> method is intended to be overwritten by derived versions of the <see cref="EditorAttribute"/>
    ///   class, in order to provide specific serialization instructions.
    /// </remarks>
    public virtual string GetValue() => Value;

  } //Class

} //Namespace
