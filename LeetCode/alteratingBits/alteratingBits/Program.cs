using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given a positive integer, check whether it has alternating bits: namely, if two adjacent bits will always have different values.

https://leetcode.com/problems/binary-number-with-alternating-bits/description/

*/


namespace alteratingBits {
    class Program {
        static void Main(string[] args) {
            bool x = HasAlternatingBits(5);
            Console.WriteLine(x);
            Console.ReadKey();
        }

        static public bool HasAlternatingBits(int n) {
            var bits = Convert.ToString(n, 2);
            for(int i =0; i<bits.Length-1; i++) {
                if(bits[i] == bits[i + 1]) {
                    return false;
                }
            }
            return true;
        }
    }
}
