/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ignia.Topics.Editor.Models.Attributes;

namespace Ignia.Topics.Editor.Models.Attributes {

  /*============================================================================================================================
  | CLASS: EDITOR BINDING MODEL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides instructions to the MVC framework on how to bind postback data to a <see cref="EditorAttribute"/> instance.
  ///   This is necessary to retain strongly typed instances in <see cref="EditorBindingModel.Attributes"/>, which otherwise
  ///   exposes a collection of <see cref="EditorAttribute"/> instances.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class EditorAttributeModelBinder : DefaultModelBinder {

    /*==========================================================================================================================
    | OVERRIDE: CREATE MODEL
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Binds the incoming post to the <see cref="EditorBindingModel"/>.
    /// </summary>
    /// <remarks>
    ///   The <see cref="CreateModel(ControllerContext, ModelBindingContext, Type)"/> method is called by the MVC framework, via
    ///   convention, when it attempts to bind a model with a corresponding name.
    /// </remarks>
    protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType) {
      var typeValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".ModelType");
      var type = Type.GetType((string)typeValue.ConvertTo(typeof(string)), true);
      var model = Activator.CreateInstance(type);
      bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, type);
      return model;
    }
  }
}
