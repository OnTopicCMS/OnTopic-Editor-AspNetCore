$(function() {

  'use strict';

  // Get header container height
  var $windowHeight     = $(window).height();
  var $topBarHeight     = $('#HeaderContainer').height();

  // Set body top padding (compensation for fixed top bar) based on initial screen size or adjustment
  $('body').css('padding-top', $topBarHeight + 'px');
  $(window).resize(function() {
    setTimeout(function() {
      $topBarHeight     = $('#HeaderContainer').height();
      $('body').css('padding-top', $topBarHeight + 'px');
    }, 50);
  });

  // Set editor tabs bar width (tabs + action buttons)
  var $dynamicFormWidth = $('#PageContentArea').width();
  var $editorNavBar     = $('#Toolbar');
  if ($editorNavBar) {
    $editorNavBar.css('width', $dynamicFormWidth + 'px').css('top', ($topBarHeight + 1) + 'px');
  }

  // Set body top padding (compensation for fixed top bar) based on initial screen size or adjustment
  $(window).resize(function() {
    setTimeout(function() {
      $dynamicFormWidth = $('#PageContentArea').width();
      $editorNavBar.css('width', $dynamicFormWidth + 'px').css('top', ($topBarHeight + 1) + 'px');
    }, 50);
  });

  // Editor modal height
  var $percentageHeight = ($windowHeight * 0.785);
  $('[id*="EditorModal"]').on('show.bs.modal', function(e) {
    $('div[id*="EditorModal"] iframe').attr('height', $percentageHeight + 'px');
  });

  // Set height for form area
  var $formAreaOffset   = $('#FormArea').offset();
  if ($formAreaOffset) {
    $('#FormArea').css('min-height', ($windowHeight - $formAreaOffset.top) + 'px');
  };

});

