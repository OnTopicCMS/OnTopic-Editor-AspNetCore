/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/

/*==============================================================================================================================
| CONFIGURE LAYOUT DIMENSIONS
>-------------------------------------------------------------------------------------------------------------------------------
| The nature of the editor's layout makes it susceptible to changes in the window height. Ideally, this would have all been
| handled via CSS. In absence of that, however, the following functions help maintain a full-height grid while keeping each of
| the individual elements independent of one another (e.g., not resorting to a table).
\-----------------------------------------------------------------------------------------------------------------------------*/
$(function () {

  'use strict';

  /*----------------------------------------------------------------------------------------------------------------------------
  | Establish initial variables
  \---------------------------------------------------------------------------------------------------------------------------*/
  var $windowHeight             = $(window).height();
  var $topBarHeight             = $('#HeaderContainer').height();

  /*----------------------------------------------------------------------------------------------------------------------------
  | Compensate for height of the header and toolbar
  \---------------------------------------------------------------------------------------------------------------------------*/
  $('body').css('padding-top', $topBarHeight + 'px');
  $(window).resize(function() {
    setTimeout(function() {
      $topBarHeight             = $('#HeaderContainer').height();
      $('body').css('padding-top', $topBarHeight + 'px');
    }, 50);
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Set size and location of toolbar
  \---------------------------------------------------------------------------------------------------------------------------*/
  var $dynamicFormWidth         = $('#PageContentArea').width();
  var $editorNavBar             = $('#Toolbar');
  if ($editorNavBar) {
    $editorNavBar.css('width', $dynamicFormWidth + 'px').css('top', $topBarHeight+1 + 'px').show();
  }

  /*----------------------------------------------------------------------------------------------------------------------------
  | Set size and location of the body area
  \---------------------------------------------------------------------------------------------------------------------------*/
  $(window).resize(function() {
    setTimeout(function() {
      $dynamicFormWidth         = $('#PageContentArea').width();
      $editorNavBar.css('width', $dynamicFormWidth + 'px').css('top', $topBarHeight+1 + 'px');
    }, 50);
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Set height of modal window
  \---------------------------------------------------------------------------------------------------------------------------*/
  var $percentageHeight = $windowHeight * 0.785;
  $('[id*="EditorModal"]').on('show.bs.modal', function(e) {
    $('div[id*="EditorModal"] iframe').attr('height', $percentageHeight + 'px');
  });

  /*----------------------------------------------------------------------------------------------------------------------------
  | Set height of form area
  \---------------------------------------------------------------------------------------------------------------------------*/
  var $formAreaOffset   = $('#DisplayGroupTabsContent').offset();
  if ($formAreaOffset) {
    $('#DisplayGroupTabsContent').css('min-height', $windowHeight-$formAreaOffset.top + 'px');
  }

  /*----------------------------------------------------------------------------------------------------------------------------
  | Initialize tooltips
  \---------------------------------------------------------------------------------------------------------------------------*/
  var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
  tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
  });

});