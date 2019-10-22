/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models.Components.Options;
using Ignia.Topics.Metadata;
using System;

namespace Ignia.Topics.Editor.Models.Components.ViewModels {

  /*============================================================================================================================
  | CLASS: BOOLEAN ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="BooleanViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public class BooleanAttributeViewModel: AttributeViewModel<BooleanOptions> {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     string                          _value                          = null;
    private                     string                          _inheritedValue                 = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="BooleanAttributeViewModel"/> class.
    /// </summary>
    public BooleanAttributeViewModel(
      AttributeDescriptorTopicViewModel attributeDescriptor,
      BooleanOptions options,
      string value = null,
      string inheritedValue = null
    ): base(
      attributeDescriptor,
      options,
      value,
      inheritedValue
    ) {}

    /*==========================================================================================================================
    | IS SELECTED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the value is selected.
    /// </summary>
    public bool IsSelected() => (
      (Value?.Equals("1", StringComparison.OrdinalIgnoreCase)?? false) ||
      (Value?.Equals("true", StringComparison.InvariantCultureIgnoreCase)?? false)
    );

  } // Class

} // Namespace
