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
  var editorModal               = document.getElementById('EditorModal_' + namespace);
  var modalTitle                = document.getElementById('ModalTitle_' + namespace);
  var editorFrame               = document.getElementById('EditorFrame_' + namespace);

  /*----------------------------------------------------------------------------------------------------------------------------
  | Provide debug data for testing
  \---------------------------------------------------------------------------------------------------------------------------*/
  console.log('initEditorModal fired:');
  console.log('namespace: '     + namespace);
  console.log('title: '         + title);
  console.log('targetUrl: '     + targetUrl);
  console.log('onCloseFunction: ' + onCloseFunction);
  console.log('#EditorModal'    + namespace + ': ' + editorModal.id);
  console.log('#ModalTitle'     + namespace  + ': ' + modalTitle.id);
  console.log('#EditorFrame'    + namespace + ': ' + editorFrame.id);

  /*----------------------------------------------------------------------------------------------------------------------------
  | Set modal window title
  \---------------------------------------------------------------------------------------------------------------------------*/
  if (modalTitle && title.length > 0) {
    modalTitle.innerText = title;
  }

  /*----------------------------------------------------------------------------------------------------------------------------
  | Set modal iframe source
  \---------------------------------------------------------------------------------------------------------------------------*/
  if (editorFrame && targetUrl.length > 0) {
    editorFrame.src = targetUrl;
  }

  /*----------------------------------------------------------------------------------------------------------------------------
  | Open modal window
  \---------------------------------------------------------------------------------------------------------------------------*/
  if (editorModal) {
    var percentageHeight = window.innerHeight * 0.785;
    editorFrame.height = percentageHeight + 'px';
    var modal = bootstrap.Modal.getInstance(editorModal);
    modal.show();
  }

};

/*==============================================================================================================================
| METHOD: CLOSE MODAL (WINDOW)
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Closes the current modal window
 */
window.closeModal = function () {
  var modalTriggerList = [].slice.call(document.querySelectorAll('[id^="EditorModal"]'));
  modalTriggerList.map(function (tooltipTriggerEl) {
    bootstrap.Modal.getInstance(tooltipTriggerEl).hide();
  });
};

/*==============================================================================================================================
| JQUERY: WIRE UP ACTIONS
\-----------------------------------------------------------------------------------------------------------------------------*/
(function (bootstrap, document) {

  /*----------------------------------------------------------------------------------------------------------------------------
  | Event Handler: Close Button
  \---------------------------------------------------------------------------------------------------------------------------*/
  var modalCloseElement = document.getElementById('ModalCloseButton');
  modalCloseElement.addEventListener('click', function(e) {
    window.parent.closeModal();
  });

})(bootstrap, document);