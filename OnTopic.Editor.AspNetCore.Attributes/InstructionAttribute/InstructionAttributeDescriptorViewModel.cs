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
    | INSTRUCTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instructional text to embed in the OnTopic Editor.
    /// </summary>
    public string? Instructions { get; init; }

  } //Class
} //Namespace