/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

/*==============================================================================================================================
| CONFIGURE MODAL EDITOR
>-------------------------------------------------------------------------------------------------------------------------------
| Some attributes support editing nested or child topics. To help reinforce that these are components of the parent page, these
| are loaded in small, lightweight modal windows with minimal navigation. The following methods help configure the modal windows
| and ensure the page is prepared to handle events related to it.
\-----------------------------------------------------------------------------------------------------------------------------*/

/*==============================================================================================================================
| METHOD: INIT EDITOR MODAL
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Triggers a new modal window to load.
 * @param {string} namespace - The parent key of the nested topic container.
 * @param {string} title - The title of the given topic, if defined, to be displayed in the title bar and header of the modal.
 * @param {string} targetUrl - The URL of the editor window that should be loaded within the modal's iframe.
 * @param {string} onCloseFunction - The (optional) JavaScript function on the parent page to call when the modal is closed.
 */
initEditorModal = function (namespace, title, targetUrl, onCloseFunction) {

  /*----------------------------------------------------------------------------------------------------------------------------
  | Establish variables
  \---------------------------------------------------------------------------------------------------------------------------*/
  var $editorModal              = $('#EditorModal_' + namespace);
  var $modalTitle               = $('#ModalTitle_' + namespace);
  var $editorFrame              = $('#EditorFrame_' + namespace);

  /*----------------------------------------------------------------------------------------------------------------------------
  | Provide debug data for testing
  \---------------------------------------------------------------------------------------------------------------------------*/
  console.log('initEditorModal fired:');
  console.log('namespace: '     + namespace);
  console.log('title: '         + title);
  console.log('targetUrl: '     + targetUrl);
  console.log('onCloseFunction: ' + onCloseFunction);
  console.log('#EditorModal'    + namespace + ': ' + $editorModal);
  console.log('#ModalTitle'     + namespace  + ': ' + $modalTitle);
  console.log('#EditorFrame'    + namespace + ': ' + $editorFrame);

  /*----------------------------------------------------------------------------------------------------------------------------
  | Set modal window title
  \---------------------------------------------------------------------------------------------------------------------------*/
  if ($modalTitle && title.length > 0) {
    $modalTitle.html(title);
  }

  /*----------------------------------------------------------------------------------------------------------------------------
  | Set modal iframe source
  \---------------------------------------------------------------------------------------------------------------------------*/
  if ($editorFrame && targetUrl.length > 0) {
    $editorFrame.attr('src', targetUrl);
  }

  /*----------------------------------------------------------------------------------------------------------------------------
  | Open modal window
  \---------------------------------------------------------------------------------------------------------------------------*/
  if ($editorModal) {
    $editorModal.modal();
    $(".nav-tabs a.active").tab("show");
  }

};

/*==============================================================================================================================
| METHOD: CLOSE MODAL (WINDOW)
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Closes the current modal window
 */
window.closeModal = function () {
  $('[id^="EditorModal"]').modal('hide');
};

/*==============================================================================================================================
| JQUERY: WIRE UP ACTIONS
\-----------------------------------------------------------------------------------------------------------------------------*/
(function ($) {

  /*----------------------------------------------------------------------------------------------------------------------------
  | Event Handler: Close Button
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('#ModalCloseButton').on('click', function (e) {
    window.parent.closeModal();
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Precondition: Set first tab as active
  \---------------------------------------------------------------------------------------------------------------------------*/
  //Set the first tab (and associated content pane) as active
  $('.tab-content div.tab-pane:first-child').addClass('active');
  $('[id*="EditorModal"]').on('shown.bs.modal', function(e) {
    $('.tab-content div.tab-pane:first-child').addClass('active');
  });

})(jQuery);