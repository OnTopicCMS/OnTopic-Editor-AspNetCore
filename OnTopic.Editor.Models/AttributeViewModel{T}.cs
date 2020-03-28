/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Metadata;

namespace OnTopic.Editor.Models {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to both a <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that
  ///   attribute from the currently selected <see cref="Topic"/>.
  /// </summary>
  public class AttributeViewModel<T>: AttributeViewModel where T: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeViewModel"/> class.
    /// </summary>
    public AttributeViewModel(
      EditingTopicViewModel currentTopic,
      T attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
      currentTopic,
      attributeDescriptor
    ) {
      AttributeDescriptor       = attributeDescriptor;
      Value                     = value?? Value;
      InheritedValue            = inheritedValue?? InheritedValue;
    }

    /*==========================================================================================================================
    | PROPERTY: ATTRIBUTE TYPE DESCRIPTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the global definition for the specific attribute type, as defined on the corresponding <see
    ///   cref="ContentType"/>. This differs from the <see cref="AttributeDescriptor"/> in that it provides access to additional
    ///   properties that are specific to the attribute type.
    /// </summary>
    /// <remarks>
    ///   This implementation of <see cref="AttributeDescriptor"/> hides the underlying implementation of <see
    ///   cref="AttributeViewModel.AttributeDescriptor"/>, replacing it with a strongly typed derivative. This effectively
    ///   mimics return type covariance. To ensure that this is safe, the <see cref="AttributeDescriptor"/> value can only be
    ///   set via the constructor—which simultaneously sets the underlying <see cref="AttributeViewModel.AttributeDescriptor"/>
    ///   with the <i>same</i> object. That way, if the object is cast as a <see cref="AttributeViewModel"/> (e.g., in shared
    ///   views) it will have access to the general <see cref="AttributeDescriptorTopicViewModel"/>, where as if it cast as a
    ///   <see cref="AttributeViewModel{T}"/>, then it will get the strongly-typed derivative at the same location. In either
    ///   case, the <see cref="AttributeDescriptor"/> can be explicitly cast to the general or derived version, since it's
    ///   guaranteed to be an instance of the derived class.
    /// </remarks>
    public new T AttributeDescriptor { get; }

  } // Class

} // Namespace
