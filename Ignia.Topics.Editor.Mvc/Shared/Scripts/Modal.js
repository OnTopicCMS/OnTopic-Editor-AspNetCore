(function($) {

//Set modal properties and open the modal
  initEditorModal               = function(namespace, title, targetUrl, onCloseFunction) {

    alert('initEditorModal fired:');
    console.log('namespace: ' + namespace + ' - title: ' + title + ' - targetUrl: ' + targetUrl + ' - onCloseFunction: ' + onCloseFunction);

    var $editorModal            = $('#EditorModal' + namespace),
    $modalTitle                 = $('#ModalTitle' + namespace),
    $editorFrame                = $('#EditorFrame' + namespace);
  //Set modal title
    if ($modalTitle && title.length > 0) {
      $modalTitle.html(title);
      }
  //Set modal iframe source
    if ($editorFrame && targetUrl.length > 0) {
      $editorFrame.attr('src', targetUrl);
      }
  //Open modal
    if ($editorModal) {
      $editorModal.modal({
        backdrop                : 'static',
        keyboard                : false
        });
      }

    };

//Modal close triggers (needed for closing the modal from within the iframe)
  window.closeModal     = function(action, key) {
    $('#EditorModal' + key).modal('hide');
    };
  $('#ModalCloseButton').on('click', function(e) {
    window.parent.closeModal('canceled', '');
    });

//Set initial Display Groups tab content pane as active tab
  $('.tab-content div.tab-pane:first-child').addClass('active');
  $('[id*="EditorModal"]').on('shown.bs.modal', function(e) {
    $('.tab-content div.tab-pane:first-child').addClass('active');
    });

//Modal close button handling
  $('#ModalCloseButton').on('click', function(e) {
    $('[id*="EditorModal"]').modal('hide');
    });

  })(jQuery);