/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using Ignia.Topics.Collections;
using Ignia.Topics.Repositories;

namespace Ignia.Topics.Editor.Models {

  /*============================================================================================================================
  | CLASS: EDITOR VIEW MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a model for an individual <see cref="Ignia.Topics.Attribute"/>, its values, and dependencies.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class AttributeViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref="AttributeValue"/> class, using the specified key/value pair.
    /// </summary>
    public AttributeViewModel(Attribute attribute, Topic topic, ITopicRepository topicRepository) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set properties
      \-----------------------------------------------------------------------------------------------------------------------*/
      Definition                = attribute;
      Topic                     = topic;
      TopicRepository           = topicRepository;

      /*------------------------------------------------------------------------------------------------------------------------
      | Set values
      \-----------------------------------------------------------------------------------------------------------------------*/
      Key                       = attribute.Key;
      Value                     = topic.Attributes.GetValue(attribute.Key, null, false, false);
      InheritedValue            = topic.Attributes.GetValue(attribute.Key);

    }

    /*==========================================================================================================================
    | ATTRIBUTE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides the global definition for the attribute, as defined on the corresponding <see cref="ContentType"/>.
    /// </summary>
    public Attribute Definition {
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

  } //Class

} //Namespace
