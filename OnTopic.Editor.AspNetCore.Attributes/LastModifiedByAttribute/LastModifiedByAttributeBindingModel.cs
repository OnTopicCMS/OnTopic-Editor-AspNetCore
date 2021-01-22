﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Components.BindingModels;

namespace OnTopic.Editor.AspNetCore.Attributes.LastModifiedByAttribute {

  /*============================================================================================================================
  | CLASS: LAST MODIFIED BY ATTRIBUTE (BINDING MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a last modified by attribute in the Topic Editor.
  /// </summary>
  public record LastModifiedByAttributeBindingModel : AttributeBindingModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="LastModifiedByAttributeBindingModel"/> class.
    /// </summary>
    public LastModifiedByAttributeBindingModel() : base() {
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