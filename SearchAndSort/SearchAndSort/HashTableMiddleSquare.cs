using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    class HashTableMiddleSquare {
        private int[] Array;
        private int bits;

        public HashTableMiddleSquare(int[] Array, int bits) {
            this.Array = Array;
            this.bits = bits;
        }

        public int getValue(int key) {
            int value = key *key;
            string stringValue = value.ToString();
            stringValue = stringValue.Substring((stringValue.Length - 3) / 2, 3);
            value = int.Parse(stringValue);
            return value;
        }

        public void Hash(int key) {
            int value = getValue(key);
            if (this.Array[value] == 0) {
                this.Array[value] = key;
                return;
            } else {
                linearProbeAdd(key, value);
            }
        }

        private void linearProbeAdd(int key, int value) {
            while (this.Array[value] != 0) {
                value += 1;
                if (value == this.Array.Length) {
                    value = 0;
                }
            }
            Array[value] = key;
        }

        public int LookUp(int key) {
            int value = getValue(key);
            if (this.Array[value] == key) {
                return value;
            } else {
                return LinearProbeLookUp(key, value);
            }
        }

        private int LinearProbeLookUp(int key, int value) {
            int i = 0;
            while (i < this.Array.Length) {
                value += 1;
                if (value == this.Array.Length) {
                    value = 0;
                }
                if (key == this.Array[value]) {
                    return value;
                }
                i += 1;
            }
            return -1;
        }

        public void Delete(int key) {
            int value = getValue(key);
            if (Array[value] == key) {
                Array[value] = 0;
                return;
            } else {
                LinearDelete(key, value);
            }
        }

        public void LinearDelete(int key, int value) {
            int i = 0;
            while (i < Array.Length) {
                value += 1;
                if (value == Array.Length) {
                    value = 0;
                }
                if (key == Array[value]) {
                    Array[value] = 0;
                    return;
                }
                i += 1;
            }
        }
    }
}
