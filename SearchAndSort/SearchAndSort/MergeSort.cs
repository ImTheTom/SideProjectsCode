using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//cwosrt = O(n log n)

namespace SearchAndSort {
    static class MergeSort {
        public static void ShowCaseSort(int[] array) {
            Console.WriteLine("Showcasing Merge Sort");
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\nAfter sort method");
            Sort(array, 0, array.Length-1);
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\n");
        }

        public static void Sort(int[] array, int low, int high) {
            if(low < high) {
                int middle = (low / 2) + (high / 2);
                Sort(array, low, middle);
                Sort(array, middle + 1, high);
                Merge(array, low, high, middle);
            }
        }

        private static void Merge(int[] array, int low, int high, int middle) {
            int left = low;
            int right = middle + 1;
            int tempIndex = 0;
            int[] temp = new int[(high - low) + 1];
            while (left<=middle && right <= high) {
                if(array[left] <= array[right]) {
                    temp[tempIndex] = array[left];
                    left += 1;
                } else {
                    temp[tempIndex] = array[right];
                    right += 1;
                }
                tempIndex += 1;
            }
            if (left <= middle) {
                while(left <= middle) {
                    temp[tempIndex] = array[left];
                    left += 1;
                    tempIndex += 1;
                }
            }
            if (right <= high) {
                while(right <= high) {
                    temp[tempIndex] = array[right];
                    right += 1;
                    tempIndex += 1;
                }
            }
            for (int i = 0; i < temp.Length; i++) {
                array[low + i] = temp[i];
            }
        }
    }
}
