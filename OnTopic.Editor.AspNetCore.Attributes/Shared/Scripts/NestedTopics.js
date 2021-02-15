/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
Ext.namespace('OnTopic');

/*==============================================================================================================================
| CLASS: NESTED TOPICS
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * @class OnTopic.NestedTopics
 * @extends OnTopic.DraggableTreeView
 * Provides a default implementation of a tree view to expose nested topics, while reusing baseline functionality from the
 * underlying OnTopic.DraggableTreeView class.
 * @constructor
 * Create new NestedTopics object directly.
 * @param {object} options (optional) Overwrite the {@link OnTopic.NestedTopics}, {@link OnTopic.DraggableTreeView}, or the
 * underlying {@link Ext.tree.TreePanel} settings.
 */
OnTopic.NestedTopics = Ext.extend(OnTopic.DraggableTreeView, {

  /*============================================================================================================================
  | METHOD: VALIDATE TARGET TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  validateTargetTopic           : function (dragOverEvent) {
    return dragOverEvent.point !== "append";
  },

  /*============================================================================================================================
  | CONSTRUCTOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  constructor                   : function(currentTopic, options) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Call parent constructor
    \-------------------------------------------------------------------------------------------------------------------------*/
    OnTopic.NestedTopics.superclass.constructor.call(this, currentTopic, options);

  },

  /*============================================================================================================================
  | INITIALIZE COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  initComponent                 : function () {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Call parent initializer
    \-------------------------------------------------------------------------------------------------------------------------*/
    OnTopic.NestedTopics.superclass.initComponent.call(this);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Set default listeners
    \-------------------------------------------------------------------------------------------------------------------------*/
    var me = this;

    me.on('nodedragover',       me.validateTargetTopic,         this);

  }

});