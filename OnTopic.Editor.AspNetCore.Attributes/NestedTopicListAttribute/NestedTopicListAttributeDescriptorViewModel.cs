/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using OnTopic.Editor.AspNetCore.Models.Components;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.NestedTopicListAttribute {

  /*============================================================================================================================
  | CLASS: NESTED TOPIC LIST ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="NestedTopicListViewComponentView"/>.
  /// </summary>
  public record NestedTopicListAttributeDescriptorViewModel: ContentTypeListAttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="NestedTopicListAttributeDescriptorViewModel"/>
    /// </summary>
    public NestedTopicListAttributeDescriptorViewModel() {
      Scripts.Register(new("/_content/OnTopic.Editor.AspNetCore.Attributes/Shared/Scripts/NestedTopics.js", UriKind.Relative));
    }

  } //Class
} //Namespace

#nullable restore