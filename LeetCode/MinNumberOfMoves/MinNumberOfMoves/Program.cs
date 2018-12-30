using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given a non-empty integer array, find the minimum number of moves required to make all array elements equal,
where a move is incrementing a selected element by 1 or decrementing a selected element by 1.

You may assume the array's length is at most 10,000.

https://leetcode.com/problems/minimum-moves-to-equal-array-elements-ii/description/

*/

namespace MinNumberOfMoves {
    class Program {
        static void Main(string[] args) {
            int[] inputArray = new int[] { 1, 0, 0, 8, 6 };
            int outputArray = MinMoves2(inputArray);
            Console.WriteLine(outputArray);
            Console.ReadLine();
        }

        //Really need to learn some sorting alogrithms
        static public int MinMoves2(int[] nums) {
            Array.Sort(nums);
            int median = nums[nums.Length / 2];
            int moves = 0;
            for(int i=0; i < nums.Length; i++) {
                moves += Math.Abs(median - nums[i]);
            }
            return moves;
        }
    }
}
