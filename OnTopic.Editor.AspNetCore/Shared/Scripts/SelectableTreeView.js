/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
Ext.namespace('OnTopic');

/*==============================================================================================================================
| CLASS: SELECTABLE TREE VIEW
\-----------------------------------------------------------------------------------------------------------------------------*/
/**
 * @class OnTopic.SelectableTreeView
 * @extends Ext.tree.TreePanel
 * Provides an implementation of a {@link Ext.tree.TreePanel} which exposes checkboxes and is backed by a hidden field for
 * collecting the topic identifiers of any nodes checked. For use with e.g. the relationships view component.
 * @constructor
 * Create new SelectableTreeView object directly.
 * @param {object} options (optional) Options to overwrite either the {@link OnTopic.SelectableTreeView} or the underlying
 * {@link Ext.tree.TreePanel}.
 */
OnTopic.SelectableTreeView = Ext.extend(Ext.tree.TreePanel, {

  /*============================================================================================================================
  | DEFINE LOCAL FIELDS
  \---------------------------------------------------------------------------------------------------------------------------*/
  checkAscendants               : false,
  selectedTopics                : null,
  backingField                  : null,

  /*============================================================================================================================
  | METHOD: SELECT TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  selectTopic                   : function(node, rec) {
    node.getUI().toggleCheck();
    if (node.isExpanded() != node.getUI().isChecked()) {
      node.toggle();
    }
    return false;
  },

  /*============================================================================================================================
  | METHOD: UPDATE BACKING FIELD
  \---------------------------------------------------------------------------------------------------------------------------*/
  updateBackingField: function (node, checked) {
    if (checked) {
      this.selectedTopics.push(node.attributes.id.toString());
    }
    else {
      this.selectedTopics.remove(node.attributes.id.toString());
    }
    this.backingField.dom.value = this.selectedTopics.concat(",");
    if (!this.checkAscendants) return true;
    if (checked && node.parentNode) node.parentNode.getUI().toggleCheck(true);
  },

  /*============================================================================================================================
  | CONSTRUCTOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  constructor                   : function(options) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Set default options based on parameters
    \-------------------------------------------------------------------------------------------------------------------------*/
    Ext.apply(this, options, OnTopic.SelectableTreeView.defaults);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Initialize root
    \-------------------------------------------------------------------------------------------------------------------------*/
    this.root = new Ext.tree.AsyncTreeNode({
      checked                   : true,
      text                      : 'Web',
      draggable                 : false,
      leaf                      : false
    });

    /*--------------------------------------------------------------------------------------------------------------------------
    | Initialize variables
    \-------------------------------------------------------------------------------------------------------------------------*/
    this.backingField           = Ext.get(options.backingField);
    this.selectedTopics         = this.backingField.dom.value.split(",");

    /*--------------------------------------------------------------------------------------------------------------------------
    | Call parent constructor
    \-------------------------------------------------------------------------------------------------------------------------*/
    OnTopic.SelectableTreeView.superclass.constructor.call(this);

  },

  /*============================================================================================================================
  | INITIALIZE COMPONENT
  \---------------------------------------------------------------------------------------------------------------------------*/
  initComponent                 : function () {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Call parent initializer
    \-------------------------------------------------------------------------------------------------------------------------*/
    OnTopic.SelectableTreeView.superclass.initComponent.call(this);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Set default listeners
    \-------------------------------------------------------------------------------------------------------------------------*/
    var me = this;

    me.on('beforeclick',        me.selectTopic,                 this);
    me.on('checkchange',        me.updateBackingField,          this);

  }

});

/*==============================================================================================================================
| DEFAULTS
\-----------------------------------------------------------------------------------------------------------------------------*/
OnTopic.SelectableTreeView.defaults = {
  useArrows                     : true,
  autoScroll                    : true,
  animate                       : true,
  enableDD                      : false,
  containerScroll               : true,
  border                        : false,
  baseCls                       : 'RelationshipsTreeView',
  rootVisible                   : false
};