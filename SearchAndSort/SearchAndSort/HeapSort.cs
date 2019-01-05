using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    class HeapSort {
        public static void ShowCaseSort(int[] array) {
            Console.WriteLine("Showcasing Heap Sort");
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\nAfter sort method");
            Sort(array);
            foreach (int i in array)
                Console.Write(i + " ");
            Console.WriteLine("\n");
        }

        public static void Sort(int[] array) {
            HeapBottomUp(array);
            for(int i = 0; i<array.Length-2; i++) {
                MaxKeyDeletion(array, array.Length - i);
            }
        }

        private static void MaxKeyDeletion(int [] array, int size) {
            int temp = array[0];
            array[0] = array[size - 1];
            array[size - 1] = temp;

            int[] tempArray = new int[size - 2];
            Array.Copy(array, tempArray, size - 2);
            HeapBottomUp(tempArray);
            for(int i=0; i < tempArray.Length; i++) {
                array[i] = tempArray[i];
            }
        }

        private static void HeapBottomUp(int[] array) {
            int n = array.Length - 1;
            for(int i = (n / 2); i >= 0; i--) {
                int k = i;
                int v = array[k];
                bool heap = false;
                while(!heap && 2*k <= n) {
                    int j = 2 * k;
                    if (j < n) {
                        if(array[j] < array[j + 1]) {
                            j += 1;
                        }
                    }
                    if (v >= array[j]) {
                        heap = true;
                    } else {
                        array[k] = array[j];
                        k = j;
                    }
                }
                array[k] = v;
            }
        }
    }
}
