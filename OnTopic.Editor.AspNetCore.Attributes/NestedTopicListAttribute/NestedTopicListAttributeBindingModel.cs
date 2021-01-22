﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Components.BindingModels;

namespace OnTopic.Editor.AspNetCore.Attributes.NestedTopicListAttribute {

  /*============================================================================================================================
  | CLASS: NESTED TOPIC LIST ATTRIBUTE (BINDING MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a topic list attribute in the Topic Editor.
  /// </summary>
  public record NestedTopicListAttributeBindingModel : AttributeBindingModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="NestedTopicListAttributeBindingModel"/> class.
    /// </summary>
    public NestedTopicListAttributeBindingModel() : base() {
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