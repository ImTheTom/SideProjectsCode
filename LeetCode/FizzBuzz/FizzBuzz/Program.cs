using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Write a program that outputs the string representation of numbers from 1 to n.

But for multiples of three it should output “Fizz” instead of the number and for the multiples of five output “Buzz”. 
For numbers which are multiples of both three and five output “FizzBuzz”.

https://leetcode.com/problems/fizz-buzz/description/

*/

namespace FizzBuzz {
    class Program {
        static void Main(string[] args) {
            int testInput = 100;
            IList<string> output = FizzBuzz(testInput);
            for (int i = 0; i < testInput; i++) {
                Console.WriteLine(output[i]);
            }
            Console.ReadKey();
        }

        static public IList<string> FizzBuzz(int n) {
            List<string> outputString = new List<string>();
            for (int i = 1; i <= n; i++) {
                if (i % 3 == 0 && i % 5 == 0) {
                    outputString.Add("FizzBuzz");
                } else if (i % 3 == 0) {
                    outputString.Add("Fizz");
                } else if (i % 5 == 0) {
                    outputString.Add("Buzz");
                } else {
                    outputString.Add(i.ToString());
                }
            }
            return outputString;
        }
    }
}
