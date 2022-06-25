/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.ReflexiveAttribute {

  /*============================================================================================================================
  | CLASS: REFLEXIVE ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="ReflexiveViewComponent"/>.
  /// </summary>
  public record ReflexiveAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="ReflexiveAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeValueDictionary"/> of attribute values.</param>
    public ReflexiveAttributeDescriptorViewModel(AttributeValueDictionary attributes): base(attributes) { }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ReflexiveAttributeDescriptorViewModel"/> class.
    /// </summary>
    public ReflexiveAttributeDescriptorViewModel() : base() { }

  } //Class
} //Namespace