/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ignia.Topics.Collections;
using Ignia.Topics.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ignia.Topics.Editor.Models.Json {

  /*============================================================================================================================
  | CLASS: JSON VIEW MODEL
  >-----------------------------------------------------------------------------------------------------------------------------
  | Code largely based on https://stackoverflow.com/questions/15040838/mvc-jsonresult-camelcase-serialization.
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a <see cref="Topic"/> in a format optimized for JSON serialization.
  /// </summary>
  public class JsonNetResult : ActionResult {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of the <see cref           ="JsonNetResult"/> class.
    /// </summary>
    public JsonNetResult(
      object                    responseBody                    = null,
      JsonRequestBehavior       requestBehavior                 = JsonRequestBehavior.DenyGet,
      JsonSerializerSettings    settings                        = null
    ) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Set variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      ResponseBody              = responseBody;
      Settings                  = settings;

      /*------------------------------------------------------------------------------------------------------------------------
      | Ensure camelCase by default
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (Settings == null) {
        Settings = new JsonSerializerSettings() {
          ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
      }

    }

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the serializer settings.
    /// </summary>
    public JsonSerializerSettings Settings { get; set; }

    /*==========================================================================================================================
    | PROPERTY: JSON REQUEST BEHAVIOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the <see cref="JsonRequestBehavior"/> of the response.
    /// </summary>
    public JsonRequestBehavior JsonRequestBehavior { get; set; }

    /*==========================================================================================================================
    | PROPERTY: CONTENT ENCODING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the encoding of the response.
    /// </summary>
    public Encoding ContentEncoding { get; set; }

    /*==========================================================================================================================
    | PROPERTY: CONTENT TYPE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the HTTP content type for the response.
    /// </summary>
    public string ContentType { get; set; }

    /*==========================================================================================================================
    | PROPERTY: RESPONSE BODY
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the body of the response.
    /// </summary>
    public object ResponseBody { get; set; }

   /*==========================================================================================================================
   | PROPERTY: FORMATTING
   \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets the formatting types depending on whether we are in debug mode
    /// </summary>
    private Formatting Formatting {
      get {
        return Debugger.IsAttached ? Formatting.Indented : Formatting.None;
      }
    }

    /*==========================================================================================================================
    | METHOD: EXECUTE RESULT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    /// Serialises the response and writes it out to the response object
    /// </summary>
    /// <param name="context">The execution context</param>
    public override void ExecuteResult(ControllerContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate input
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (context == null) {
        throw new ArgumentNullException("context");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Validate context
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (
        JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
        context.HttpContext.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase)
      ) {
        throw new InvalidOperationException("Cannot return JSON via GET with JsonRequestBehavior set to DenyGet.");
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Set content type
      \-----------------------------------------------------------------------------------------------------------------------*/
      HttpResponseBase response = context.HttpContext.Response;

      if (!string.IsNullOrEmpty(ContentType)) {
        response.ContentType = ContentType;
      }
      else {
        response.ContentType = "application/json";
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Set encoding
      \-----------------------------------------------------------------------------------------------------------------------*/
      // set content encoding
      if (ContentEncoding != null) {
        response.ContentEncoding = ContentEncoding;
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return response
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (ResponseBody != null) {
        response.Write(JsonConvert.SerializeObject(ResponseBody, Formatting, Settings));
      }
    }

  } //Class

} //Namespace
