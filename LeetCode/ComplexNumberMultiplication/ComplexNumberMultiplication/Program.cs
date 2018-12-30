using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given two strings representing two complex numbers.

You need to return a string representing their multiplication.Note i2 = -1 according to the definition. 

https://leetcode.com/problems/complex-number-multiplication/description/

*/

namespace ComplexNumberMultiplication {
    class Program {
        static void Main(string[] args) {
            string inputA = "1+-1i";
            string inputB = "1+-1i";
            string output = ComplexNumberMultiply(inputA, inputB);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public string ComplexNumberMultiply(string a, string b) {
            int indexOfAdditionA = a.IndexOf("+");
            int indexOfAdditionB = b.IndexOf("+");
            string realA = a.Substring(0, indexOfAdditionA);
            indexOfAdditionA++;
            string imaginaryA = a.Substring(indexOfAdditionA, a.Length-1- indexOfAdditionA);
            string realB = b.Substring(0, indexOfAdditionB);
            indexOfAdditionB++;
            string imaginaryB = b.Substring(indexOfAdditionB, b.Length - 1 - indexOfAdditionB);
            int intRealA = Convert.ToInt32(realA);
            int intRealB = Convert.ToInt32(realB);
            int intImaginaryA = Convert.ToInt32(imaginaryA);
            int intImaginaryB = Convert.ToInt32(imaginaryB);
            int realComponents = intRealA * intRealB;
            int imaginaryComponents = intImaginaryA * intImaginaryB;
            int firstTwo = intRealA * intImaginaryB;
            int secondTwo = intRealB * intImaginaryA;
            int totalReal = realComponents - imaginaryComponents;
            int totalImaginary = firstTwo + secondTwo;
            string output = totalReal + "+" + totalImaginary + "i";
            return output;
        }

    }
}
