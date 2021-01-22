﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Components.BindingModels;

namespace OnTopic.Editor.AspNetCore.Attributes.FilePathAttribute {

  /*============================================================================================================================
  | CLASS: FILE PATH ATTRIBUTE (BINDING MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a file path attribute in the Topic Editor.
  /// </summary>
  public record FilePathAttributeBindingModel : AttributeBindingModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="FilePathAttributeBindingModel"/> class.
    /// </summary>
    public FilePathAttributeBindingModel() : base() {
    }

    /*==========================================================================================================================
    | GET VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the value associated with the attribute.
    /// </summary>
    public override string GetValue() => Value;

  } // Class
} // Namespace