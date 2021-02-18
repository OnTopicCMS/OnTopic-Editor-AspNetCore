/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnTopic.Editor.AspNetCore.Models;
using OnTopic.Lookup;

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
    internal AttributeBindingModelBinder() { }

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
    ///   The <see cref="CreateModel(ControllerContext, ModelBindingContext, Type)"/> method is called by the MVC framework, via
    ///   convention, when it attempts to bind a model with a corresponding name.
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
      var type                  = TypeLookupService.Lookup($"{editorType}BindingModel");
      var model                 = (AttributeBindingModel?)Activator.CreateInstance(type);

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