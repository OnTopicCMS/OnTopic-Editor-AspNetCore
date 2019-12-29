/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Mapping.Annotations;
using OnTopic.Metadata;
using OnTopic.ViewModels;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable

namespace OnTopic.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: TOPIC REFERENCE ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="TopicReferenceViewComponent"/>.
  /// </summary>
  public class TopicReferenceAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | PROPERTY: ROOT TOPIC KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a <see cref="Topic.GetUniqueKey"/> path representing the <see cref="RootTopic"/> to display to the
    ///   user. This allows relationships to be targeted to particular areas of the topic graph.
    /// </summary>
    [Obsolete(
      "This property is exposed exlusively for backward compatibility with the DefaultConfiguration's Scope property. New " +
      "attributes should instead use the RootTopic property. The RootTopicKey property will be removed in the future.",
      false
    )]
    public string? RootTopicKey { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ROOT TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets a <see cref="Topic.Id"/> representing the scope of <see cref="Topic"/>s to display to the user. This
    ///   allows relationships to be targeted to particular areas of the topic graph.
    /// </summary>
    [AttributeKey("RootTopicId")]
    [NotNull]
    public TopicViewModel? RootTopic { get; set; }

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
    public string? TargetContentType { get; set; }

  } //Class
} //Namespace

#nullable restore