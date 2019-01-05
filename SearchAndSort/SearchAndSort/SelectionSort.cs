using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    class SelectionSort {
        public static void ShowCaseSort(int[] array) {
            Console.WriteLine("Showcasing Selection Sort");
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\nAfter sort method");
            Sort(array);
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\n");
        }

        public static int[] Sort(int[] array) {
            for(int i=0; i<array.Length-1; i++) {
                int min = i;
                for(int j=i+1; j < array.Length; j++) {
                    if (array[j] < array[min])
                        min = j;
                }
                int temp = array[i];
                array[i] = array[min];
                array[min] = temp;
            }
            return array;
        }
    }
}
