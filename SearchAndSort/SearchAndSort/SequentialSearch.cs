using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Cbest(n) = 1
// Cworst(n) = n
// Cavg(n)=(n+1)/2

namespace SearchAndSort {
    static class SequentialSearch {

        public static void ShowCaseSearch(int[] array) {
            Console.WriteLine("Showcasing sequential Search");
            foreach (int i in array)
                Console.Write(i + " ");
            int key = array[array.Length-1];
            Console.WriteLine("\nFinding "+key);
            int result = Find(array, key);
            Console.WriteLine("Found key at "+result);
            key = 21;
            Console.WriteLine("\nFinding " + key);
            result = Find(array, key);
            Console.WriteLine("Found key at " + result+"\n");
        }

        public static int Find(int [] array, int key) {
            int i = 0;
            while(i<array.Length && array[i] != key) {
                i += 1;
            }
            if (i < array.Length)
                return i;
            return -1;
        }

    }
}
