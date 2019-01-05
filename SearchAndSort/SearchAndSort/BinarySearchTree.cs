using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    static class BinarySearchTree {
        public static BinaryNode Search(BinaryNode x, int key) {
            if (x == null || key == x.intValue) {
                return x;
            }
            if (key < x.intValue) {
                return Search(x.leftChild, key);
            }
            return Search(x.rightChild, key);
        }

        public static List<BinaryNode> Insert(List<BinaryNode> tree, BinaryNode newNode) {
            BinaryNode y = null;
            BinaryNode x = tree[0];
            while (x != null) {
                y = x;
                if (newNode.intValue < x.intValue) {
                    x = x.leftChild;
                } else {
                    x = x.rightChild;
                }
            }
            if (y == null) {
                tree[0] = newNode;
            } else if (newNode.intValue < y.intValue) {
                y.leftChild = newNode;
            } else {
                y.rightChild = newNode;
            }
            tree.Add(newNode);
            return tree;
        }
    }
}
