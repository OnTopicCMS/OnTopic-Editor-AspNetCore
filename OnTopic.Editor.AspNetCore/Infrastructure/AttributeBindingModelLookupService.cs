/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Lookup;

namespace OnTopic.Editor.AspNetCore.Infrastructure {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE BINDING MODEL (LOOKUP SERVICE)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a mapping between string and class names to be used when mapping <see cref="Topic"/> to a <see
  ///   cref="AttributeBindingModel"/> or derived class.
  /// </summary>
  internal class AttributeBindingModelLookupService : DynamicTypeLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="AttributeBindingModelLookupService"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="AttributeBindingModelLookupService"/>.</returns>
    internal AttributeBindingModelLookupService() : base(t => typeof(AttributeBindingModel).IsAssignableFrom(t)) {

    }

  } //Class
} //Namespace