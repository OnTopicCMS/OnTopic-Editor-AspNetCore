/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using OnTopic.Editor.Models.Metadata;

namespace OnTopic.Editor.Models.Components.ViewModels {

  /*============================================================================================================================
  | CLASS: DATE/TIME  ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="DateTimeViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public class DateTimeAttributeViewModel: AttributeViewModel<DateTimeAttributeTopicViewModel> {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     string                          _defaultDate                    = null;
    private                     string                          _defaultTime                    = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="DateTimeSelectorAttributeViewModel"/> class.
    /// </summary>
    public DateTimeAttributeViewModel(
      EditingTopicViewModel currentTopic,
      DateTimeAttributeTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) {}

    /*==========================================================================================================================
    | METHOD: GET DEFAULT DATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Parses the <see cref="Value"/> and returns the date portion in the format expected by the date/time plugin.
    /// </summary>
    public string GetDefaultDate() {
      if (String.IsNullOrEmpty(_defaultDate)) {
        //Convert from JavaScript date format conventions to C# conventions
        var dateFormat = AttributeDescriptor.DateFormat.Replace("y", "yy").Replace("mm", "MM");
        if (!String.IsNullOrEmpty(Value)) {
          if (DateTime.TryParse(Value, out var dateValue)) {
            _defaultDate        = dateValue.ToString(dateFormat);
          }
        }
        else {
          _defaultDate          = CalculateOffset(DateTime.Now).ToString(dateFormat);
        }
      }
      return _defaultDate;
    }

    /*==========================================================================================================================
    | METHOD: GET DEFAULT TIME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Parses the <see cref="Value"/> and returns the date portion in the format expected by the date/time plugin.
    /// </summary>
    public string GetDefaultTime() {
      if (String.IsNullOrEmpty(_defaultTime)) {
        if (!String.IsNullOrEmpty(Value)) {
          if (DateTime.TryParse(Value, out var timeValue)) {
            _defaultTime        = timeValue.ToString(AttributeDescriptor.TimeFormat);
          }
        }
        else {
          _defaultTime          = CalculateOffset(DateTime.Now).ToString(AttributeDescriptor.TimeFormat);
        }
      }
      return _defaultTime;
    }

    /*==========================================================================================================================
    | METHOD: CALCULATE OFFSET
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Given a date, applies any offsets applied to the date and time.
    /// </summary>
    public DateTime CalculateOffset(DateTime originalDate) {
      var offset = AttributeDescriptor.DateTimeOffset?? 0;
      if (AttributeDescriptor.DateTimeOffset is 0) return originalDate;
      return AttributeDescriptor.DateTimeOffsetUnits switch {
        "Minutes"               => originalDate.AddMinutes(offset),
        "Hours"                 => originalDate.AddHours(offset),
        "Days"                  => originalDate.AddDays(offset),
        "Months"                => originalDate.AddMonths(offset),
        "Years"                 => originalDate.AddYears(offset),
        _                       => originalDate.AddDays(offset),
      };
      ;

    }

  } // Class
} // Namespace