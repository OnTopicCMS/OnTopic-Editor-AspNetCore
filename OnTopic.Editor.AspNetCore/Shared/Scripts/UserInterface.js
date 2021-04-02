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
  return title.replace(/[^A-Za-z0-9]+/g, "");
}

/*==============================================================================================================================
| METHOD: CONFIRM ROLLBACK
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Confirms the user's intent to rollback the current topic to the selected version
 * @param {string} versionText - The specific value of the version to be rolled back to. This represents the version date.
 * @returns {confirm} A prompt confirming the user's decision.
 */
function confirmRollback(versionText) {
  return confirm(
    'Are you sure you roll back this Topic to its ' + versionText + ' version? All data entered for this Topic will be ' +
    'reverted to their state as of this version.'
  );
}

/*==============================================================================================================================
| METHOD: CONFIRM DELETE
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Confirms the user's intent to delete the current topic and all descendents
 * @param {string} title - The title of the page, to help validate the user's intent.
 * @returns {confirm} A prompt confirming the user's decision.
 */
function confirmDelete(title) {
  return confirm(
    'Are you sure you want to delete the "' + title + '" topic? Both the topic any any descendants will be ' +
    'permanently deleted.'
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
  | Event Handler: Delete Topic
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('#DeletePageButton').on('click', function (e) {
    var title = $(this).data("title");
    if (!confirmDelete(title)) return false;
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Event Handler: Auto-populate key with default value based on title, but only for new topics
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('body.js-new #Title input:text').keydown(function (e) {
    var source = $(this);
    var target = $('#Key input:text');
    source.originalValue = source.val();
    if (target.val() === getKeyValue(source.originalValue)) {
      setTimeout(function () {
        target.val(getKeyValue(source.val()));
      }, 50);
    }
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Tooltip: Attribute Descriptions
  \---------------------------------------------------------------------------------------------------------------------------*/
  var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
  tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl, {
      placement                 : 'right',
    //delay                     : { 'show': 25, 'hide': 100 },
      viewport                  : '#DisplayGroupTabsContent'
    });
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