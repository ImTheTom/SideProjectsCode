using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 
 Initially, there is a Robot at position (0, 0). Given a sequence of its moves, judge if this robot makes a circle, which means it moves back to the original place.

The move sequence is represented by a string. And each move is represent by a character. The valid robot moves are R (Right), L (Left), U (Up) and D (down). The output should be true or false representing whether the robot makes a circle. 

    https://leetcode.com/problems/judge-route-circle/description/

    */

namespace RouteCircle {
    class Program {
        static void Main(string[] args) {
            string moves = "LL";
            bool unitTest = JudgeCircle(moves);
            if (unitTest) {
                Console.WriteLine("True");
            } else {
                Console.WriteLine("False");
            }
            Console.ReadKey();
        }

        public static bool JudgeCircle(string moves) {
            int totalRight = 0;
            int totalUp = 0;
            for(int i = 0; i < moves.Length; i++) {
                char currentLetter = moves[i];
                if(currentLetter == 'U') {
                    totalUp += 1;
                }else if(currentLetter == 'D') {
                    totalUp -= 1;
                }else if(currentLetter == 'R') {
                    totalRight += 1;
                }else if (currentLetter == 'L') {
                    totalRight -= 1;
                }
            }
            if(totalRight == 0 && totalUp == 0) {
                return true;
            }
            return false;
        }
    }
}
