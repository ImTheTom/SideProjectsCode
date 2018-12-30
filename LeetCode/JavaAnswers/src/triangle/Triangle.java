package triangle;

/*
 * https://leetcode.com/problems/triangle/description/
 * 
 * Given a triangle, find the minimum path sum from top to bottom. Each step you may move to adjacent numbers on the row below.
 * 
 * Assume all are positives
 */

import java.util.ArrayList;
import java.util.List;

public class Triangle {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		List <Integer> top = new ArrayList<Integer>();
		top.add(2);
		List <Integer> topMiddle = new ArrayList<Integer>();
		topMiddle.add(3);
		topMiddle.add(4);
		List <Integer> bottomMiddle = new ArrayList<Integer>();
		bottomMiddle.add(5);
		bottomMiddle.add(6);
		bottomMiddle.add(7);
		List <Integer> bottom = new ArrayList<Integer>();
		bottom.add(4);
		bottom.add(1);
		bottom.add(8);
		bottom.add(3);
		List<List<Integer>> triangle = new ArrayList<List<Integer>>();
		triangle.add(top);
		triangle.add(topMiddle);
		triangle.add(bottomMiddle);
		triangle.add(bottom);
		int result = minimumTotal(triangle);
		System.out.println(result);
	}
	
    public static int minimumTotal(List<List<Integer>> triangle) {
    	int[] result = new int[triangle.size()+1];
        for(int i=triangle.size()-1;i>=0;i--) {
        	for(Integer j =0;j<triangle.get(i).size();j++) {
        		result[j] = Math.min(result[j],result[j+1])+triangle.get(i).get(j);
        	}
            for(int k:result)
            	System.out.print(k+ " ");
            System.out.println("\n");
        }
        return result[0];
    }

}
