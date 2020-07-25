/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       OnTopic Editor
\=============================================================================================================================*/
/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

/*==============================================================================================================================
| METHOD: EDITOR CONFIG
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Provides configuration settings for CKEditor.
 */
CKEDITOR.editorConfig = function( config ) {
  config.removePlugins          = 'autosave,scayt';
  config.allowedContent         = true;
  config.baseHref               = '/';
  config.skin                   = 'moono-lisa';
  config.resize_maxWidth        = '800';
  config.fillEmptyBlocks        = false;
  config.shiftEnterMode         = CKEDITOR.ENTER_BR;
  config.contentsCss            = '/_content/OnTopic.Editor.AspNetCore/Shared/Styles/CKEditor.css';
  config.disableNativeSpellChecker = false;
  config.toolbar                = [
    { name: 'document', items: [ 'Source' ] },
    { name: 'clipboard', items: ['Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste' ] },
    { name: 'blocks', items: [ 'ShowBlocks', 'RemoveFormat' ] },
    { name: 'links', items: [ 'Link', 'Unlink', 'Anchor' ] },
    { name: 'insert', items: [ 'Image', 'Table', 'HorizontalRule', 'SpecialChar' ] },
    '/',
    { name: 'styles', items: ['Styles', 'Format'] },
    { name: 'basicstyles', items: [ 'Bold', 'Italic', 'Strike', 'Subscript', 'Superscript' ] },
    { name: 'position', items: [ 'JustifyLeft', 'JustifyCenter', 'JustifyRight' ] },
    { name: 'paragraph', items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', '-', 'Blockquote' ] },
    { name: 'tools', items: [ 'Find', 'Replace', 'SelectAll', 'Maximize' ] }
  ];
  config.toolbarCanCollapse     = true;
//Set font names
  config.font_names             =
    'Tahoma/Tahoma, Arial/Arial, sans-serif;'           +
    'Times New Roman/Times New Roman, Times, serif;'    +
    'Verdana';
//Call external styles set definition for Styles dropdown menu
  config.stylesCombo_stylesSet  = 'OnTopicStyleSet:/_content/OnTopic.Editor.AspNetCore/Shared/Scripts/CkEditor/StylesSet.js';
//Set classes for styles defined in styles set
  config.bodyClass              = 'CKEPanel';

};

/*==============================================================================================================================
| METHOD: INSTANCE READY
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * Provides late binding rules to initialize once the editor instance is ready.
 */
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