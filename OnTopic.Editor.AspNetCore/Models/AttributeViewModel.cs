﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Attributes;

namespace OnTopic.Editor.AspNetCore.Models {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to both a <see cref="AttributeDescriptorViewModel"/> as well as the instance values for that attribute
  ///   from the currently selected <see cref="Topic"/>.
  /// </summary>
  public record AttributeViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeViewModel"/> class.
    /// </summary>
    public AttributeViewModel(
      EditingTopicViewModel currentTopic,
      AttributeDescriptorViewModel attributeDescriptor
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate parameters
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(currentTopic, nameof(currentTopic));
      Contract.Requires(attributeDescriptor, nameof(attributeDescriptor));

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      CurrentTopic              = currentTopic;
      AttributeDescriptor       = attributeDescriptor;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set values
      \-----------------------------------------------------------------------------------------------------------------------*/
      var key                   = AttributeDescriptor.Key!;
      var topic                 = CurrentTopic;

      Value                     = topic.Attributes.ContainsKey(key) ? topic.Attributes[key] : null;
      InheritedValue            = topic.InheritedAttributes.ContainsKey(key) ? topic.InheritedAttributes[key] : null;

    }

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTE DESCRIPTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the global definition for the attribute, as defined on the corresponding <see cref="ContentTypeDescriptor"/>.
    /// </summary>
    public AttributeDescriptorViewModel AttributeDescriptor { get; }

    /*==========================================================================================================================
    | PROPERTY: CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="EditingTopicViewModel"/> that the user is currently editing.
    /// </summary>
    public EditingTopicViewModel CurrentTopic { get; }

    /*==========================================================================================================================
    | VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the current value, as defined on the corresponding <see cref="AttributeRecord"/> instance.
    /// </summary>
    public virtual string? Value { get; init; }

    /*==========================================================================================================================
    | INHERITED VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the inherited value, as defined on either parent or derived topics.
    /// </summary>
    public string? InheritedValue { get; init; }

  } // Class
} // Namespace