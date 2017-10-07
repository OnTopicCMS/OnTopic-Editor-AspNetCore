/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System.Web;
using System.Web.Mvc;

namespace Ignia.Topics.Editor.Mvc {

  /*============================================================================================================================
  | CLASS: FILTER CONFIG
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Registers filters that can address cross-cutting concerns such as error handling, authentication, and logging.
  /// </summary>
  public class FilterConfig {

    /*==========================================================================================================================
    | METHOD: REGISTER GLOBAL FILTERS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provided a <see cref="GlobalFilterCollection"/> configuration reference, used to register new
    ///   <see cref="FilterAttribute"/> instances.
    /// </summary>
    /// <param name="filters">
    ///   The filter collection for the server, typically passed from the <see cref="System.Web.HttpApplication"/> class.
    /// </param>
    public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
      filters.Add(new HandleErrorAttribute());
    }

  } //Class

} //Namespace
