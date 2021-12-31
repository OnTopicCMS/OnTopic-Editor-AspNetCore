/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models.Metadata;

namespace OnTopic.Editor.AspNetCore.Attributes.NumberAttribute {

  /*============================================================================================================================
  | CLASS: NUMBER ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="NumberViewComponent"/>.
  /// </summary>
  public record NumberAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | PROPERTY: MINIMUM VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the lower bound for acceptable values. Defaults to <c>0</c>.
    /// </summary>
    public int MinimumValue { get; init; }

    /*==========================================================================================================================
    | PROPERTY: MAXIMUM VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the upper bound for acceptable values. Defaults to <see cref="Int32.MaxValue"/>.
    /// </summary>
    public int MaximumValue { get; init; } = Int32.MaxValue;

  } //Class
} //Namespace