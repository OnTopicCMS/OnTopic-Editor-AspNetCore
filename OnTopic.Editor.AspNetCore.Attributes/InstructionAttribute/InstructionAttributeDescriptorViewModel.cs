/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.InstructionAttribute {

  /*============================================================================================================================
  | CLASS: INSTRUCTION ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="InstructionViewComponent"/>.
  /// </summary>
  public record InstructionAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="InstructionAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeValueDictionary"/> of attribute values.</param>
    public InstructionAttributeDescriptorViewModel(AttributeValueDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      Instructions              = attributes.GetValue(nameof(Instructions));
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="InstructionAttributeDescriptorViewModel"/> class.
    /// </summary>
    public InstructionAttributeDescriptorViewModel() : base() { }

    /*==========================================================================================================================
    | INSTRUCTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instructional text to embed in the OnTopic Editor.
    /// </summary>
    public string? Instructions { get; init; }

  } //Class
} //Namespace