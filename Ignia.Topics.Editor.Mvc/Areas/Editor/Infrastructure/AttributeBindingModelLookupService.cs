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
  ///   The underlying <see cref="DynamicTypeLookupService"/> will search all assemblies for <see cref="Type"/>s that end with
  ///   <c>AttributeBindingModel</c>.
  /// </summary>
  public class AttributeBindingModelLookupService : DynamicTypeLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of a <see cref="AttributeBindingModelLookupService"/>.
    /// </summary>
    public AttributeBindingModelLookupService() : base(
      t => (
        t.Name.EndsWith("AttributeBindingModel", StringComparison.InvariantCultureIgnoreCase) &&
        typeof(AttributeBindingModel).IsAssignableFrom(t)
      ),
      typeof(object)
    )
    { }

  } //Class
} //Namespace