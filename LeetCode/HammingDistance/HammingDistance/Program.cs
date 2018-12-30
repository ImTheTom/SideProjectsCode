using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

The Hamming distance between two integers is the number of positions at which the corresponding bits are different.

Given two integers x and y, calculate the Hamming distance.

https://leetcode.com/problems/hamming-distance/description/

*/

namespace HammingDistance {
    class Program {
        static void Main(string[] args) {
            int input1 = 1;
            int input2 = 4;
            int output = HammingDistance(input1, input2);
            Console.ReadKey();
        }

        static public int HammingDistance(int x, int y) {
            int count = 0;
            int xOrValue = x ^ y;
            do {
                if (xOrValue % 2 != 0) {
                    count++;
                }
                xOrValue = xOrValue >> 1;
            } while (xOrValue != 0);
            return count;

        }
    }
}
