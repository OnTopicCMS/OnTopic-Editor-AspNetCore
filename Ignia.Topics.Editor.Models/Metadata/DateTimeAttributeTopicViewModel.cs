/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;

namespace Ignia.Topics.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: DATE/TIME ATTRIBUTE (TOPIC VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to attributes associated with the <see cref="DateTimeViewComponent"/>.
  /// </summary>
  public class DateTimeAttributeTopicViewModel: AttributeDescriptorTopicViewModel {

    /*==========================================================================================================================
    | INCLUDE DATE PICKER?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the date picker should be displayed.
    /// </summary>
    public bool IncludeDatePicker { get; set; }

    /*==========================================================================================================================
    | INCLUDE TIME PICKER?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether or not the time picker should be displayed.
    /// </summary>
    public bool IncludeTimePicker { get; set; }

    /*==========================================================================================================================
    | DATE FORMAT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes the date format that the value should be stored in.
    /// </summary>
    public string DateFormat { get; set; }

    /*==========================================================================================================================
    | TIME FORMAT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes the time format that the value should be stored in. Defaults to "mm/dd/yy".
    /// </summary>
    public string TimeFormat { get; set; }

    /*==========================================================================================================================
    | DATE/TIME SEPARATOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes the delimiter that should be use to separate the date and time format, if both are selected. Defaults to
    ///   "hh:mm TT".
    /// </summary>
    public string DateTimeSeparator { get; set; }

  } // Class

} // Namespace
