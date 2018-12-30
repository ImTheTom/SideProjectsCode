using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndSort {
    static class TreeTraversal {
        public static void DoBFT(Node x) {
            Console.WriteLine("Doing BF Traversal");
            BFT(x);
            Console.WriteLine();
        }

        public static void DoDFT(Node x) {
            Console.WriteLine("Doing DF Traversal");
            DFT(x);
            Console.WriteLine();
        }

        private static void BFT(Node x) {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(x);
            while (q.Count != 0) {
                Node r = q.Dequeue();
                Console.Write(r.value);
                r = r.firstChild;
                while (r != null) {
                    q.Enqueue(r);
                    r = r.firstSibling;
                }
            }
        }

        private static void DFT(Node x) {
            Stack<Node> s = new Stack<Node>();
            s.Push(x);
            while(s.Count != 0) {
                Node r = s.Pop();
                do {
                    Console.Write(r.value);
                    if (r.firstSibling != null) {
                        s.Push(r.firstSibling);
                    }
                    r = r.firstChild;
                } while (r != null);
            }
        }
    }
}
