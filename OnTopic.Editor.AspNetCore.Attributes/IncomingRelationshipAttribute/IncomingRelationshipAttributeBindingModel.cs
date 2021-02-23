/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models;

namespace OnTopic.Editor.AspNetCore.Attributes.IncomingRelationshipAttribute {

  /*============================================================================================================================
  | CLASS: INCOMING RELATIONSHIP ATTRIBUTE (BINDING MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents an instance of an incoming relationship attribute in the Topic Editor.
  /// </summary>
  public record IncomingRelationshipAttributeBindingModel : AttributeBindingModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="IncomingRelationshipAttributeBindingModel"/> class.
    /// </summary>
    public IncomingRelationshipAttributeBindingModel() : base() {
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