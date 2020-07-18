/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.stylesSet.add( 'OnTopicStyleSet', [
  /* Block Styles */

  // These styles are already available in the "Format" combo, so they are
  // not needed here by default. You may enable them to avoid placing the
  // "Format" combo in the toolbar, maintaining the same features.
  /*
  { name : 'Heading 5'            , element : 'h5' },
  { name : 'Heading 6'            , element : 'h6' },
  { name : 'Preformatted Text', element : 'pre' },
  { name : 'Address'              , element : 'address' },

  */

//Restored from above to remove Format combo
  { name                : 'Heading 2',
    element             : 'h2'
  //attributes          : { 'class' : 'h2 Heading' }
  },
  { name                : 'Heading 3',
    element             : 'h3'
  },
  { name                : 'Body',
    element             : 'p'
  //attributes          : { 'class' : 'Body-Text' }
  },

/* Inline Styles */

//These are core styles available as toolbar buttons. You may opt enabling
//some of them in the Styles combo, removing them from the toolbar.
/*
  { name : 'Strong'               , element : 'strong', overrides : 'b' },
  { name : 'Emphasis'             , element : 'em'        , overrides : 'i' },
  { name : 'Underline'            , element : 'u' },
  { name : 'Strikethrough'        , element : 'strike' },
  { name : 'Subscript'            , element : 'sub' },
  { name : 'Superscript'          , element : 'sup' },
  */
//Removed CKEditor defaults
/*
  { name : 'Marker: Yellow'       , element : 'span', styles : { 'background-color' : 'Yellow' } },
  { name : 'Marker: Green'        , element : 'span', styles : { 'background-color' : 'Lime' } },
  { name : 'Big'                  , element : 'big' },
  { name : 'Small'                , element : 'small' },
  { name : 'Typewriter'           , element : 'tt' },
  { name : 'Keyboard Phrase'      , element : 'kbd' },
  { name : 'Inline Quotation'     , element : 'q' },
  { name : 'Sample Text'          , element : 'samp' },
  { name : 'Deleted Text'         , element : 'del' },
  { name : 'Inserted Text'        , element : 'ins' },
  { name : 'Language: RTL'        , element : 'span', attributes : { 'dir' : 'rtl' } },
  { name : 'Language: LTR'        , element : 'span', attributes : { 'dir' : 'ltr' } },

//CKEditor defaults
  { name : 'Computer Code'        , element : 'code' },
  { name : 'Variable'             , element : 'var' },

//Altered CKEditor defaults
  { name                : 'Citation',
    element             : 'cite',
    attributes          : { 'class'  : 'Citation' }
    },
  */

/* Object Styles */

//Removed CKEditor defaults
/*
  { name : 'Image on Left', element : 'img', attributes : { 'style' : 'padding: 5px; margin-right: 5px', 'border' : '2', 'align' : 'left' } },
  { name : 'Image on Right', element : 'img', attributes : { 'style' : 'padding: 5px; margin-left: 5px', 'border' : '2', 'align' : 'right' } },
  { name : 'Square Bulleted List', element : 'ul', styles : { 'list-style-type' : 'square' } }
  */

  ]);