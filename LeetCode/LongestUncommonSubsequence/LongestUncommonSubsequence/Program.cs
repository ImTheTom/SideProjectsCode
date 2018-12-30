using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

https://leetcode.com/problems/longest-uncommon-subsequence-i/description/

 Given a group of two strings, you need to find the longest uncommon subsequence of this group of two strings.
 The longest uncommon subsequence is defined as the longest subsequence of one of these strings and this subsequence should not be any subsequence of the other strings.

A subsequence is a sequence that can be derived from one sequence by deleting some characters without changing the order of the remaining elements.Trivially,
any string is a subsequence of itself and an empty string is a subsequence of any string.

The input will be two strings, and the output needs to be the length of the longest uncommon subsequence.
If the longest uncommon subsequence doesn't exist, return -1. 

*/

namespace LongestUncommonSubsequence {
    class Program {
        static void Main(string[] args) {
            string a = "aba";
            string b = "cdc";
            int c = FindLUSlength(a, b);
            Console.WriteLine(c);
            Console.ReadKey();
        }

        static public int FindLUSlength(string a, string b) {
            if(a == b) {
                return -1;
            }
            return Math.Max(a.Length, b.Length);
        }
    }
}
