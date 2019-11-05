/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models.Metadata;
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
      EditingTopicViewModel currentTopic,
      AttributeDescriptorTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      CurrentTopic              = currentTopic;
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
    public AttributeDescriptorTopicViewModel AttributeDescriptor { get; }

    /*==========================================================================================================================
    | PROPERTY: CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="EditingTopicViewModel"/> that the user is currently editing.
    /// </summary>
    public EditingTopicViewModel CurrentTopic { get; }

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
    ///   Provides the associated attribute type, as implied by the <see cref="Topic.ContentType"/> of the <see
    ///   cref="AttributeDescriptor"/>.
    /// </summary>
    public string EditorType => AttributeDescriptor.ContentType;

    /*==========================================================================================================================
    | TOPID ID
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the <see cref="Topic.Id"/>, assuming the <see cref="Topic"/> has already been saved; otherwise will return
    ///   <c>-1</c>.
    /// </summary>
    public int TopicId { get; set; }

    /*==========================================================================================================================
    | VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the current value, as defined on the <see cref="AttributeValue"/> instance.
    /// </summary>
    public virtual string Value { get; set; }

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
    public virtual string InheritedValue { get; set; }

    /*==========================================================================================================================
    | IS ENABLED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the field should be enabled, as defined on the <see cref="AttributeValue"/> instance.
    /// </summary>
    public bool IsEnabled {
      get => AttributeDescriptor.GetBooleanConfigurationValue(
        "IsEnabled",
        AttributeDescriptor.GetBooleanConfigurationValue("Enabled", AttributeDescriptor.IsEnabled)
      );
      set {
        AttributeDescriptor.IsEnabled = value;
      }
    }

    /*==========================================================================================================================
    | CSS CLASS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines the CSS class names to be used, if any are configured.
    /// </summary>
    public string CssClass {
      get => AttributeDescriptor.GetConfigurationValue(
        "CssClass",
        AttributeDescriptor.GetConfigurationValue("CssClassField", AttributeDescriptor.CssClass)
      );
      set {
        AttributeDescriptor.CssClass = value;
      }
    }

  } // Class

} // Namespace
