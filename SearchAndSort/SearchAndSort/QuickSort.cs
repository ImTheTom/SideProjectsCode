using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// worst = O(n^2)
// avg = O(n log n)

namespace SearchAndSort {
    static class QuickSort {
        public static void ShowCaseSort(int[] array) {
            Console.WriteLine("Showcasing Quick Sort");
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\nAfter sort method");
            Sort(array, 0, array.Length - 1);
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\n");
        }

        public static void Sort(int[] array, int low, int high) {
            int left = low;
            int right = high;
            int pivot = array[low + (high - low) / 2];
            while (left < right) {
                while (array[left] < pivot)
                    left += 1;
                while (array[right] > pivot)
                    right -= 1;
                if(left <= right) {
                    Swap(array, left, right);
                    left += 1;
                    right -= 1;
                }
            }
            if(low < right) {
                Sort(array, low, right);
            }
            if(left < high) {
                Sort(array, left, high);
            }
        }

        public static void Swap(int [] array, int firstIndex, int secondIndex) {
            int temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }
    }
}
