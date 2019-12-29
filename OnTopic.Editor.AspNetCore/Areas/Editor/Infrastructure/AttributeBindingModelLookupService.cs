/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Reflection;
using OnTopic.Editor.Models.Components.BindingModels;
using System;

namespace OnTopic.Editor.AspNetCore.Infrastructure {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE BINDING MODEL (LOOKUP SERVICE)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a mapping between string and class names to be used when mapping <see cref="Topic"/> to a <see
  ///   cref="AttributeBindingModel"/> or derived class.
  /// </summary>
  public class AttributeBindingModelLookupService : StaticTypeLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Estabblishes a new instance of the <see cref="EditorBindingModelLookupService"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="EditorBindingModelLookupService"/>.</returns>
    public AttributeBindingModelLookupService() : base() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Add Editor-specific view models
      \-----------------------------------------------------------------------------------------------------------------------*/
      Add(typeof(BooleanAttributeBindingModel));
      Add(typeof(DateTimeAttributeBindingModel));
      Add(typeof(FileListAttributeBindingModel));
      Add(typeof(FilePathAttributeBindingModel));
      Add(typeof(HtmlAttributeBindingModel));
      Add(typeof(LastModifiedAttributeBindingModel));
      Add(typeof(LastModifiedByAttributeBindingModel));
      Add(typeof(NestedTopicListAttributeBindingModel));
      Add(typeof(NumberAttributeBindingModel));
      Add(typeof(RelationshipAttributeBindingModel));
      Add(typeof(TextAttributeBindingModel));
      Add(typeof(TextAreaAttributeBindingModel));
      Add(typeof(TokenizedTopicListAttributeBindingModel));
      Add(typeof(TopicListAttributeBindingModel));
      Add(typeof(TopicReferenceAttributeBindingModel));

    }

  } //Class
} //Namespace