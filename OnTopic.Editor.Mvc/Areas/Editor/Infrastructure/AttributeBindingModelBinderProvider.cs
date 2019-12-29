/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using OnTopic.Editor.Models.Components.BindingModels;
using OnTopic.Internal.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace OnTopic.Editor.Mvc.Infrastructure {

  /*============================================================================================================================
  | CLASS: ATTRIBUTE BINDING MODEL (BINDER PROVIDER)
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Determines whether a binding request should use the <see cref="AttributeBindingModelBinder"/>—and, if so, returns a
  ///   reference to the <see cref="AttributeBindingModelBinder"/> type in response.
  /// </summary>
  internal class AttributeBindingModelBinderProvider : IModelBinderProvider {

    /*==========================================================================================================================
    | METHOD: GET BINDER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Binds the incoming post to the <see cref="EditorBindingModel"/>.
    /// </summary>
    /// <remarks>
    ///   The <see cref="CreateModel(ControllerContext, ModelBindingContext, Type)"/> method is called by the MVC framework, via
    ///   convention, when it attempts to bind a model with a corresponding name.
    /// </remarks>
    public IModelBinder GetBinder(ModelBinderProviderContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | VALIDATE INPUT
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires<ArgumentNullException>(context != null, $"A {nameof(ModelBinderProviderContext)} reference is required.");

      /*------------------------------------------------------------------------------------------------------------------------
      | RETURN BINDER (IF APPROPRIATE)
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (context.Metadata.ModelType == typeof(AttributeBindingModel)) {
        return new BinderTypeModelBinder(typeof(AttributeBindingModelBinder));
      }
      return null;

    }

  } // Class
} // Namespace
