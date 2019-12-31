Ext.form.XTextField = function(config) {
  Ext.form.XTextField.superclass.constructor.call(this,config);
  }
  
Ext.extend(Ext.form.XTextField, Ext.form.TextField, {
  // private
  isEmpty: false,
  applyEmptyText : function() {
    this.isEmpty = true;
    Ext.form.XTextField.superclass.applyEmptyText.call(this);
    },
  reset: function() {
    this.isEmpty = false;
    Ext.form.XTextField.superclass.reset.call(this);
    },
  applyTo : function(target) {
    Ext.form.TextField.superclass.applyTo.call(this, target);
    var f = this.el.dom.form;
    if (f) {
      Ext.EventManager.on(f, 'submit', function() { if (this.isEmpty) { this.el.dom.value = ''; } }, this);
      }
    }
  });