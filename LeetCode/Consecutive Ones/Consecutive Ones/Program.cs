using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given a positive integer n, find the number of non-negative integers less than or equal to n, whose binary representations do NOT contain consecutive ones.

https://leetcode.com/problems/non-negative-integers-without-consecutive-ones/description/


*/


namespace Consecutive_Ones {
    class Program {
        static void Main(string[] args) {
            int total = FindIntegers(10000000);
            Console.WriteLine(total);
            Console.ReadKey();
        }

        static public int FindIntegers(int num) {
            int total = 0;
            for (int i = 0; i <= num; i++) {
                var current = Convert.ToString(i, 2);
                bool noConsecutives = true;
                for(int j =0; j < current.Length-1; j++) {
                    if(current[j]=='1' && current[j+1] =='1') {
                        noConsecutives = false;
                    }
                }
                if (noConsecutives) {
                    total += 1;
                }
            }
            return total;
        }
    }
}
