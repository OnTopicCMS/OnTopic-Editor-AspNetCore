/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Metadata;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to both a <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that
  ///   attribute from the currently selected <see cref="Topic"/>.
  /// </summary>
  public class AttributeViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeViewModel"/> class.
    /// </summary>
    public AttributeViewModel(
      AttributeDescriptorTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      AttributeDescriptor       = attributeDescriptor;
      Value                     = value;
      InheritedValue            = inheritedValue;

    }

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTE DESCRIPTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the global definition for the attribute, as defined on the corresponding <see cref="ContentType"/>.
    /// </summary>
    public AttributeDescriptorTopicViewModel AttributeDescriptor { get; set; }

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the associated attribute key, as defined on the <see cref="AttributeDescriptor"/> instance.
    /// </summary>
    /// <remarks>
    ///   While this value can be retrieved directly from the <see cref="AttributeDescriptor"/> property, relaying it through
    ///   the <see cref="AttributeViewModel"/> provides cleaner output in the forms by allowing the generated <c>id</c> and
    ///   <c>name</c> attributes to map to the target <see cref="EditorBindingModel"/> interface.
    /// </remarks>
    public string Key => AttributeDescriptor.Key;

    /*==========================================================================================================================
    | EDITOR TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the associated attribute type, as defined on the <see cref="AttributeDescriptor"/> instance.
    /// </summary>
    /// <remarks>
    ///   While this value can be retrieved directly from the <see cref="AttributeDescriptor"/> property, relaying it through
    ///   the <see cref="AttributeViewModel"/> provides cleaner output in the forms by allowing the generated <c>id</c> and
    ///   <c>name</c> attributes to map to the target <see cref="EditorBindingModel"/> interface.
    /// </remarks>
    public string EditorType => AttributeDescriptor.EditorType.Replace(".ascx", "");

    /*==========================================================================================================================
    | VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the current value, as defined on the <see cref="AttributeValue"/> instance.
    /// </summary>
    public string Value { get; set; }

    /*==========================================================================================================================
    | INHERITED VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the inherited value, as defined on either parent or derived topics.
    /// </summary>
    /// <remarks>
    ///   If the <see cref="Value"/> is set, then the <see cref="InhertedValue"/> will always be equal to the
    ///   <see cref="Value"/>.
    /// </remarks>
    public string InheritedValue { get; set; }

  } // Class

} // Namespace
