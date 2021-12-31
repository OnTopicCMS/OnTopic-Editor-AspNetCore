﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Internal.Diagnostics;
using OnTopic.Lookup;

#pragma warning disable CA1812 // Avoid uninstantiated internal classes

namespace OnTopic.Editor.AspNetCore.Infrastructure {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE BINDING MODEL (BINDER)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides instructions to the MVC framework on how to bind postback data to a <see cref="AttributeBindingModel"/> instance.
  ///   This is necessary to retain strongly typed instances in <see cref="EditorBindingModel.Attributes"/>, which otherwise
  ///   exposes a collection of <see cref="AttributeBindingModel"/> instances.
  /// </summary>
  internal class AttributeBindingModelBinder : IModelBinder {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="AttributeBindingModelBinder"/>.
    /// </summary>
    public AttributeBindingModelBinder() { }

    /*==========================================================================================================================
    | PROPERTY: TYPE LOOKUP SERVICE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes static variables for the <see cref="TopicFactory"/>.
    /// </summary>
    internal static ITypeLookupService TypeLookupService { get; set; } = new AttributeBindingModelLookupService();

    /*==========================================================================================================================
    | OVERRIDE: BIND MODEL (ASYNC)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Binds the incoming post to the <see cref="EditorBindingModel"/>.
    /// </summary>
    /// <remarks>
    ///   The <see cref="BindModelAsync(ModelBindingContext)"/> method is called by ASP.NET Core, via convention, when it
    ///   attempts to bind to a model with a corresponding name.
    /// </remarks>
    public Task BindModelAsync(ModelBindingContext bindingContext) {

      /*------------------------------------------------------------------------------------------------------------------------
      | LOOKUP FIELD
      \-----------------------------------------------------------------------------------------------------------------------*/
      var modelName             = bindingContext.ModelName;
      var key                   = bindingContext.ValueProvider.GetValue(modelName + ".AttributeDescriptor.Key").FirstValue;
      var editorType            = bindingContext.ValueProvider.GetValue(modelName + ".AttributeDescriptor.ContentType").FirstValue;
      var value                 = bindingContext.ValueProvider.GetValue(modelName + ".Value").FirstValue;

      /*------------------------------------------------------------------------------------------------------------------------
      | HANDLE OUT-OF-RANGE ERROR
      \-----------------------------------------------------------------------------------------------------------------------*/
      //The ASP.NET Core binder will keep iterating over the index until no result is returned. Assume failure to located
      //dependency fields means this point has been reached.
      if (key is null || editorType is null) {
        return Task.CompletedTask;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | HANDLE ATTRIBUTE DESCRIPTOR NAMING CONVENTION
      \-----------------------------------------------------------------------------------------------------------------------*/
      editorType                = editorType.Replace("AttributeDescriptor", "Attribute", StringComparison.Ordinal);

      /*------------------------------------------------------------------------------------------------------------------------
      | ESTABLISH MODEL
      \-----------------------------------------------------------------------------------------------------------------------*/
      var type                  = TypeLookupService.Lookup($"{editorType}BindingModel", "AttributeBindingModel");

      Contract.Assume(
        type,
        $"The type '{editorType}' could not be located by the {TypeLookupService.GetType().Name}."
      );

      var model                 = (AttributeBindingModel?)Activator.CreateInstance(type);

      Contract.Assume(
        model,
        $"An instance of the type '{type.Name}' could not be instantiated. It may be missing an empty constructor."
      );

      model                     = model with {
        Key                     = key,
        Value                   = value,
        EditorType              = editorType
      };

      bindingContext.Result     = ModelBindingResult.Success(model);

      /*------------------------------------------------------------------------------------------------------------------------
      | COMPLETE
      \-----------------------------------------------------------------------------------------------------------------------*/
      return Task.CompletedTask;

    }

  } // Class
} // Namespace

#pragma warning restore CA1812 // Avoid uninstantiated internal classes