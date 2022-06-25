/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.NumberAttribute {

  /*============================================================================================================================
  | CLASS: NUMBER ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="NumberViewComponent"/>.
  /// </summary>
  public record NumberAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="NumberAttributeDescriptorViewModel"/> with an <paramref name="attributes"/>
    ///   dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeValueDictionary"/> of attribute values.</param>
    public NumberAttributeDescriptorViewModel(AttributeValueDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      MinimumValue              = attributes.GetInteger(nameof(MinimumValue))?? MinimumValue;
      MaximumValue              = attributes.GetInteger(nameof(MaximumValue))?? MaximumValue;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="NumberAttributeDescriptorViewModel"/> class.
    /// </summary>
    public NumberAttributeDescriptorViewModel() : base() { }

    /*==========================================================================================================================
    | PROPERTY: MINIMUM VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the lower bound for acceptable values. Defaults to <c>0</c>.
    /// </summary>
    public int MinimumValue { get; init; }

    /*==========================================================================================================================
    | PROPERTY: MAXIMUM VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the upper bound for acceptable values. Defaults to <see cref="Int32.MaxValue"/>.
    /// </summary>
    public int MaximumValue { get; init; } = Int32.MaxValue;

  } //Class
} //Namespace