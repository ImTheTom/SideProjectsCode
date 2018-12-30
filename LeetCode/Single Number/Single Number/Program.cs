using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given an array of integers, every element appears twice except for one.Find that single one.

https://leetcode.com/problems/single-number/description/

*/

namespace Single_Number {
    class Program {
        static void Main(string[] args) {
            int[] testInput = new int[] { -1,-1,2,3,3,4,4,5,5};
            int output = SingleNumber(testInput);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public int SingleNumber(int[] nums) {
            var dictionary = new Dictionary<int, int>();
            dictionary = nums.Distinct().ToDictionary(key => key, value => 0);
            for(int i = 0; i < nums.Length; i++) {
                dictionary[nums[i]] += 1;
            }
            return dictionary.FirstOrDefault(x => x.Value == 1).Key;
        }

    }
}
