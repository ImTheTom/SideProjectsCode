using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given an 2D board, count how many battleships are in it.
The battleships are represented with 'X's, empty slots are represented with '.'s.

https://leetcode.com/problems/battleships-in-a-board/description/

*/

namespace Battleships {
    class Program {
        static void Main(string[] args) {
            //char[,] input = new char[,] { { 'X', '.', '.', 'X' },
            //                              {'.','.','.','X' },
            //                              {'.','.','.','X' }};
            char[,] input = new char[,] { { 'X', 'X' }, { '.', '.' } };
            int output = CountBattleships(input);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public int CountBattleships(char[,] board) {
            int total = 0;
            for(int i = 0; i < board.GetLength(0); i++) {
                for(int j=0; j < board.GetLength(1); j++) {
                    if (board[i, j] == '.'){
                        continue;
                    }else if ((i!= board.GetLength(0)-1)&&(j != board.GetLength(1) - 1)) {
                        if (board[i + 1, j] == 'X'|| board[i, j + 1] == 'X') {
                            continue;
                        }
                    }else if (j != board.GetLength(1) - 1) {
                        if (board[i, j + 1] == 'X') {
                            continue;
                        }
                    } else if(i != board.GetLength(0) - 1) {
                        if (board[i + 1, j] == 'X') {
                            continue;
                        }
                    }
                    total += 1;
                }
            }
            return total;
        }
    }
}
