using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

You are given two arrays(without duplicates) nums1 and nums2 where nums1’s elements are subset of nums2.
Find all the next greater numbers for nums1's elements in the corresponding places of nums2.

The Next Greater Number of a number x in nums1 is the first greater number to its right in nums2.If it does not exist, output -1 for this number.

https://leetcode.com/problems/next-greater-element-i/description/

*/

namespace NextGreaterNumber {
    class Program {
        static void Main(string[] args) {
            int[] input1 = new int[] { 4, 1, 2 };
            int[] input2 = new int[] { 1, 3, 4, 2 };
            int[] output = NextGreaterElement(input1, input2);
            for(int i=0; i < output.Length; i++) {
                Console.WriteLine(output[i]);
            }
            Console.ReadKey();
        }
        static public int[] NextGreaterElement(int[] findNums, int[] nums) {
            int[] output = new int[findNums.Length];
            for(int i=0; i < findNums.Length; i++) {
                int current = -1;
                int index = Array.IndexOf(nums, findNums[i]);
                for(int j= index; j < nums.Length; j++) {
                    if(findNums[i] < nums[j]) {
                        current = nums[j];
                        break;
                    }
                }
                output[i] = current;
            }
            return output;
        }
    }
}
