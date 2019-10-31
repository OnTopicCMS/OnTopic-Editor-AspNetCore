/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

#nullable enable

namespace Ignia.Topics.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: FILE PATH ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="FilePathViewComponent"/>.
  /// </summary>
  public class FilePathAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: TRUNCATE PATH AT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the Topic level (based on <see cref="Topic.Key"/>) at which to stop the recursive processing logic for
    ///   <see cref="InheritedValue"/>. If set, the <see cref="InheritedValue"/> will ignore children under the specified
    ///   <see cref="Topic"/> when formulating the full file path.
    /// </summary>
    public string? TruncatePathAtTopic { get; set; }

    /*==========================================================================================================================
    | PROPERTY: INCLUDE LEAF TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the current leaf <see cref="Topic"/> should be included in the <see cref="InheritedValue"/>.
    /// </summary>
    public bool? IncludeLeafTopic { get; set; }

    /*==========================================================================================================================
    | PROPERTY: INHERIT VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the <see cref="Value"/> is expected to be inherited from parent topics if left blank.
    /// </summary>
    public bool? InheritValue { get; set; }

    /*==========================================================================================================================
    | PROPERTY: RELATIVE TO PARENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the <see cref="Value"/> should automatically inject any parent topics in the path. If set, the
    ///   value will be set to the inherited value (if present) along with the path between the level at which that value is set
    ///   and the current topic.
    /// </summary>
    public bool? RelativeToParent { get; set; }

  } //Class
} //Namespace

#nullable restore