/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: INSTRUCTION ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="InstructionViewComponent"/>.
  /// </summary>
  public class InstructionAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | INSTRUCTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instructional text to embed in the OnTopic Editor.
    /// </summary>
    public string? Instructions { get; set; }

  } //Class
} //Namespace

#nullable restore