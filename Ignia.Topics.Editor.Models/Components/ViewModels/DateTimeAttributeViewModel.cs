﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Editor.Models.Metadata;
using Ignia.Topics.Metadata;
using System;

namespace Ignia.Topics.Editor.Models.Components.ViewModels {

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
      DateTimeAttributeTopicViewModel attributeDescriptor,
      string value = null,
      string inheritedValue = null
    ): base(
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
        if (!String.IsNullOrEmpty(Value)) {
          DateTime dateValue;
          if (DateTime.TryParse(Value, out dateValue)) {
            _defaultDate        = dateValue.ToString(AttributeDescriptor.DateFormat);
          }
        }
        else {
          _defaultDate          = DateTime.Now.ToString(AttributeDescriptor.DateFormat);
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
          DateTime timeValue;
          if (DateTime.TryParse(Value, out timeValue)) {
            _defaultTime        = timeValue.ToString(AttributeDescriptor.TimeFormat);
          }
        }
        else {
          _defaultTime          = DateTime.Now.ToString(AttributeDescriptor.TimeFormat);
        }
      }
      return _defaultTime;
    }

  } // Class

} // Namespace
