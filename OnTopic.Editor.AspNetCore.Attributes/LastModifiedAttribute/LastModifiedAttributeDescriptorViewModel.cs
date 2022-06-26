/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.LastModifiedAttribute {

  /*============================================================================================================================
  | CLASS: LAST MODIFIED ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="LastModifiedViewComponent"/>.
  /// </summary>
  public record LastModifiedAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="LastModifiedAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public LastModifiedAttributeDescriptorViewModel(AttributeDictionary attributes): base(attributes) { }

    /// <summary>
    ///   Initializes a new instance of the <see cref="LastModifiedAttributeDescriptorViewModel"/> class.
    /// </summary>
    public LastModifiedAttributeDescriptorViewModel() : base() { }

  } //Class
} //Namespace