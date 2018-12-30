using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Given an array of 2n integers, your task is to group these integers into n pairs of integer,
say(a1, b1), (a2, b2), ..., (an, bn) which makes sum of min(ai, bi) for all i from 1 to n as large as possible.

https://leetcode.com/problems/array-partition-i/description/

*/

namespace ArrayPartition1 {
    class Program {
        static void Main(string[] args) {
            int[] testArray = new int[] { 1, 4, 3, 2 };
            int total = other(testArray);
            Console.WriteLine(total);
            Console.ReadKey();
        }
        static public int ArrayPairSum(int[] nums) {
            for(int i =0; i < nums.Length; i++) {
                for(int j = 0; j < nums.Length-1; j++) {
                    if (nums[j] < nums[j + 1]) {
                        int temp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = temp;
                    }
                }
            }
            int total = 0;
            for(int i=1; i < nums.Length; i++) {
                if (i % 2 != 0) {
                    total += nums[i];
                }
            }
            return total;
        }

        // Other function exceeded time limit, so just used inbuilt sort algorithm.
        // I despretly need to learn sort algorithms... Maybe over this summer.
        static public int other(int[] nums) {
            Array.Sort(nums);
            int total = 0;
            for (int i = 0; i < nums.Length; i++) {
                if (i == 0) {
                    total += nums[i]; 
                }else if (i % 2 == 0) {
                    total += nums[i];
                }
            }
            return total;
        }
    }
}
