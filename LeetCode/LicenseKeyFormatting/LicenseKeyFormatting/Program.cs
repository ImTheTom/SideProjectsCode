using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Now you are given a string S, which represents a software license key which we would like to format.
The string S is composed of alphanumerical characters and dashes.The dashes split the alphanumerical characters within the string into groups.
(i.e. if there are M dashes, the string is split into M+1 groups). The dashes in the given string are possibly misplaced.

We want each group of characters to be of length K (except for possibly the first group, which could be shorter,
but still must contain at least one character). To satisfy this requirement, we will reinsert dashes. Additionally,
all the lower case letters in the string must be converted to upper case.


So, you are given a non-empty string S, representing a license key to format,
and an integer K. And you need to return the license key formatted according to the description above.

https://leetcode.com/problems/license-key-formatting/description/

*/

namespace LicenseKeyFormatting {
    class Program {
        static void Main(string[] args) {
            string input1 = "2-4A0r7-4k";
            int k = 4;
            string output = LicenseKeyFormatting(input1, k);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public string LicenseKeyFormatting(string S, int K) {
            S = S.Replace("-", "");
            S = S.ToUpper();
            StringBuilder outputString = new StringBuilder();
            for(int i = 0; i < S.Length; i++) {
                outputString.Append(S[i]);
            }
            int length = outputString.ToString().Length;
            for(int i=K; i <length; i = i + K) {
                outputString.Insert(length - i, '-');
            }
            return outputString.ToString();
        }
    }
}
