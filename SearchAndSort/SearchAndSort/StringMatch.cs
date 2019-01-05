using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    static class StringMatch {
        public static int BruteForceStringMatch(string text, string pattern) {
            for(int i=0; i < text.Length - pattern.Length; i++) {
                int j = 0;
                while(j<pattern.Length && pattern[j] == text[i + j]) {
                    j += 1;
                }
                if (j == pattern.Length) {
                    return i;
                }
            }
            return -1;
        }

        public static int HorspoolStringMatch(string text, string pattern) {
            int[] shiftTable = ShiftTable(pattern);
            int i = pattern.Length - 1;
            while (i <= text.Length - 1) {
                int k = 0;
                while(k<=pattern.Length-1 && pattern[pattern.Length - 1 - k] == text[i - k]) {
                    k += 1;
                }
                if (k == pattern.Length) {
                    return i - pattern.Length + 1;
                } else {
                    i += shiftTable[text[i]];
                }
            }
            return -1;
        }

        private static int[] ShiftTable(string pattern) {
            int[] shiftTable = new int[255];
            for(int i=0; i < shiftTable.Length; i++) {
                shiftTable[i] = pattern.Length;
            }
            for(int j=0; j<pattern.Length-1; j++) {
                shiftTable[pattern[j]] = pattern.Length - 1 - j;
            }
            return shiftTable;
        }
    }
}
