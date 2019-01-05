using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    class Node {
        public char value;
        public int intValue;
        public Node firstChild;
        public Node firstSibling;

        public Node(char value) {
            this.value = value;
            this.intValue = (int)value;
            this.firstChild = null;
            this.firstSibling = null;
        }

        public Node(int value) {
            this.value = (char)value;
            this.intValue = value;
            this.firstChild = null;
            this.firstSibling = null;
        }
    }
}
