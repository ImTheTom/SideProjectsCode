using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

 You're now a baseball game point recorder.

Given a list of strings, each string can be one of the 4 following types:

    Integer(one round's score): Directly represents the number of points you get in this round.
    "+" (one round's score): Represents that the points you get in this round are the sum of the last two valid round's points.
    "D" (one round's score): Represents that the points you get in this round are the doubled data of the last valid round's points.
    "C" (an operation, which isn't a round's score): Represents the last valid round's points you get were invalid and should be removed.

Each round's operation is permanent and could have an impact on the round before and the round after.

You need to return the sum of the points you could get in all the rounds.

https://leetcode.com/problems/baseball-game/description/

*/

namespace BaseballGame {
    class Program {
        static void Main(string[] args) {
            string[] input = new string[] {"5", "2", "C", "D", "+"};
            int output = CalPoints(input);
            Console.WriteLine(output);
            Console.ReadKey();
        }

        static public int CalPoints(string[] ops) {
            int score = 0;
            List<int> history = new List<int>();
            for (int i=0; i < ops.Length; i++) {
                int currentScore = 0;
                if (Int32.TryParse(ops[i], out currentScore)) {
                    history.Add(currentScore);
                } else if (ops[i] == "C") {
                    history.Remove(history[history.Count-1]);
                } else if (ops[i] == "D") {
                    int current = history[history.Count-1] * 2;
                    history.Add(current);
                } else if (ops[i] == "+") {
                    int current = history[history.Count-1] + history[history.Count - 2];
                    history.Add(current);
                }
            }
            for(int i=0; i< history.Count(); i++) {
                score += history[i];
            }
            return score;
        }
    }
}
