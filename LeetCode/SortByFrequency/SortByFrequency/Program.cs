using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given a string, sort it in decreasing order based on the frequency of characters.

https://leetcode.com/problems/sort-characters-by-frequency/description/

*/

namespace SortByFrequency {
    class Program {

        static char[] characters = new char[69] {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P',
                                             'Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d','e','f','g',
                                             'h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w', 'x',
                                             'y','z','0','1','2','3','4','5','6','7','8','9','+','/', ' ', '\'', ',', '"', '.'};

        static void Main(string[] args) {
            string input1 = "Mymommaalwayssaid,\"Lifewaslikeaboxofchocolates.Youneverknowwhatyou'regonnaget.";
            string output = FrequencySort(input1);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public string FrequencySort(string s) {
            int[] frequency = new int[69];
            for(int i = 0; i < frequency.Length; i++) {
                frequency[i] = 0;
            }
            for(int i = 0; i < s.Length; i++) {
                char current = s[i];
                int index = Array.IndexOf(characters, current);
                frequency[index] += 1;
            }
            StringBuilder outputString = new StringBuilder(s.Length);
            for(int i =0; i < s.Length; i++) {
                int highestValue = frequency.Max();
                int index = Array.IndexOf(frequency, highestValue);
                for(int j= highestValue; j > 0; j--) {
                    outputString.Append(characters[index]);
                    frequency[index] -= 1;
                    i += 1;
                }
                i -= 1;
            }
            return outputString.ToString();
        }
    }
}
