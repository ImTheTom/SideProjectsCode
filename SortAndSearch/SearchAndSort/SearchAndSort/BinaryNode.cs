using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    class BinaryNode {
        public char value;
        public int intValue;
        public BinaryNode leftChild;
        public BinaryNode rightChild;
        
        public BinaryNode(char value) {
            this.value = value;
            this.intValue = (int)value;
            this.leftChild = null;
            this.rightChild = null;
        }

        public BinaryNode(int value) {
            this.value = (char)value;
            this.intValue = value;
            this.leftChild = null;
            this.rightChild = null;
        }
    }
}
