/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Attributes.DateTimeAttribute {

  /*============================================================================================================================
  | CLASS: DATE/TIME ATTRIBUTE DESCRIPTOR (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="DateTimeViewComponent"/>.
  /// </summary>
  public record DateTimeAttributeDescriptorViewModel: AttributeDescriptorViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new <see cref="DateTimeAttributeDescriptorViewModel"/> with an <paramref name="attributes"/> dictionary.
    /// </summary>
    /// <param name="attributes">An <see cref="AttributeDictionary"/> of attribute values.</param>
    public DateTimeAttributeDescriptorViewModel(AttributeDictionary attributes): base(attributes) {
      Contract.Requires(attributes, nameof(attributes));
      IncludeDatePicker         = attributes.GetBoolean(nameof(IncludeDatePicker));
      IncludeTimePicker         = attributes.GetBoolean(nameof(IncludeTimePicker));
      DateTimeOffset            = attributes.GetInteger(nameof(DateTimeOffset));
      DateTimeOffsetUnits       = attributes.GetValue(nameof(DateTimeOffsetUnits))?? DateTimeOffsetUnits;

    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="DateTimeAttributeDescriptorViewModel"/> class.
    /// </summary>
    public DateTimeAttributeDescriptorViewModel() : base() { }

    /*==========================================================================================================================
    | INCLUDE DATE PICKER?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the date picker should be displayed.
    /// </summary>
    public bool? IncludeDatePicker { get; init; }

    /*==========================================================================================================================
    | INCLUDE TIME PICKER?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the time picker should be displayed.
    /// </summary>
    public bool? IncludeTimePicker { get; init; }

    /*==========================================================================================================================
    | DATE/TIME OFFSET
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the relative offset that the default value should apply. Defaults to <c>0</c>.
    /// </summary>
    public int? DateTimeOffset { get; init; }

    /*==========================================================================================================================
    | DATE/TIME OFFSET UNITS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the units that the offset direction is in. Defaults to "Days".
    /// </summary>
    public string? DateTimeOffsetUnits { get; init; } = "Days";

  } //Class
} //Namespace