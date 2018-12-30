using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given an integer array with even length, where different numbers in this array represent different kinds of candies. Each number means one candy of the corresponding kind.
You need to distribute these candies equally in number to brother and sister.Return the maximum number of kinds of candies the sister could gain. 

Example 1:

Input: candies = [1, 1, 2, 2, 3, 3]
Output: 3
Explanation:
There are three different kinds of candies (1, 2 and 3), and two candies for each kind.
Optimal distribution: The sister has candies [1,2,3]
and the brother has candies [1,2,3], too. 
The sister has three different kinds of candies. 


https://leetcode.com/problems/distribute-candies/description/

*/


namespace Distibute_Candies {
    class Program {
        static void Main(string[] args) {
            int[] testCase = new int[] {1, 1, 1, 1, 2, 2, 2, 3, 3, 3};
            int testAnswer = DistributeCandies(testCase);
            Console.WriteLine(testAnswer);
            Console.ReadKey();
        }

        static public int DistributeCandies(int[] candies) {
            Array.Sort(candies);
            int count = 1;
            for(int i=1; i<candies.Length&& count < candies.Length / 2; i++) {
                if (candies[i] > candies[i - 1]) {
                    count += 1;
                }
            }
            return count;
        }
    }
}
