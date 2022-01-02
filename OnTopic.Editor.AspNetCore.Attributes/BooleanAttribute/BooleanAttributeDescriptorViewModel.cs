/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.BooleanAttribute {

  /*============================================================================================================================
  | CLASS: BOOLEAN ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="BooleanViewComponent"/>.
  /// </summary>
  public record BooleanAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="BooleanAttributeDescriptorViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public BooleanAttributeDescriptorViewModel(AttributeDictionary attributes): base(attributes) { }

    /// <summary>
    ///   Initializes a new instance of the <see cref="BooleanAttributeDescriptorViewModel"/> class.
    /// </summary>
    public BooleanAttributeDescriptorViewModel(): base() {}

  } //Class
} //Namespace