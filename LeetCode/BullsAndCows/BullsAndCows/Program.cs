using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

You are playing the following Bulls and Cows game with your friend:
You write down a number and ask your friend to guess what the number is.
Each time your friend makes a guess, you provide a hint that indicates how many digits
in said guess match your secret number exactly in both digit and position(called "bulls")
and how many digits match the secret number but locate in the wrong position(called "cows").
Your friend will use successive guesses and hints to eventually derive the secret number.

https://leetcode.com/problems/bulls-and-cows/description/

*/

namespace BullsAndCows {
    class Program {
        static void Main(string[] args) {
            string inputNum = "1807";
            string inputNum2 = "7810";
            string Output = GetHint(inputNum, inputNum2);
            Console.WriteLine(Output);
            Console.ReadKey();
        }

        static public string GetHint(string secret, string guess) {
            int bulls = 0;
            int cows = 0;
            int[] numbers = new int[10];
            for(int i = 0; i < secret.Length; i++) {
                int currentSecret = (int)Char.GetNumericValue(secret[i]);
                int currentGuess = (int)Char.GetNumericValue(guess[i]);
                if(currentGuess == currentSecret) {
                    bulls += 1;
                } else {
                    if(numbers[currentSecret] < 0) {
                        cows++;
                    }
                    if(numbers[currentGuess] > 0) {
                        cows++;
                    }
                    numbers[currentSecret]++;
                    numbers[currentGuess]--;
                }
            }
            return bulls + "A" + cows + "B";
        }
    }
}
