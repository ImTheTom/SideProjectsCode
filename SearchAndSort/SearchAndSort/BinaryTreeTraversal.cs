using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    static class BinaryTreeTraversal {

        public static void DoInorder(BinaryNode x) {
            Console.WriteLine("Doing InOrder Traversal");
            Inorder(x);
            Console.WriteLine();
        }

        public static void DoPostOrder(BinaryNode x) {
            Console.WriteLine("Doing PostOrder Traversal");
            PostOrder(x);
            Console.WriteLine();
        }

        public static void DoPreOrder(BinaryNode x) {
            Console.WriteLine("Doing PreOrder Traversal");
            PreOrder(x);
            Console.WriteLine();
        }

        private static void Inorder(BinaryNode x) {
            if (x != null) {
                Inorder(x.leftChild);
                Console.Write(x.value);
                Inorder(x.rightChild);
            }
        }

        private static void PreOrder(BinaryNode x) {
            if (x != null) {
                Console.Write(x.value);
                PreOrder(x.leftChild);
                PreOrder(x.rightChild);
            }
        }

        private static void PostOrder(BinaryNode x) {
            if (x != null) {
                PostOrder(x.leftChild);
                PostOrder(x.rightChild);
                Console.Write(x.value);
            }
        }
    }
}
