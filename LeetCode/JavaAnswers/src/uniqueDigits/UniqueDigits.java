package uniqueDigits;

/*
 * https://leetcode.com/problems/count-numbers-with-unique-digits/description/
 * 
 * Given a non-negative integer n, count all numbers with unique digits, x, where 0 ≤ x < 10n.
 * 
 * currently time limit exceeded at 8/9 test cases
 */

public class UniqueDigits {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int result = countNumbersWithUniqueDigits(8);
		System.out.println(result);
	}
	
    public static int countNumbersWithUniqueDigits(int n) {
        int number = (int) Math.pow(10, n);
        int total = 0;
        for(int i =0; i<number;i++) {
        	boolean unique =checkUnique(i);
        	if(unique) {
        		total+=1;
        	}
        }
        return total;
    }
	
    public static boolean checkUnique(int n) {2/
        String numberAsString = String.valueOf(n);
        boolean unqiue = true;
        for(int i=0; i<numberAsString.length()-1;i++) {
        	char current = numberAsString.charAt(i);
        	for(int j=i+1;j<numberAsString.length();j++) {
        		if(current==numberAsString.charAt(j)) {
        			unqiue = false;
        		}
        	}
        }
        return unqiue;
    }

}
