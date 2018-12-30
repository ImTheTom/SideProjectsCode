using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

You are given a map in form of a two-dimensional integer grid where 1 represents land and 0 represents water.
Grid cells are connected horizontally/vertically (not diagonally). The grid is completely surrounded by water,
and there is exactly one island (i.e., one or more connected land cells). The island doesn't have "lakes" 
(water inside that isn't connected to the water around the island). One cell is a square with side length 1.
The grid is rectangular, width and height don't exceed 100. Determine the perimeter of the island.

https://leetcode.com/problems/island-perimeter/description/

*/

namespace IslandPerimeter {
    class Program {
        static void Main(string[] args) {
            int[,] input = new int[,] { { 1 },{ 0 } };
            int output = IslandPerimeter(input);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public int IslandPerimeter(int[,] grid) {
            int total = 0;
            for(int i=0; i < grid.GetLength(0); i++) {
                for(int j = 0; j < grid.GetLength(1); j++) {
                    if(grid[i,j] == 1) {
                        if (i == 0) {
                            total += 1;
                        } else {
                            if(grid[i-1, j] == 0) {
                                total += 1;
                            }
                        }
                        if (j == 0) {
                            total += 1;
                        } else {
                            if (grid[i, j - 1] == 0) {
                                total += 1;
                            }
                        }
                        if (j == grid.GetLength(1)-1) {
                            total += 1;
                        } else {
                            if (grid[i, j + 1] == 0) {
                                total += 1;
                            }
                        }
                        if (i == grid.GetLength(0)-1) {
                            total += 1;
                        } else {
                            if (grid[i+1, j] == 0) {
                                total += 1;
                            }
                        }
                    }
                }
            }
            return total;
        }
    }
}
