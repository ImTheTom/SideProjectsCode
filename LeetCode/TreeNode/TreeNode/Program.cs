using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Given an integer array with no duplicates.A maximum tree building on this array is defined as follow:


  The root is the maximum number in the array.

  The left subtree is the maximum tree constructed from left part subarray divided by the maximum number.
  The right subtree is the maximum tree constructed from right part subarray divided by the maximum number.

Construct the maximum tree by the given array and output the root node of this tree.

https://leetcode.com/problems/maximum-binary-tree/description/


*/

namespace TreeNode {
    class Program {

       public class TreeNode {
          public int val;
          public TreeNode left;
          public TreeNode right;
          public TreeNode(int x) { val = x; }
       }

        static void Main(string[] args) {
            int[] testInput = new int[] { 3, 2, 1, 6, 0, 5 };
            ConstructMaximumBinaryTree(testInput);
            Console.WriteLine();
            Console.ReadKey();
        }

        static public TreeNode ConstructMaximumBinaryTree(int[] nums) {
            return FindIndexOfStems(nums, 0, nums.Length);
        }

        public static TreeNode FindIndexOfStems(int[] nums, int start, int end) {
            if (start == end) {
                return null;
            }
            int maxInt = FindMaxIndex(nums, start, end);
            TreeNode root = new TreeNode(nums[maxInt]);
            root.left = FindIndexOfStems(nums, start, maxInt);
            root.right = FindIndexOfStems(nums, maxInt + 1, end);
            return root;
        }

        static public int FindMaxIndex(int[] nums, int start, int end) {
            int maxInt = start;
            for (int i = start; i<end; i++) {
                if(nums[i] > nums[maxInt]) {
                    maxInt = i;
                }
            }
            return maxInt;
        }
    }
}
