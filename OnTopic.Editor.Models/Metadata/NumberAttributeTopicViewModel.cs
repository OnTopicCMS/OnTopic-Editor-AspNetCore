/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

using System;

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: NUMBER ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="NumberViewComponentView"/>.
  /// </summary>
  public class NumberAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: MINIMUM VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the lower bound for acceptable values. Defaults to 0.
    /// </summary>
    public int MinimumValue { get; set; } = 0;

    /*==========================================================================================================================
    | PROPERTY: MAXIMUM VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the upper bound for acceptable values. Defaults to <see cref="Int32.Max"/>.
    /// </summary>
    public int MaximumValue { get; set; } = Int32.MaxValue;

  } //Class
} //Namespace

#nullable restore