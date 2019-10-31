/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Mapping;
using Ignia.Topics.Metadata;
using System;

#nullable enable

namespace Ignia.Topics.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: TOPIC REFERENCE ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TopicReferenceViewComponent"/>.
  /// </summary>
  public class TopicReferenceAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | SCOPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the scope of the topic graph within which to search for results. E.g., <c>Root:Web:Configuration</c>.
    /// </summary>
    public string? Scope { get; set; }

    /*==========================================================================================================================
    | RESULT LIMIT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the maximum number of <see cref="Topic"/> results to pull from the web service.
    /// </summary>
    public int? ResultLimit { get; set; }

    /*==========================================================================================================================
    | TARGET CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the <see cref="Topic.Key"/> of the <see cref="ContentTypeDescriptor"/> to filter results by.
    /// </summary>
    [AttributeKey("ContentType")]
    public string? TargetContentType { get; set; }

  } //Class
} //Namespace

#nullable restore