/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
Ext.namespace('OnTopic');

/*==============================================================================================================================
| CLASS: DRAGGABLE TREE VIEW
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * @class OnTopic.DraggableTreeView
 * @extends Ext.tree.TreePanel
 * Provides a default implementation of a tree view to expose the topic hierarchy based on data from the OnTopic Editor's JSON
 * service. Includes event handlers for moving topics within the hierarchy.
 * @constructor
 * Create new DraggableTreeView object directly.
 * @param {object} options (optional) Options to overwrite either the {@link OnTopic.Navigation} or the underlying {@link
 * Ext.tree.TreePanel}.
 */
OnTopic.DraggableTreeView = Ext.extend(Ext.tree.TreePanel, {

  /*============================================================================================================================
  | DEFINE LOCAL FIELDS
  \---------------------------------------------------------------------------------------------------------------------------*/
  currentTopic                  : null,
  currentPosition               : null,

  /*============================================================================================================================
  | METHOD: VALIDATE SOURCE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  validateSourceTopic: function (e) {
    if (!e.dropNode.attributes.isMovable && e.dropNode.parentNode.attributes.path !== e.target.parentNode.attributes.path) {
      e.cancel = true;
    }
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
      method                    : "POST",
      url                       : "/OnTopic/Move",
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
  | REFRESH
  \---------------------------------------------------------------------------------------------------------------------------*/
  refresh                       : function() {
    this.getRootNode().reload();
  },

  /*============================================================================================================================
  | CONSTRUCTOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  constructor                   : function(currentTopic, options) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Set default options based on parameters
    \-------------------------------------------------------------------------------------------------------------------------*/
    var defaultOptions          = {
      currentTopic              : currentTopic,
      currentPosition           : currentTopic.indexOf(':', 5),
      root                      : new Ext.tree.AsyncTreeNode({})
    };

    Ext.apply(defaultOptions, options, OnTopic.DraggableTreeView.defaults);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Call parent constructor
    \-------------------------------------------------------------------------------------------------------------------------*/
    OnTopic.DraggableTreeView.superclass.constructor.call(this, defaultOptions);

  },

  /*============================================================================================================================
  | INITIALIZE COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  initComponent                 : function () {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Call parent initializer
    \-------------------------------------------------------------------------------------------------------------------------*/
    OnTopic.DraggableTreeView.superclass.initComponent.call(this);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Set default listeners
    \-------------------------------------------------------------------------------------------------------------------------*/
    var me = this;

    me.on('nodedragover',       me.validateSourceTopic,         this);
    me.on('movenode',           me.moveTopic,                   this);

  }

});

/*==============================================================================================================================
| DEFAULTS
\-----------------------------------------------------------------------------------------------------------------------------*/
OnTopic.DraggableTreeView.defaults = {
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