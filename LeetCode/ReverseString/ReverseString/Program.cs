using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Write a function that takes a string as input and returns the string reversed.

https://leetcode.com/problems/reverse-string/description/

*/

namespace ReverseString {
    class Program {
        static void Main(string[] args) {
            string input = "Hello World.";
            string output = ReverseString(input);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public string ReverseString(string s) {
            StringBuilder output = new StringBuilder();
            for(int i = s.Length-1; i >= 0; i--) {
                output.Append(s[i]);
            }
            return output.ToString();
        }
    }
}
