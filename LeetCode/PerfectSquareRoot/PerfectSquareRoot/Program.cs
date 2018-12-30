using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given a positive integer num, write a function which returns True if num is a perfect square else False.

https://leetcode.com/problems/valid-perfect-square/description/

*/

namespace PerfectSquareRoot {
    class Program {
        static void Main(string[] args) {
            int testInput1 = 16;
            int testInput2 = 2147483647;
            bool output1 = IsPerfectSquare(testInput1);
            bool output2 = IsPerfectSquare(testInput2);
            if (output1) {
                Console.WriteLine(testInput1+" is a perfect square");
            } else {
                Console.WriteLine(testInput1 + " isn't a perfect square");
            }
            if (output2) {
                Console.WriteLine(testInput2 + " is a perfect square");
            } else {
                Console.WriteLine(testInput2 + " isn't a perfect square");
            }
            Console.ReadKey();
        }

        static public bool IsPerfectSquare(int num) {
            if(num < 1) {
                return false;
            }
            double i = num;
            if (i > 10000) {
                while (i * i > num) {
                    i = (i + num / i) / 2;
                }
                return Math.Round(i) * Math.Round(i) == num;
            }
            int currentMax = num/2;
            bool isSquare = false;
            for(int j = 1; j<=currentMax+1; j++) {
                if(j*j == num) {
                    isSquare = true;
                    break;
                }
            }
            return isSquare;
        }
    }
}
