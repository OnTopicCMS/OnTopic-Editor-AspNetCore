/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Metadata;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.InstructionAttribute {

  /*============================================================================================================================
  | CLASS: INSTRUCTION ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="InstructionViewComponent"/>.
  /// </summary>
  public record InstructionAttributeDescriptorTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | INSTRUCTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instructional text to embed in the OnTopic Editor.
    /// </summary>
    public string? Instructions { get; init; }

  } //Class
} //Namespace

#nullable restore