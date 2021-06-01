// Helper class for managing the tree view and keeping track of all changes
function TreeViewHelper(elementId) {
    var tree = $.jstree.reference(elementId);
    var treeChanges = [];

    var hasChanges = function () {
        return treeChanges.length > 0;
    };

    var getChanges = function () {
        return treeChanges;
    };

    var reset = function () {
        tree.deselect_all();
        tree.refresh();
        treeChanges = [];
    };

    var deselectAll = function () {
        tree.deselect_all();
    };

    var moveNode = function (data) {
        if (data.old_parent !== data.parent) {
            var node = data.node;
            var parent = tree.get_node(data.parent);
            var oldPath = node.data.path;
            var path = !parent.parent ? node.data.name : parent.data.path + "\\" + node.data.name;
            var type = node.data.jstree.type;
            treeChanges.push({
                "oldPath": oldPath,
                "path": path,
                "operationType": "move",
                "objectType": type
            });

            node.data.path = path;
            if (type === "folder") {
                updateFoldersPath(node);
            }
        }
    };

    var deleteNode = function () {
        var selected = tree.get_selected();
        for (var i = 0; i < selected.length; i++) {
            var node = tree.get_node(selected[i]);
            treeChanges.push({
                "path": node.data.path,
                "operationType": "delete",
                "objectType": node.data.jstree.type
            });

            tree.delete_node(node);
        }
    };

    var updateFoldersPath = function (node) {
        for (var i = 0; i < node.children.length; i++) {
            var child = tree.get_node(node.children[i]);
            child.data.path = node.data.path + "\\" + child.data.name;
            updateFoldersPath(child);
        }
    };

    var addNode = function (name) {
        var root = tree.get_node(tree.get_selected()[0]);
        var path;
        if (!root) {
            root = tree.get_node("#");
            path = name;
        } else {
            path = root.data.path + "\\" + name;
        }

        if (isNameUnique(name, root)) {
            var node = {
                li_attr: {
                    "data-name": name,
                    "data-path": path
                },
                "text": name,
                "data": {
                    "name": name,
                    "path": path
                }
            };

            tree.create_node(root.id, node, "last", function (created) {
                tree.deselect_all();
                tree.select_node(created);
            });

            treeChanges.push({
                "path": path,
                "operationType": "add",
                "objectType": "folder"
            });

            return true;
        } else {
            window.alert("There is already a folder with the same name.");

            return false;
        }
    };

    var isNameUnique = function (name, root) {
        for (var i = 0; i < root.children.length; i++) {
            var child = tree.get_node(root.children[i]);
            if (child.data.name.toLocaleLowerCase() == name.toLocaleLowerCase()) {
                return false;
            }
        }

        return true;
    };

    var hasSelection = function () {
        return tree.get_selected().length > 0;
    };

    var getSelectionPath = function () {
        var selected = tree.get_node(tree.get_selected()[0]);
        return selected ? selected.data.path : "";
    };

    return Object.freeze({
        hasChanges,
        getChanges,
        reset,
        deselectAll,
        moveNode,
        deleteNode,
        addNode,
        hasSelection,
        getSelectionPath
    });
};