/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

/*==============================================================================================================================
| CONFIGURE "ARE YOU SURE?"
>-------------------------------------------------------------------------------------------------------------------------------
| PaperCut's Are-You-Sure library is a simple jQuery plugin that detects when the user is leaving a page with unsaved
| modifications to the form. In this scenario, it prompts them to confirm this is intentional, and that they understand any
| unsaved work will be lost.
\-----------------------------------------------------------------------------------------------------------------------------*/
(function ($) {

  'use strict';

  /*----------------------------------------------------------------------------------------------------------------------------
  | Ignore navigation elements
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('.Sidebar select').addClass('ays-ignore');

  /*----------------------------------------------------------------------------------------------------------------------------
  | Event Handler: Add "Are you sure?" to form
  \---------------------------------------------------------------------------------------------------------------------------*/
  //Enable dirty form checking; warn users if navigating away from page without saving
  $('form').areYouSure({
    'message': 'You have made changes to this topic without saving. If you continue, any changes will be lost.'
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Configuration: Ensure hidden WYSIWYG fields participate in form check
  \---------------------------------------------------------------------------------------------------------------------------*/
  var isFormSubmitting = false;
  $('form').submit(function () {
    isFormSubmitting = true;
  });
  $(window).on('beforeunload', function () {
    if (!isFormSubmitting) {
      for (var i in CKEDITOR.instances) {
        if (CKEDITOR.instances[i].checkDirty()) {
          $('form').trigger('checkform.areYouSure');
          return 'You have made changes to this topic without saving.';
        }
      }
    }
  });

})(jQuery);