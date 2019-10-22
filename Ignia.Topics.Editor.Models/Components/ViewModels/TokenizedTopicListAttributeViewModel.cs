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
  | CLASS: TOKENIZED TOPIC LIST ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="TokenizedTopicListViewComponent"/>. Additionally provides access to the
  ///   underlying <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the
  ///   currently selected <see cref="Topic"/>.
  /// </summary>
  public class TokenizedTopicListAttributeViewModel: AttributeViewModel<TokenizedTopicListOptions> {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="TokenizedTopicListAttributeViewModel"/> class.
    /// </summary>
    public TokenizedTopicListAttributeViewModel(
      AttributeDescriptorTopicViewModel attributeDescriptor,
      TokenizedTopicListOptions options,
      string value = null,
      string inheritedValue = null
    ): base(
      attributeDescriptor,
      options,
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
