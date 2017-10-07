/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Web.Mvc;

namespace Ignia.Topics.Editor.Mvc.Areas.Editor {

  /*============================================================================================================================
  | CLASS: EDITOR AREA REGISTRATION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Registers configuration objects associated with the editor area, including route configuration.
  /// </summary>
  public class EditorAreaRegistration : AreaRegistration {

    /*==========================================================================================================================
    | AREA NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes the friendly name for the area.
    /// </summary>
    /// <returns>The name of the area.</returns>
    public override string AreaName {
      get {
        return "Editor";
      }
    }

    /*==========================================================================================================================
    | METHOD: REGISTER AREA
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provided a <see cref="AreaRegistrationContext"/>, which allows area-specific configuration of e.g. routes.
    /// </summary>
    /// <param name="context">
    ///   The <see cref="AreaRegistrationContext"/>, typically passed from the <see cref="System.Web.HttpApplication"/> class.
    /// </param>
    public override void RegisterArea(AreaRegistrationContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle OnTopic Web namespace
      \-----------------------------------------------------------------------------------------------------------------------*/
      context.Routes.MapRoute(
        name: "TopicEditor",
        url: "Edit/{*path}",
        defaults: new { controller = "Editor", action = "Index", id = UrlParameter.Optional }
      );

    }

  } //Class

} //Namespace