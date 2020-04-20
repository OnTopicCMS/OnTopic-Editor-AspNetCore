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
 * @extends Ext.tree.TreePanel
 * Provides a default implementation of a tree view to expose the topic hierarchy based on data from the OnTopic Editor's JSON
 * service. Includes event handlers for navigating to topics when clicked, expanding the hierarchy to the currently selected
 * topic, and moving topics within the hierarchy.
 * @constructor
 * Create new Navigation object directly.
 * @param {object} options (optional) Any options to overwrite from either the {@link OnTopic.Navigation} or the underlying
 * {@link Ext.tree.TreePanel}.
 */
OnTopic.Navigation = Ext.extend(Ext.tree.TreePanel, {

  /*============================================================================================================================
  | DEFINE LOCAL FIELDS
  \---------------------------------------------------------------------------------------------------------------------------*/
  currentTopic                  : null,
  rootTopicId                   : null,
  currentPosition               : null,

  // Track what nodes are moved
  oldPosition                   : null,
  oldNextSibling                : null,

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
  | METHOD: DRAG TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  dragTopic                     : function(tree, node, event) {
    node.draggable              = (node.attributes.draggable == 'false');
    oldPosition                 = node.parentNode.indexOf(node);
    oldNextSibling              = node.nextSibling;
  },

  /*============================================================================================================================
  | METHOD: MOVE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  moveTopic                     : function(tree, node, oldParent, newParent, position) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Define variables
    \-------------------------------------------------------------------------------------------------------------------------*/
    var me                      = this;

    // Determine sibling ID to place node after, based off position
    var siblingId               = position > 0? newParent.childNodes[position - 1].id : -1;

    /*--------------------------------------------------------------------------------------------------------------------------
    | Move on server
    \-------------------------------------------------------------------------------------------------------------------------*/
    $.ajax({
      method: "POST",
      url: "/OnTopic/Move",
      data: {
        topicId                 : node.attributes.id,
        targetTopicId           : newParent.attributes.id,
        siblingId               : siblingId
      }
    })

    /*--------------------------------------------------------------------------------------------------------------------------
    | Refresh tree or page
    \-------------------------------------------------------------------------------------------------------------------------*/
    .done(function () {

      //If current or ascendent topic, redirect to new location since the current URL is no longer valid
      if (me.currentTopic.startsWith(node.attributes.path)) {
        location.href = location.href.replace(oldParent.attributes.webPath, newParent.attributes.webPath);
      }

      //Otherwise, refresh the tree to ensure the new webPaths are reflected
      else {
        me.currentPosition = me.currentTopic.indexOf(':', 5);
        tree.getRootNode().reload();
      }

    });

  },

  /*============================================================================================================================
  | CONSTRUCTOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  constructor : function(currentTopic, rootTopicId, options) {
    var me                      = this;

    //Define defaults based on variables
    var defaultOptions           = {
      rootTopicId               : rootTopicId,
      currentTopic              : currentTopic,
      currentPosition           : currentTopic.indexOf(':', 5),
      root                      : new Ext.tree.AsyncTreeNode({
        text                    : 'Web',
        draggable               : false,
        id                      : rootTopicId
      }),
      listeners: {
        click                   : me.navigate,
        load                    : me.openTopic,
        startdrag               : me.dragTopic,
        movenode                : me.moveTopic
      }
    };

    //Merge local defaults with static defaults and user-defined preferences
    Ext.apply(defaultOptions, OnTopic.Navigation.defaults, options);

    //Call parent class
    Ext.tree.TreePanel.superclass.constructor.call(this, defaultOptions);

  }

});


/*==============================================================================================================================
| DEFAULTS
\-----------------------------------------------------------------------------------------------------------------------------*/
OnTopic.Navigation.defaults     = {
  useArrows                     : true,
  autoScroll                    : true,
  animate                       : true,
  enableDD                      : true,
  containerScroll               : true,
  border                        : false,
  baseCls                       : 'treeview',
  dataUrl                       : '/OnTopic/JSON/Root/?ShowAll=true&UseKeyAsText=true',
  rootVisible                   : false
};