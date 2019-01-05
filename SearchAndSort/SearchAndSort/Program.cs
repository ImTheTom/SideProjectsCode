using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    class Program {
        static void Main(string[] args) {
            doFullTests();
            Console.ReadLine();
        }

        public static void doFullTests() {
            SequentialSearch.ShowCaseSearch(CreateArray(10, 10, false));
            BinarySearch.ShowCaseSearch(CreateArray(10, 10, true));

            InsertionSort.ShowCaseSort(CreateArray(10, 10, false));
            SelectionSort.ShowCaseSort(CreateArray(10, 10, false));
            BubbleSort.ShowCaseSort(CreateArray(10, 10, false));
            MergeSort.ShowCaseSort(CreateArray(10, 10, false));
            QuickSort.ShowCaseSort(CreateArray(10, 10, false));
            HeapSort.ShowCaseSort(CreateArray(10, 10, false));

            BinaryNode[] tree = CreateBinaryTree();
            BinaryTreeTraversal.DoInorder(tree[0]);//DBEACGHF
            BinaryTreeTraversal.DoPostOrder(tree[0]);//DEBHGFCA
            BinaryTreeTraversal.DoPreOrder(tree[0]);//ABDECFGH

            List<BinaryNode> tree2 = CreateBinarySearchTree();
            BinaryNode x = BinarySearchTree.Search(tree2[0], 80);
            Console.WriteLine(x.intValue);
            BinarySearchTree.Insert(tree2, new BinaryNode(45));
            Console.WriteLine(tree2[3].rightChild.intValue);

            List<Node> tree3 = CreateTree();
            TreeTraversal.DoBFT(tree3[0]);
            TreeTraversal.DoDFT(tree3[0]);

            int[] array = new int[7];
            int[] keys = new int[] { 374, 1091, 911, 227, 421, 161, 83 };
            HastTableDivide h = new HastTableDivide(array, 7);
            for (int i = 0; i < array.Length; i++) {
                h.Hash(keys[i]);
            }
            for (int i = 0; i < array.Length; i++) {
                Console.WriteLine(i + " " + array[i]);
            }
            Console.WriteLine(h.LookUp(83));
            h.Delete(83);
            Console.WriteLine(h.LookUp(83));

            int[] array2 = new int[999];
            HashTableMiddleSquare h2 = new HashTableMiddleSquare(array2, 3);
            for (int i = 0; i < keys.Length; i++) {
                h2.Hash(keys[i]);
            }
            for (int i = 0; i < array2.Length; i++) {
                if(array2[i]!=0)
                    Console.WriteLine(i + " " + array2[i]);
            }
            Console.WriteLine(h2.LookUp(83));
            h2.Delete(83);
            Console.WriteLine(h2.LookUp(83));

            string pattern = "barbaric";
            string text = "the_artic_sarcastic_barbaric_bar";
            Console.WriteLine(StringMatch.BruteForceStringMatch(text, pattern));

            Console.WriteLine(StringMatch.HorspoolStringMatch(text, pattern));
        }

        public static int[] CreateArray(int arraySize, int num, bool sorted) {
            if (num <= 0) {
                throw new Exception("num was equal or less than 0");
            }
            int randomSeed = (int)DateTime.Now.Ticks;
            Random random = new Random(randomSeed);
            int[] testArray = new int[arraySize];
            for (int i = 0; i < testArray.Length; i++)
                testArray[i] = random.Next(-num, num);
            if (sorted)
                Array.Sort(testArray);
            return testArray;
        }

        //Creates a binary Tree. A with 2 children B and C.
        //B has two children D and E. C has 1 F which is right
        //D and E has no children. F has 1 left child G
        //G has one child right H and H has no child.
        public static BinaryNode[] CreateBinaryTree() {
            BinaryNode[] tree = new BinaryNode[8];
            char[] values = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            for(int i=0; i < tree.Length; i++) {
                tree[i] = new BinaryNode(values[i]);
            }
            tree[0].leftChild = tree[1];
            tree[0].rightChild = tree[2];
            tree[1].leftChild = tree[3];
            tree[1].rightChild = tree[4];
            tree[2].rightChild = tree[5];
            tree[5].leftChild = tree[6];
            tree[6].rightChild = tree[7];
            return tree;
        }

        //Creates a binary search tree with 60 as root and 50 and 70
        //as children. 30 and 55 for the 50 children with 65 and 80 for 70
        //80 then has 1 child as 75 the rest do not
        public static List<BinaryNode> CreateBinarySearchTree() {
            BinaryNode[] tree = new BinaryNode[8];
            int[] values = new int[] { 60, 50, 70, 30, 55, 65, 80, 75 };
            for (int i = 0; i < tree.Length; i++) {
                tree[i] = new BinaryNode(values[i]);
            }
            tree[0].leftChild = tree[1];
            tree[0].rightChild = tree[2];
            tree[1].leftChild = tree[3];
            tree[1].rightChild = tree[4];
            tree[2].leftChild = tree[5];
            tree[2].rightChild = tree[6];
            tree[6].leftChild = tree[7];
            return tree.ToList<BinaryNode>();
        }

        //Creates a tree with A as the root and 3 chrildren BCE
        //B has three children FGH and E has two with IJ
        //C has no children
        public static List<Node> CreateTree() {
            Node[] tree = new Node[9];
            char[] values = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H','I'};
            for (int i = 0; i < tree.Length; i++) {
                tree[i] = new Node(values[i]);
            }
            tree[0].firstChild = tree[1];
            tree[1].firstChild = tree[4];
            tree[1].firstSibling = tree[2];
            tree[2].firstSibling = tree[3];
            tree[3].firstChild = tree[7];
            tree[4].firstSibling = tree[5];
            tree[5].firstSibling = tree[6];
            tree[7].firstSibling = tree[8];
            return tree.ToList<Node>();
        }        
    }
}
