using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given two binary strings, return their sum(also a binary string). 

https://leetcode.com/problems/add-binary/description/


*/

namespace AddBinary {
    class Program {
        static void Main(string[] args) {
            string inputString = "11";
            string inputString2 = "1";
            string output = AddBinary(inputString, inputString2);
            Console.WriteLine(output);
            Console.ReadKey();
        }
        static public string AddBinary(string a, string b) {
            bool carryOver = false;
            int currentANumber;
            int currentBNumber;
            int carryOverNumber = 0;
            var outputString = new StringBuilder();
            for(int aNum = a.Length-1, bNum = b.Length-1; aNum >= 0 || bNum >=0; aNum--, bNum--) {
                if (aNum >= 0) {
                    currentANumber =(a[aNum]- '0');
                } else {
                    currentANumber = 0;
                }
                if (bNum >= 0) {
                    currentBNumber = (b[bNum] - '0');
                } else {
                    currentBNumber = 0;
                }
                int total = currentANumber + currentBNumber+ carryOverNumber;
                if(total > 1) {
                    outputString.Insert(0, total % 2);
                    carryOverNumber = 1;
                    carryOver = true;
                } else {
                    outputString.Insert(0, total);
                    carryOverNumber = 0;
                    carryOver = false;
                }
            }
            if (carryOver) {
                outputString.Insert(0, '1');
            }
            return outputString.ToString();
        }
    }
}
