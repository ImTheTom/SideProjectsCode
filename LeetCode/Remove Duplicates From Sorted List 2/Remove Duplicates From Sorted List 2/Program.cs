using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Given a sorted linked list, delete all nodes that have duplicate numbers, leaving only distinct numbers from the original list.

https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/description/

*/
 public class ListNode {
      public int val;
     public ListNode next;
      public ListNode(int x) { val = x; }
 }

namespace Remove_Duplicates_From_Sorted_List_2 {
    class Program {
        static void Main(string[] args) {
        }
        static public ListNode DeleteDuplicates(ListNode head) {
            if (head == null) return null;
            ListNode outputHead = new ListNode(0);
            outputHead.next = head;
            ListNode previousHead = outputHead;
            ListNode currentHead = head;
            while (currentHead != null) {
                while (currentHead.next!= null && currentHead.val == currentHead.next.val) {
                    currentHead = currentHead.next;
                }
                if(previousHead.next == currentHead) {
                    previousHead = previousHead.next;
                } else {
                    previousHead.next = currentHead.next;
                }
                currentHead = currentHead.next;
            }
            return outputHead.next;
        }
    }
}
