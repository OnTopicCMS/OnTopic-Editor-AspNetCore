/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
Ext.namespace('OnTopic');

/*==============================================================================================================================
| CLASS: NAVIGATION
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * @class OnTopic.Navigation
 * @extends OnTopic.DraggableTreeView
 * Provides an implementation of the {@link OnTopic.TreeView} for use as a navigation component. Includes event handlers for
 * navigating to topics when clicked and expanding the hierarchy to the currently selected topic.
 * @constructor
 * Create new Navigation object directly.
 * @param {object} options (optional) Options to overwrite the {@link OnTopic.Navigation}, the parent {@link
 * OnTopic.DraggableTreeView}, or the root {@link Ext.tree.TreePanel}.
 */
OnTopic.Navigation = Ext.extend(OnTopic.DraggableTreeView, {

  /*============================================================================================================================
  | METHOD: NAVIGATE
  \---------------------------------------------------------------------------------------------------------------------------*/
  navigate                      : function(n) {
    location.href               = "/OnTopic/Edit" + n.attributes.webPath;
  },

  /*============================================================================================================================
  | METHOD: OPEN TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  openTopic                     : function(n) {
    if (!n) return;
    var currentTopic = this.currentTopic;
    if (this.currentPosition < 0) {
      this.currentPosition = this.currentTopic.length;
    }
    var currentNode = n;
    if (this.currentPosition <= this.currentTopic.length && this.currentPosition >= 0) {
      currentNode = currentNode.findChild('path', this.currentTopic.substring(0, this.currentPosition));
      if (this.currentPosition == this.currentTopic.length) {
        this.currentPosition++;
      }
      else {
        this.currentPosition = this.currentTopic.indexOf(':', currentNode.attributes.path.length + 1);
      }
      if (this.currentPosition < 0) {
        this.currentPosition = this.currentTopic.length;
      }
      if (currentNode.hasChildNodes() && !currentNode.isExpanded()) {
        currentNode.expand(false);
        return;
      }
    }
    currentNode.ensureVisible();
    this.selectPath(
      currentNode.getPath('text'),
      'text'
    );
    currentNode.select(currentNode);
  },

  /*============================================================================================================================
  | CONSTRUCTOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  constructor                   : function(currentTopic, options) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Call parent constructor
    \-------------------------------------------------------------------------------------------------------------------------*/
    OnTopic.Navigation.superclass.constructor.call(this, currentTopic, options);

  },

  /*============================================================================================================================
  | INITIALIZE COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  initComponent                 : function () {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Call parent initializer
    \-------------------------------------------------------------------------------------------------------------------------*/
    OnTopic.Navigation.superclass.initComponent.call(this);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Set default listeners
    \-------------------------------------------------------------------------------------------------------------------------*/
    var me = this;

    me.on('click',              me.navigate,                    this);
    me.on('load',               me.openTopic,                   this);

  }

});