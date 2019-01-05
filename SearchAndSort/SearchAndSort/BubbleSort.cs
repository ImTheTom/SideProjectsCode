using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// O(n^2)
// Stable

namespace SearchAndSort {
    static class BubbleSort {
        public static void ShowCaseSort(int[] array) {
            Console.WriteLine("Showcasing Bubble Sort");
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\nAfter sort method");
            Sort(array);
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\n");
        }

        public static int[] Sort(int[] array) {
            for (int i = 0; i < array.Length - 1; i++) {
                for(int j =0; j < array.Length - 1 - i;j++) {
                    if (array[j + 1] < array[j]) {
                        int temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
            return array;
        }
    }
}
