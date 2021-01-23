/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models.Metadata;

#nullable enable

namespace OnTopic.Editor.AspNetCore.Attributes.DateTimeAttribute {

  /*============================================================================================================================
  | CLASS: DATE/TIME ATTRIBUTE DESCRIPTOR (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="DateTimeViewComponent"/>.
  /// </summary>
  public record DateTimeAttributeDescriptorTopicViewModel: AttributeDescriptorTopicViewModel {

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
    | DATE FORMAT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes the date format that the value should be stored in.
    /// </summary>
    public string? DateFormat { get; init; } = "yy-mm-dd";

    /*==========================================================================================================================
    | TIME FORMAT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes the time format that the value should be stored in. Defaults to "yyyy-mm-dd".
    /// </summary>
    public string? TimeFormat { get; init; } = "hh:mm tt";

    /*==========================================================================================================================
    | DATE/TIME SEPARATOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes the delimiter that should be use to separate the date and time format, if both are selected. Defaults to
    ///   "hh:mm TT".
    /// </summary>
    public string? DateTimeSeparator { get; init; } = " ";

    /*==========================================================================================================================
    | DATE/TIME OFFSET
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the relative offset that the default value should apply. Defaults to 0.
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

#nullable restore