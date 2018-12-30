using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

In MATLAB, there is a very useful function called 'reshape', which can reshape a matrix into a new one with different size but keep its original data.

You're given a matrix represented by a two-dimensional array, and two positive integers r and c representing the row number and column number of the wanted reshaped matrix, respectively.

The reshaped matrix need to be filled with all the elements of the original matrix in the same row-traversing order as they were.

If the 'reshape' operation with given parameters is possible and legal, output the new reshaped matrix; Otherwise, output the original matrix.

https://leetcode.com/problems/reshape-the-matrix/description/

*/

namespace Reshape {
    class Program {
        static void Main(string[] args) {
            int[,] nums = new int[,] { { 1, 2 }, { 3, 4 } };
            int[,] output = MatrixReshape(nums, 1, 4);
            Console.WriteLine(output[0,0]);
            Console.WriteLine(output[0,1]);
            Console.WriteLine(output[0,2]);
            Console.WriteLine(output[0,3]);
            Console.ReadKey();
        }
        static public int[,] MatrixReshape(int[,] nums, int r, int c) {
            if (r * c == nums.GetLength(0) * nums.GetLength(1)) {
                int[,] output = new int[r, c];
                List<int> numbers = new List<int>();
                for (int i = 0; i < nums.GetLength(0); i++) {
                    for (int j = 0; j < nums.GetLength(1); j++) {
                        numbers.Add(nums[i, j]);
                    }
                }
                int total = 0;
                for (int i = 0; i < output.GetLength(0); i++) {
                    for (int j = 0; j < output.GetLength(1); j++) {
                        output[i, j] = numbers[total];
                        total += 1;
                    }
                }
                return output;
            } else {
                return nums;
            }
        }
    }
}
