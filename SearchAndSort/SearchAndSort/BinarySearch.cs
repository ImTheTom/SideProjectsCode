using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Cavg = logn
// Cworst = logn +1
// cbest = 1

namespace SearchAndSort {
    static class BinarySearch {

        public static void ShowCaseSearch(int[] array) {
            Console.WriteLine("Showcasing Binary Search");
            foreach (int i in array)
                Console.Write(i + " ");
            int key = array[array.Length/2];
            Console.WriteLine("\nFinding " + key);
            int result = Find(array, key);
            Console.WriteLine("Found key at " + result);
            key = 21;
            Console.WriteLine("\nFinding " + key);
            result = Find(array, key);
            Console.WriteLine("Found key at " + result + "\n");
        }

        public static int Find(int [] array, int key) {
            int low = 0;
            int high = array.Length - 1;
            int middle;
            while(low <= high) {
                middle = (low + high) / 2;
                if (key == array[middle])
                    return middle;
                else if (key < array[middle])
                    high = middle - 1;
                else
                    low = middle + 1;
            }
            return -1;
        }
    }
}
