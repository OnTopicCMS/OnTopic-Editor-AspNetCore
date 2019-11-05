/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using Ignia.Topics.Metadata;

namespace Ignia.Topics.Editor.Models.Metadata {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE DESCRIPTOR TOPIC VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides core properties from a <see cref="AttributeDescriptor"/> to a view component.
  /// </summary>
  public class AttributeDescriptorTopicViewModel: ViewModels.TopicViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeDescriptorTopicViewModel"/> class.
    /// </summary>
    public AttributeDescriptorTopicViewModel() {}

    /*==========================================================================================================================
    | PROPERTY: DESCRIPTION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a friendly description for the <see cref="AttributeDescriptor"/>, intended as documentation for users of the
    ///   editor.
    /// </summary>
    public string Description { get; set; }

    /*==========================================================================================================================
    | PROPERTY: MODEL TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines how the attribute is modeled in terms of the object-oriented code (e.g., as a relationship? An attribute?).
    /// </summary>
    public virtual ModelType ModelType { get; set; }

    /*==========================================================================================================================
    | PROPERTY: DISPLAY GROUP
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines what group of attributes to associate the current attribute with.
    /// </summary>
    public string DisplayGroup { get; set; }

    /*==========================================================================================================================
    | PROPERTY: DEFAULT CONFIGURATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the raw string representation of any optional values needed to configure the attribute editor.
    /// </summary>
    public string DefaultConfiguration { get; set; }

    /*==========================================================================================================================
    | PROPERTY: CONFIGURATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a parsed version of the <see cref="DefaultConfiguration"/>, with key/value pairs being represented in a
    ///   dictionary for easy reference.
    /// </summary>
    public IDictionary<string, string> Configuration { get; set; }

    /*==========================================================================================================================
    | METHOD: GET CONFIGURATION VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a configuration value from the <see cref="Configuration"/> dictionary; if the value doesn't exist, then
    ///   optionally returns a default value.
    /// </summary>
    public string GetConfigurationValue(string key, string defaultValue = null) {
      if (Configuration != null && Configuration.ContainsKey(key) && Configuration[key] != null) {
        return Configuration[key].ToString();
      }
      return defaultValue;
    }

    /*==========================================================================================================================
    | METHOD: GET BOOLEAN CONFIGURATION VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a configuration value from the <see cref="Configuration"/> dictionary as a boolean value.
    /// </summary>
    public bool GetBooleanConfigurationValue(string key, bool defaultValue = false) {
      if (Boolean.TryParse(GetConfigurationValue(key, defaultValue.ToString()), out var value)) {
        return value;
      }
      return defaultValue;
    }

    /*==========================================================================================================================
    | METHOD: GET INTEGER CONFIGURATION VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a configuration value from the <see cref="Configuration"/> dictionary as an integer value.
    /// </summary>
    public int GetIntegerConfigurationValue(string key, int defaultValue = 0) {
      if (Int32.TryParse(GetConfigurationValue(key, defaultValue.ToString()), out var value)) {
        return value;
      }
      return defaultValue;
    }

    /*==========================================================================================================================
    | PROPERTY: IS REQUIRED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the attribute should be considered required or not.
    /// </summary>
    public bool IsRequired { get; set; }

    /*==========================================================================================================================
    | PROPERTY: DEFAULT VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines a default value for new topics.
    /// </summary>
    /// <remarks>
    ///   Assuming a topic is new, and isn't derived from another topic, the <see cref="DefaultValue"/> should be treated as the
    ///   value. This helps establish a logical default, but also prevents values from being inherited from e.g. parent topics;
    ///   as such, this should be used with caution. The value used here will be stored locally on a per-topic basis.
    /// </remarks>
    public string DefaultValue { get; set; }

    /*==========================================================================================================================
    | PROPERTY: IMPLICIT VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines the value that the code is expected to assign if an attribute's value is left empty.
    /// </summary>
    /// <remarks>
    ///   The <see cref="ImplicitValue"/> is not actually used by code to set the value. It is simply a way of communicating to
    ///   editors what value it is <i>expected</i> code will use. In practice, code may set any default it wishes, and may even
    ///   set different defaults in different contexts (e.g., based on different views). The <see cref="ImplicitValue"/> will be
    ///   exposed to editors as an HTML placeholder on input fields that support it.
    /// </remarks>
    public string ImplicitValue { get; set; }

    /*==========================================================================================================================
    | PROPERTY: SORT ORDER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines the attribute's prioritization in the page order.
    /// </summary>
    public int SortOrder { get; set; }

    /*==========================================================================================================================
    | IS ENABLED
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines whether the field should be enabled, as defined on the <see cref="AttributeValue"/> instance.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /*==========================================================================================================================
    | CSS CLASS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Defines the CSS class names to be used, if any are configured.
    /// </summary>
    public string CssClass { get; set; }

  } //Class
} //Namespace