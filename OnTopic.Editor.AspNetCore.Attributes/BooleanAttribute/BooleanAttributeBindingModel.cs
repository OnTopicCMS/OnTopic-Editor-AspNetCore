/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Components.BindingModels;

namespace OnTopic.Editor.AspNetCore.Attributes.BooleanAttribute {

  /*============================================================================================================================
  | CLASS: BOOLEAN ATTRIBUTE (BINDING MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of a boolean attribute in the Topic Editor.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public record BooleanAttributeBindingModel : AttributeBindingModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="BooleanAttributeBindingModel"/> class.
    /// </summary>
    public BooleanAttributeBindingModel() : base() {
    }

    /*==========================================================================================================================
    | GET VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves the value associated with the attribute.
    /// </summary>
    /// <remarks>
    ///   Determines whether the value is checked and, if it is, returns "1"; otherwise returns "0".
    /// </remarks>
    public override string GetValue() => Value is "True"? "1" : "0";

  } // Class
} // Namespace