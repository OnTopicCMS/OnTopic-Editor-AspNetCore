/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Web;
using System.Web.Optimization;

namespace Ignia.Topics.Editor.Mvc {

  /*============================================================================================================================
  | CLASS: BUNDLE CONFIG
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to bundles of resources that can easily be embedded in views.
  /// </summary>
  public class BundleConfig {

    /*==========================================================================================================================
    | METHOD: REGISTER BUNDLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provided a <see cref="BundleCollection"/> configuration reference, used to register new
    ///   <see cref="Bundle"/> instances.
    /// </summary>
    /// <param name="bundles">
    ///   The bundle collection for the server, typically passed from the <see cref="System.Web.HttpApplication"/> class.
    ///   For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    /// </param>
    public static void RegisterBundles(BundleCollection bundles) {

      /*
      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
      */

    }

  } // Class

} // Namespace
