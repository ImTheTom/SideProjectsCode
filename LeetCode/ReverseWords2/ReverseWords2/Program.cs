using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given a string, you need to reverse the order of characters in each word within
a sentence while still preserving whitespace and initial word order.

https://leetcode.com/problems/reverse-words-in-a-string-iii/description/

*/

namespace ReverseWords2 {
    class Program {
        static void Main(string[] args) {
            string input = "Let's take LeetCode contest";
            string output = ReverseWords(input);
            Console.WriteLine(output);
            Console.ReadKey();
        }
        static public string ReverseWords(string s) {
            string[] words = s.Split(' ');
            StringBuilder output = new StringBuilder();
            for(int i = 0; i < words.Length; i++) {
                string current = words[i];
                for(int j=current.Length-1; j >= 0; j--) {
                    output.Append(current[j]);
                }
                if (i < words.Length - 1) {
                    output.Append(' ');
                }
            }
            return output.ToString();
        }
    }
}
