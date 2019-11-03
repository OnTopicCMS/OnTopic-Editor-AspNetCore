/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Topics.Reflection;
using Ignia.Topics.Editor.Models.Components.BindingModels;
using System;

namespace Ignia.Topics.Editor.Mvc.Infrastructure {

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
      Add(typeof(LastModifiedByAttributeBindingModel));
      Add(typeof(NestedTopicListAttributeBindingModel));
      Add(typeof(NumberAttributeBindingModel));
      Add(typeof(RelationshipAttributeBindingModel));
      Add(typeof(TextAttributeBindingModel));
      Add(typeof(TokenizedTopicListAttributeBindingModel));
      Add(typeof(TopicListAttributeBindingModel));
      Add(typeof(TopicReferenceAttributeBindingModel));

    }

  } //Class
} //Namespace