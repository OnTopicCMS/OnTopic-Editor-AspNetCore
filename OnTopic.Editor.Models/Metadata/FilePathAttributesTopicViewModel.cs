/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: FILE PATH ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="FilePathViewComponent"/>.
  /// </summary>
  public class FilePathAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: INHERIT VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the <see cref="Value"/> is expected to be inherited from parent topics if left blank.
    /// </summary>
    public bool? InheritValue { get; set; } = true;

    /*==========================================================================================================================
    | PROPERTY: RELATIVE TO TOPIC PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the <see cref="Value"/> should automatically inject any parent topics in the path. If set, the
    ///   value will be set to the inherited value (if present) along with the path between the level at which that value is set
    ///   and the current topic.
    /// </summary>
    public bool? RelativeToTopicPath { get; set; } = true;

    /*==========================================================================================================================
    | PROPERTY: BASE TOPIC PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the Topic level (based on <see cref="Topic.GetUniqueKey"/>) at which to stop the recursive processing
    ///   logic for <see cref="AttributeViewModel.InheritedValue"/>. If set, the <see cref="AttributeViewModel.InheritedValue"/>
    ///   will  ignore children under the specified <see cref="Topic"/> when formulating the full file path.
    /// </summary>
    public string? BaseTopicPath { get; set; } = "";

    /*==========================================================================================================================
    | PROPERTY: INCLUDE CURRENT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the current <see cref="Topic"/> should be included in the <see cref="InheritedValue"/>.
    /// </summary>
    public bool? IncludeCurrentTopic { get; set; } = true;

  } //Class
} //Namespace

#nullable restore