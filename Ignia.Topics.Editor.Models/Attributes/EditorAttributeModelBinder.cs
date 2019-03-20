/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ignia.Topics.Editor.Models.Attributes {

  /*============================================================================================================================
  | CLASS: EDITOR BINDING MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides instructions to the MVC framework on how to bind postback data to a <see cref="EditorAttribute"/> instance.
  ///   This is necessary to retain strongly typed instances in <see cref="EditorBindingModel.Attributes"/>, which otherwise
  ///   exposes a collection of <see cref="EditorAttribute"/> instances.
  /// </summary>
  public class EditorAttributeModelBinder : IModelBinder {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="EditorAttributeModelBinder"/>.
    /// </summary>
    public EditorAttributeModelBinder() { }

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
      var modelName = bindingContext.ModelName;
      var typeValue = bindingContext.ValueProvider.GetValue(modelName + ".AttributeDescriptor.EditorType").FirstValue.ToString();
      if (typeValue != null) {
        typeValue = typeValue.Replace(".ascx", "");
      }
      var type = Type.GetType("Ignia.Topics.Editor.Models.Attributes." + typeValue.ToString() + "EditorAttribute", true);
      var model = Activator.CreateInstance(type);
      bindingContext.Result = ModelBindingResult.Success(model);
      return Task.CompletedTask;
    }

  } // Class

} // Namespace
