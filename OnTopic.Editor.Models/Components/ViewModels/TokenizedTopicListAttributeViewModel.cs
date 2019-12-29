/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Metadata;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnTopic.Editor.Models.Components.ViewModels {

  /*============================================================================================================================
  | CLASS: TOKENIZED TOPIC LIST ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="TokenizedTopicListViewComponent"/>. Additionally provides access to the
  ///   underlying <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the
  ///   currently selected <see cref="Topic"/>.
  /// </summary>
  public class TokenizedTopicListAttributeViewModel: AttributeViewModel<TokenizedTopicListAttributeTopicViewModel> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TokenizedTopicListAttributeViewModel"/> class.
    /// </summary>
    public TokenizedTopicListAttributeViewModel(
      EditingTopicViewModel currentTopic,
      TokenizedTopicListAttributeTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) { }

    /*==========================================================================================================================
    | SELECTED TOPICS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a JSON formatted version of the attribute value. For use with TokenInput's <c>prePopulate</c> setting.
    /// </summary>
    public string SelectedTopics { get; set; }

  } // Class
} // Namespace
