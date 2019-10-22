/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models.Components.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ignia.Topics.Editor.Models.Components.ViewModels {

  /*============================================================================================================================
  | CLASS: TOPIC LIST ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="TopicListViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public class TopicListAttributeViewModel: AttributeViewModel<TopicListOptions> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TopicListAttributeViewModel"/> class.
    /// </summary>
    public TopicListAttributeViewModel(
      AttributeDescriptorTopicViewModel attributeDescriptor,
      TopicListOptions options,
      string value = null,
      string inheritedValue = null
    ): base(
      attributeDescriptor,
      options,
      value,
      inheritedValue
    ) { }

    /*==========================================================================================================================
    | IS NEW?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets whether or not this is an attribute of a new or existing topic.
    /// </summary>
    /// <remarks>
    ///   This property is important because it's not possible to retrieve or create nested topics under a topic that hasn't yet
    ///   been saved. In this case, much of the functionality of the <see cref="TopicListViewComponent"/> will be disabled until
    ///   the user has saved the new topic that they are creating.
    /// </remarks>
    public bool IsNew { get; set; }

    /*==========================================================================================================================
    | UNIQUE KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the <see cref="Topic.GetUniqueKey"/> value for the current topic.
    /// </summary>
    public string UniqueKey { get; set; }

    /*==========================================================================================================================
    | WEB PATH
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the <see cref="Topic.GetWebPath"/> value for the current topic.
    /// </summary>
    public string WebPath { get; set; }

  } // Class
} // Namespace
