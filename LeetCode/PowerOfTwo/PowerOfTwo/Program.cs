using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given an integer, write a function to determine if it is a power of two.

https://leetcode.com/problems/power-of-two/description/

*/

namespace PowerOfTwo {
    class Program {
        static void Main(string[] args) {
            int testInput1 = 2;
            bool output = IsPowerOfTwo(testInput1);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public bool IsPowerOfTwo(int n) {
            bool isPower = false;
            if(n == 1) {
                return true;
            }
            while(n >= 2) {
                if (n == 2) {
                    isPower = true;
                    break;
                }
                if (n % 2 == 0) {
                    n = n / 2;
                } else {
                    break;
                }
            }
            return isPower;
        }
    }
}
