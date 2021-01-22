/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.Models.Components.BindingModels;
using OnTopic.Lookup;
using OnTopic.Models;

namespace OnTopic.Editor.AspNetCore.Infrastructure {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE BINDING MODEL (LOOKUP SERVICE)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a mapping between string and class names to be used when mapping <see cref="Topic"/> to a <see
  ///   cref="AttributeBindingModel"/> or derived class.
  /// </summary>
  public class AttributeBindingModelLookupService : DynamicTypeLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Estabblishes a new instance of the <see cref="EditorBindingModelLookupService"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="EditorBindingModelLookupService"/>.</returns>
    public AttributeBindingModelLookupService() : base(t => typeof(AttributeBindingModel).IsAssignableFrom(t)) {

    }

  } //Class
} //Namespace