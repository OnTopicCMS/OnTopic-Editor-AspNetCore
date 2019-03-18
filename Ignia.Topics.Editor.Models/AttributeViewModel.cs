/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Metadata;
using Ignia.Topics.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: EDITOR VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for an individual <see cref="Ignia.Topics.Attribute"/>, its values, and dependencies.
  /// </summary>
  public class AttributeViewModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeValue"/> class, using the specified key/value pair.
    /// </summary>
    public AttributeViewModel(
      AttributeDescriptor attributeDescriptor,
      Topic topic, ITopicRepository topicRepository,
      ViewDataDictionary viewData
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      Definition                = attributeDescriptor;
      Topic                     = topic;
      TopicRepository           = topicRepository;
      ViewData                  = viewData;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set values
      \-----------------------------------------------------------------------------------------------------------------------*/
      Key                       = attributeDescriptor.Key;
      Value                     = topic.Attributes.GetValue(attributeDescriptor.Key, null, false, false);
      InheritedValue            = topic.Attributes.GetValue(attributeDescriptor.Key);

    }

    /*==========================================================================================================================
    | ATTRIBUTE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the global definition for the attribute, as defined on the corresponding <see cref="ContentType"/>.
    /// </summary>
    public AttributeDescriptor Definition {
      get;
    }

    /*==========================================================================================================================
    | KEY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the current key, as defined on the <see cref="AttributeValue"/> instance.
    /// </summary>
    public string Key {
      get;
    }

    /*==========================================================================================================================
    | VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the current value, as defined on the <see cref="AttributeValue"/> instance.
    /// </summary>
    public string Value {
      get;
    }

    /*==========================================================================================================================
    | TOPIC
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the current <see cref="Topic"/>.
    /// </summary>
    public Topic Topic {
      get;
    }

    /*==========================================================================================================================
    | INHERITED VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the inherited value, as defined on either parent or derived topics.
    /// </summary>
    /// <remarks>
    ///   If the <see cref="Value"/> is set, then the <see cref="InhertedValue"/> will always be equal to the
    ///   <see cref="Value"/>.
    /// </remarks>
    public string InheritedValue {
      get;
    }

    /*==========================================================================================================================
    | TOPIC REPOSITORY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="ITopicRepository"/> for dynamic access to the entire topic graph.
    /// </summary>
    public ITopicRepository TopicRepository {
      get;
    }

    /*==========================================================================================================================
    | VIEW DATA
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a reference to the <see cref="ViewDataDictionary"/> for the request.
    /// </summary>
    private ViewDataDictionary ViewData {
      get;
    }

    /*==========================================================================================================================
    | PROPERTY: GET CONFIGURATION VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Retrieves a configuration value from the <see cref="Configuration"/> dictionary; if the value doesn't exist, then
    ///   optionally returns a default value.
    /// </summary>
    public string GetConfigurationValue(string key, string defaultValue = null) {
      if (ViewData != null && ViewData.ContainsKey(key) && ViewData[key] != null) {
        return ViewData[key].ToString();
      }
      return Definition.GetConfigurationValue(key, defaultValue);
    }

  } // Class

} // Namespace
