/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Diagnostics.CodeAnalysis;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.AspNetCore.Models.Metadata;

namespace OnTopic.Editor.AspNetCore.Attributes.BooleanAttribute {

  /*============================================================================================================================
  | CLASS: BOOLEAN ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="BooleanViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public record BooleanAttributeViewModel: AttributeViewModel<BooleanAttributeDescriptorViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="BooleanAttributeViewModel"/> class.
    /// </summary>
    public BooleanAttributeViewModel(
      EditingTopicViewModel currentTopic,
      BooleanAttributeDescriptorViewModel attributeDescriptor,
      string? value = null,
      string? inheritedValue = null
    ): base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) {}

    /*==========================================================================================================================
    | IS TRUE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the value is explicitly set to true.
    /// </summary>
    public bool? IsTrue() {
      if (IsBoolean(Value, out var value) && value.Value) {
        return true;
      }
      return null;
    }

    /*==========================================================================================================================
    | IS FALSE?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether value is explicitly set to false.
    /// </summary>
    public bool? IsFalse() {
      if (IsBoolean(Value, out var value) && !value.Value) {
        return true;
      }
      return null;
    }

    /*==========================================================================================================================
    | IS BOOLEAN?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the <paramref name="input"/> is a <see cref="Boolean"/>. If it is, converts it to a <see cref="Boolean"
    ///   /> via the <paramref name="value"/>.
    /// </summary>
    public static bool IsBoolean(string? input, [NotNullWhen(true)] out bool? value) {
      value = null;
      if (String.IsNullOrEmpty(input)) {
        return false;
      }
      if (
        input.Equals("1", StringComparison.OrdinalIgnoreCase) ||
        input.Equals("true", StringComparison.OrdinalIgnoreCase)
      ) {
        value = true;
        return true;
      }
      if (
        input.Equals("0", StringComparison.OrdinalIgnoreCase) ||
        input.Equals("false", StringComparison.OrdinalIgnoreCase)
      ) {
        value = false;
        return true;
      }
      return false;
    }


  } // Class
} // Namespace