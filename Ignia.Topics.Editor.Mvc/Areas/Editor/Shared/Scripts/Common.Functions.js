(function($) {
  'use strict';

  // Decorate sidebar form fields (e.g., the add topic selection) so that Are-You-Sure will ignore them
  $('.Sidebar select').addClass('ays-ignore');

  // Enable dirty form checking; warn users if navigating away from page without saving
  $('form').areYouSure({
    'message'           : 'You have made changes to this topic without saving.'
  });

  // Make sure hidden WYSIWYG textarea fields trigger the form check, but only if not submitting
  var isFormSubmitting  = false;
  $('form').submit(function() {
    isFormSubmitting    = true;
  });
  $(window).on('beforeunload', function() {
    if (!isFormSubmitting) {
      for (var i in CKEDITOR.instances) {
        if(CKEDITOR.instances[i].checkDirty()) {
          $('form').trigger('checkform.areYouSure');
          return 'You have made changes to this topic without saving.';
        }
      }
    }
  });

  // Modal close triggers (needed for closing the modal from within the iframe)
  window.closeModal     = function(action, key) {
    $('#EditorModal').modal('hide');
  };
  $('#ModalCloseButton').on('click', function(e) {
    window.parent.closeModal('canceled', '');
  });

  // Set initial Display Groups tab content pane as active tab
  $('.tab-content div.tab-pane:first-child').addClass('active');
  $('[id*="EditorModal"]').on('shown.bs.modal', function(e) {
    $('.tab-content div.tab-pane:first-child').addClass('active');
  });

  // Confirm version rollback
  $('#VersionsDropdown ul li a').on('click', function(e) {
    var selectedVersion = $(this).text();
    if (!confirmRollback(selectedVersion)) return false;
  });

  // Modal close button handling
  $('#ModalCloseButton').on('click', function(e) {
    $('[id*="EditorModal"]').modal('hide');
  });

  // Initialize tooltips for field descriptions
  $('.Content-Description').tooltip({
    placement           : 'right',
  //delay               : { 'show': 25, 'hide': 100 },
    viewport            : '#DisplayGroupTabsContent'
  });

  // Initialize tooltip for inherited topic note
  $('a.alert-link').tooltip({
    placement           : 'bottom'
  });

})(jQuery);

// Set modal properties and open the modal
function initEditorModal(title, targetUrl, onCloseFunction) {

  var
    $editorModal        = $('#EditorModal'),
    $modalTitle         = $('#ModalTitle'),
    $editorFrame        = $('#EditorFrame');

  // Set modal title
  if ($modalTitle && title.length > 0) {
    $modalTitle.html(title);
  }

  // Set modal iframe source
  if ($editorFrame && targetUrl.length > 0) {
    $editorFrame.attr('src', targetUrl);
  }

  // Open modal
  if ($editorModal) {
    $editorModal.modal({
      backdrop          : 'static',
      keyboard          : false
    });
  };

};

// Passthrough function for evaluating/setting the Topic Key value based on the Title value
function getKeyValue(input) {
  return input.replace(/[^A-Za-z0-9]+/g, "");
}

// Confirm version rollback
function confirmRollback(versionText) {
  return confirm('Are you sure you roll back this Topic to its ' + versionText + ' version? All data entered for this Topic will be reverted to their state as of this version.');
}
