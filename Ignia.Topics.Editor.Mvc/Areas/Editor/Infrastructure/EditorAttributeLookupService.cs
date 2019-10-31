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
  | CLASS: TOPIC VIEW MODEL LOOKUP SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   The <see cref="DynamicTopicViewModelLookupService"/> will search all assemblies for <see cref="Type"/>s that end with
  ///   "EditorAttribute".
  /// </summary>
  public class EditorAttributeLookupService : DynamicTypeLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of a <see cref="EditorAttributeLookupService"/>.
    /// </summary>
    public EditorAttributeLookupService() : base(
      t => (
        t.Name.EndsWith("AttributeBindingModel", StringComparison.InvariantCultureIgnoreCase) &&
        typeof(AttributeBindingModel).IsAssignableFrom(t)
      ),
      typeof(object)
    )
    { }

  } //Class
} //Namespace