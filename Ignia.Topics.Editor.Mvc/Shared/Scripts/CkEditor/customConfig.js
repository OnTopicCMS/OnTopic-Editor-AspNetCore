/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config ) {
//Define changes to default configuration here. For example:
//config.language = 'fr';
//config.uiColor = '#AADC6E';
//config.extraPlugins           = 'a11ychecker';
  config.removePlugins          = 'autosave';
  config.allowedContent         = true;
  config.baseHref               = '/';
  config.skin                   = 'moono-lisa';
//config.uiColor                = '#CFCFCF';
//config.height                 = '515px';
//config.resize_maxHeight       = '800';
  config.resize_maxWidth        = '800';
  config.fillEmptyBlocks        = false;
  config.shiftEnterMode         = CKEDITOR.ENTER_BR;
  config.contentsCss            = '/_content/Ignia.Topics.Editor.Mvc/Shared/Styles/CKEditor.min.css';
  config.toolbar                = [
    { name: 'document', items: [ 'Source' ] },
    { name: 'clipboard', items: [ 'Cut', 'Copy', 'Paste',  '-', 'Undo', 'Redo' ] },
    { name: 'blocks', items: [ 'ShowBlocks', 'RemoveFormat' ] },
    { name: 'editing', items: [ 'Scayt' ] },
    { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
    { name: 'insert', items: [ 'Image', 'Table', 'HorizontalRule', 'SpecialChar' ] },
    '/',
    { name: 'basicstyles', items: [ 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat' ] },
    { name: 'position', items: [ 'JustifyLeft', 'JustifyCenter', 'JustifyRight', '-', 'Outdent', 'Indent' ] },
    { name: 'paragraph', items: [ 'NumberedList', 'BulletedList', '-', 'Blockquote' ] },
    '/',
    { name: 'styles', items: ['Styles', 'Format'] },
	  { name: 'tools', items: [ 'Find', 'Replace', 'SelectAll', 'Maximize' ] }
  ];
  config.toolbarCanCollapse     = true;
  config.toolbar_Full           = [
    ['Source','-','Save','NewPage','Preview','-','Templates'],
    ['Cut','Copy','Paste','PasteText','PasteFromWord','-','Print', 'SpellChecker', 'Scayt'],
    ['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat'],
    ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
    '/',
    ['Bold','Italic','Underline','Strike','-','Subscript','Superscript'],
    ['NumberedList','BulletedList','-','Outdent','Indent','Blockquote','CreateDiv'],
    ['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],
    ['Link','Unlink','Anchor'],
    ['Image','Flash','Table','HorizontalRule','Smiley','SpecialChar','PageBreak'],
    '/',
    ['Styles','Format','Font','FontSize'],
    ['TextColor','BGColor'],
    ['Maximize', 'ShowBlocks','-','About']
    ];
  config.toolbar_Basic          = [
    ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink','-','About']
    ];
  config.toolbar_OnTopic        = [
    ['Preview','Source','Undo','Redo','ShowBlocks','RemoveFormat'],
    ['Bold','Italic','Cut','Copy','Paste','PasteText','Scayt'],
    ['Styles'], //,'Format'
    '/',
    ['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock','Outdent','Indent'],
    ['NumberedList','BulletedList','Link','Unlink','Anchor'],
    ['CreateDiv','Image','Table','HorizontalRule','SpecialChar'],
    ['Find','Replace','SelectAll','Maximize'],
    ];
//Set font names
  config.font_names             =
    'Tahoma/Tahoma, Arial/Arial, sans-serif;'           +
    'Times New Roman/Times New Roman, Times, serif;'    +
    'Verdana';
//Call external styles set definition for Styles dropdown menu
  config.stylesCombo_stylesSet  = 'OnTopicStyleSet:StyleSets/OnTopic.stylesSet.js';
//Set classes for styles defined in styles set
  config.bodyClass              = 'CKEPanel';

};

CKEDITOR.on('instanceReady', function (ev) {
  var blockTags = ['div', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'p', 'pre', 'ul', 'ol', 'li', 'br'];
  var rules = {
    indent: false,
    breakBeforeOpen: false,
    breakAfterOpen: false,
    breakBeforeClose: false,
    breakAfterClose: true
  };

  for (var i = 0; i < blockTags.length; i++) {
    ev.editor.dataProcessor.writer.setRules(blockTags[i], rules);
  }
});