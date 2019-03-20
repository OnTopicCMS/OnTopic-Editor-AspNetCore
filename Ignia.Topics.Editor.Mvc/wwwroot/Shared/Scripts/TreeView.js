var tree;
var rootTopicId = '';

Ext.onReady(function(){
  var Tree = Ext.tree;

  var currentTopic = "<%= PageTopic.FullName %>";
  var currentPosition = currentTopic.indexOf(":", 11);

//track what nodes are moved
  var oldPosition = null;
  var oldNextSibling = null;

  tree = new Tree.TreePanel({
    useArrows             : true,
    autoScroll            : true,
    animate               : true,
    enableDD              : true,
    containerScroll       : true,
    border                : false,
    baseCls               : 'TreeView',
    dataUrl               : 'Topics.Json.aspx?ShowRoot=false',
    root                  : new Ext.tree.AsyncTreeNode({
      text                : 'Web',
      draggable           : false,
      id                  : rootTopicId
      }),
    rootVisible           : false,
    listeners             : {
      click               : function(n) {
      //location.href = n.attributes.path.replace(/:/g, "/").replace(RootTopic + "/", "/Edit/");
        location.href = "?Path=" + n.attributes.path;
        },
      load                : function(n) {
        if (!n) return;
        if (currentPosition < 0) {
          currentPosition = currentTopic.length;
          }
        var currentNode = n;
        if (currentPosition <= currentTopic.length && currentPosition >= 0) {
          currentNode = currentNode.findChild("path", currentTopic.substring(0, currentPosition));
          if (currentPosition === currentTopic.length) {
            currentPosition++;
            }
          else {
            currentPosition = currentTopic.indexOf(":", currentNode.attributes.path.length + 1);
            }
          if (currentPosition < 0) {
            currentPosition = currentTopic.length;
            }
          if (currentNode.hasChildNodes() && !currentNode.isExpanded()) {
            currentNode.expand(false);
            return;
            }
          }
        tree.selectPath(currentNode.getPath());
        currentNode.ensureVisible();
        },
      startdrag           : function(tree, node, event){
        oldPosition = node.parentNode.indexOf(node);
        oldNextSibling = node.nextSibling;
        },
      movenode: function (tree, node, oldParent, newParent, position) {
        var params;
        if (oldParent === newParent){
          params = {'node':node.id, 'delta':position-oldPosition};
          }
        else {
          params = {'node':node.id, 'parent':newParent.id, 'position':position};
        //### REM JJC081410: Temporarily disabled moving between nodes until core bug can be identified and resolved.
        //Ext.Msg.alert("Disabled", "Moving between nodes is currently not supported.  This functionality is currently being redeveloped.");
        //return;
          }

      //Determine sibling ID to place node after, based off position
        var siblingId = -1;
        if (position > 0) {
          siblingId = newParent.childNodes[position-1].id; // TODO: double check indexing here
          }

      //Ext.Msg.alert("Debugging", "Node: " + node.attributes.id + ", Parent: " + newParent.attributes.id + ", Sibling: " + siblingId);

        PageMethods.MoveNode(
          node.attributes.id,
          newParent.attributes.id,
          siblingId,
          function(result) {
            if (siblingId > 0) {
            //Ext.Msg.alert('Moved', 'The ' + node.attributes.id + ' node has been moved from ' + oldParent.attributes.id + ' to ' + newParent.attributes.id + '.  SiblingId is: ' + siblingId);
              }
            else {
            //Ext.Msg.alert('Moved', 'The ' + node.attributes.id + ' node has been moved from ' + oldParent.attributes.id + ' to ' + newParent.attributes.id + '.');
              }
            }
          );

        }
      }
    });

  tree.render('TreeView');
  });

