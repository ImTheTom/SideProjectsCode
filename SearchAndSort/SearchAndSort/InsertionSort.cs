using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Cwosrt = O(n^2)
// Cbest = O(n)
// Cavg = O(n^2)
// stable

namespace SearchAndSort {
    static class InsertionSort {

        public static void ShowCaseSort(int[] array) {
            Console.WriteLine("Showcasing Insertion Sort");
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\nAfter sort method");
            Sort(array);
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\n");
        }

        public static int[] Sort(int [] array) {
            for(int i=1; i < array.Length; i++) {
                int v = array[i];
                int j = i - 1;
                while(j>=0 && array[j] > v) {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = v;
            }
            return array;
        }
    }
}
