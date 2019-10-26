/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

/*==============================================================================================================================
| CONFIGURE USER INTERFACE
>-------------------------------------------------------------------------------------------------------------------------------
| Wireup event handlers and provide common functions for use across the user interface, including the toolbar and actual editor
| form.
\-----------------------------------------------------------------------------------------------------------------------------*/

/*==============================================================================================================================
| METHOD: GET KEY VALUE
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Given the value of a title, automatically proposes a key name by removing characters not permitted in topic keys.
 * @param {string} title - The value of the title attribute.
 * @returns {string} A valid key to use for the topic
 */
function getKeyValue(title) {
  return input.replace(/[^A-Za-z0-9]+/g, "");
}

/*==============================================================================================================================
| METHOD: Confirm rollback
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Confirms the user's intent to rollback the current topic to the selected version
 * @param {string} version - The specific value of the version to be rolled back to. This represents the version date.
 */
// Confirm version rollback
function confirmRollback(versionText) {
  return confirm(
    'Are you sure you roll back this Topic to its ' + versionText + ' version? All data entered for this Topic will be ' +
    'reverted to their state as of this version.'
  );
}

/*==============================================================================================================================
| JQUERY: WIRE UP ACTIONS
\-----------------------------------------------------------------------------------------------------------------------------*/
(function ($) {

  'use strict';

  /*----------------------------------------------------------------------------------------------------------------------------
  | Event Handler: Version Rollback
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('#VersionsDropdown ul li a').on('click', function (e) {
    var selectedVersion = $(this).text();
    if (!confirmRollback(selectedVersion)) return false;
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Tooltip: Attribute Descriptions
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('.Content-Description').tooltip({
    placement           : 'right',
  //delay               : { 'show': 25, 'hide': 100 },
    viewport            : '#DisplayGroupTabsContent'
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Tooltip: Inherited Topic Notification
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('a.alert-link').tooltip({
    placement           : 'bottom'
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Tooltip: Manually's configured tooltips
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('[data-toggle="tooltip"]').tooltip();

})(jQuery);