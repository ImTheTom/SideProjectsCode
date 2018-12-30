package happyNumber;

import java.util.LinkedList;

/*
 * https://leetcode.com/problems/happy-number/description/
 * 
 * Write an algorithm to determine if a number is "happy".
 */

public class HappyNumber {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		boolean a = isHappy(2);
		System.out.println(a);
	}
	
    public static boolean isHappy(int n) {
    	int[] loop = {4,16,37,58,89,145,42,20};
    	while(n!=1) {
    		for(int i: loop) {
    			if(i==n)
    				return false;
    		}
    	LinkedList<Integer> stack = new LinkedList<Integer>();
    	while(n > 0) {
    		stack.push(n%10);
    		n = n/10;
    	}
    	n = numbers(stack);
    	System.out.println(n);
    	}
    	if(n==1)
    		return true;
    	return false;
    }
    
    public static int numbers(LinkedList<Integer> stack) {
    	int total = 0;
    	for(int i:stack) {
    		total +=Math.pow(i, 2);
    	}
		return total;
    }

}
