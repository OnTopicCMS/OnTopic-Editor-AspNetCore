/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

namespace OnTopic.Editor.AspNetCore.Models.ClientResources {

  /*============================================================================================================================
  | CLASS: SCRIPT RESOURCE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Represents a client-side script.
  /// </summary>
  public class ScriptResource: ClientResource {

    /*==========================================================================================================================
    | IN HEADER?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the script should be placed within the header of the page.
    /// </summary>
    /// <remarks>
    ///   By default, scripts will be placed in the footer, after the bulk of the page content, so they aren't downloaded prior
    ///   to the markup. This is generally a best practice. Optionally, however, scripts that are critical to the immediate
    ///   rendering or tracking of the page content may be placed in the header using this option. This may also be combined
    ///   with <see cref="IsDeferred"/> to control the execution timing.
    /// </remarks>
    public bool InHeader { get; set; }

    /*==========================================================================================================================
    | IS DEFERRED?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Determines if the script should be executed immediately, or deferred until the page has finished loading.
    /// </summary>
    /// <remarks>
    ///   By default, script execution will be deferred until the page has finished loading, so that the client-side DOM is
    ///   fully available, and to improve the initial page load time. Optionally, however, scripts that need to run immediately,
    ///   such This is generally a best practice.  Optionally, however, scripts that are critical to the immediate rendering or
    ///   tracking of the page content may be executed immediately. This will often be combined with <see cref="InHeader"/> to
    ///   ensure that the download of the script is prioritized.
    /// </remarks>
    public bool IsDeferred { get; set; } = true;

  }
}