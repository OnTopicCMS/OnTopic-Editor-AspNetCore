/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.LastModifiedByAttribute {

  /*============================================================================================================================
  | CLASS: LAST MODIFIED BY ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="LastModifiedByViewComponent"/>.
  /// </summary>
  public record LastModifiedByAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="LastModifiedByAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public LastModifiedByAttributeDescriptorViewModel(AttributeDictionary attributes): base(attributes) { }

    /// <summary>
    ///   Initializes a new instance of the <see cref="LastModifiedByAttributeDescriptorViewModel"/> class.
    /// </summary>
    public LastModifiedByAttributeDescriptorViewModel() : base() { }

  } //Class
} //Namespace