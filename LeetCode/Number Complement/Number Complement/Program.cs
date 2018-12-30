using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
Given a positive integer, output its complement number.The complement strategy is to flip the bits of its binary representation.

https://leetcode.com/problems/number-complement/description/
*/
namespace Number_Complement {
    class Program {
        static void Main(string[] args) {
            int testInput = 5;
            int testOutput = FindComplement(testInput);
            Console.WriteLine(testOutput);
            Console.ReadKey();
        }

        static public int FindComplement(int num) {
            var current = Convert.ToString(num, 2);
            string output = "";
            for (int i = 0; i < current.Length; i++) {
                if (current[i] == '1') {
                    output = output + "0";
                } else {
                    output = output + "1";
                }
            }
            return Convert.ToInt32(output, 2);
        }
    }
}
