/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models.Components.Options;
using Ignia.Topics.Metadata;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to both a <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that
  ///   attribute from the currently selected <see cref="Topic"/>.
  /// </summary>
  public class AttributeViewModel<T>: AttributeViewModel where T: DefaultOptions {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeViewModel"/> class.
    /// </summary>
    public AttributeViewModel(
      AttributeDescriptorTopicViewModel attributeDescriptor,
      T options,
      string value = null,
      string inheritedValue = null
    ): base(
      attributeDescriptor,
      value,
      inheritedValue
    ) {
      Options = options;
    }

    /*==========================================================================================================================
    | OPTIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Exposes the options associated with the specific attribute view model.
    /// </summary>
    public T Options { get; set; }

  } // Class

} // Namespace
