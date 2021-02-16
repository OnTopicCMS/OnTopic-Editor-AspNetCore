/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Globalization;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Editor.AspNetCore.Models.Metadata;

namespace OnTopic.Editor.AspNetCore.Attributes.DateTimeAttribute {

  /*============================================================================================================================
  | CLASS: DATE/TIME  ATTRIBUTE (VIEW MODEL)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents the data model for the <see cref="DateTimeViewComponent"/>. Additionally provides access to the underlying
  ///   <see cref="AttributeDescriptorTopicViewModel"/> as well as the instance values for that attribute from the currently
  ///   selected <see cref="Topic"/>.
  /// </summary>
  public record DateTimeAttributeViewModel: AttributeViewModel<DateTimeAttributeDescriptorTopicViewModel> {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private                     string                          _defaultDate;
    private                     string                          _defaultTime;
    private readonly            IFormatProvider                 _format                         = CultureInfo.InvariantCulture;
    private                     DateTime?                       _value;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="DateTimeSelectorAttributeViewModel"/> class.
    /// </summary>
    public DateTimeAttributeViewModel(
      EditingTopicViewModel currentTopic,
      DateTimeAttributeDescriptorTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
      currentTopic,
      attributeDescriptor,
      value,
      inheritedValue
    ) {}

    /*==========================================================================================================================
    | PROPERTY: DATE/TIME VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the <see cref="Value"/> is set and, if so, returns that value as a <see cref="DateTime"/> object;
    ///   otherwise returns the current <see cref="DateTime"/>.
    /// </summary>
    public DateTime DateTimeValue {
      get {
        if (_value is not null) {
          return _value.Value;
        }
        else if (!String.IsNullOrEmpty(Value) && DateTime.TryParse(Value, out var dateTimeValue)) {
          _value = dateTimeValue;
        }
        else {
          _value = CalculateOffset(DateTime.Now);
        }
        return _value.Value;
      }
    }

    /*==========================================================================================================================
    | PROPERTY: FORMATTED VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Parses the <see cref="Value"/> and returns the full date and time in the format expected by the date/time control.
    /// </summary>
    public string FormattedValue {
      get {
        if (AttributeDescriptor.IncludeDatePicker is not null || AttributeDescriptor.IncludeTimePicker is not null) {
          if (AttributeDescriptor.IncludeDatePicker is true && AttributeDescriptor.IncludeTimePicker is true) {
            return DateTimeValue.ToString("o");
          }
          else if (AttributeDescriptor.IncludeDatePicker is true) {
            return GetDefaultDate();
          }
          else if (AttributeDescriptor.IncludeTimePicker is true) {
            return GetDefaultTime();
          };
        }
        return DateTimeValue.ToString("o");
      }
    }

    /*==========================================================================================================================
    | METHOD: GET DEFAULT DATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Parses the <see cref="Value"/> and returns the date portion in the format expected by the date/time plugin.
    /// </summary>
    public string GetDefaultDate() {
      if (String.IsNullOrEmpty(_defaultDate)) {
        //Convert from JavaScript date format conventions to C# conventions
        var dateFormat          = AttributeDescriptor.DateFormat
          .Replace("y", "yy", StringComparison.Ordinal)
          .Replace("mm", "MM", StringComparison.Ordinal);
        _defaultDate            = DateTimeValue.ToString(dateFormat, _format);
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
        _defaultTime            = DateTimeValue.ToString(AttributeDescriptor.TimeFormat, _format);
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