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
  | CLASS: NESTED TOPIC LIST ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="NestedTopicListViewComponentView"/>.
  /// </summary>
  public record NestedTopicListAttributeDescriptorTopicViewModel: ContentTypeListAttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="NestedTopicListAttributeDescriptorTopicViewModel"/>
    /// </summary>
    public NestedTopicListAttributeDescriptorTopicViewModel() {
      Scripts.Register(new("/_content/OnTopic.Editor.AspNetCore.Attributes/Shared/Scripts/Scripts.js", UriKind.Relative));
    }

  } //Class
} //Namespace

#nullable restore