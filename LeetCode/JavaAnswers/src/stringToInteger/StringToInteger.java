package stringToInteger;

/*
 * https://leetcode.com/problems/string-to-integer-atoi/description/
 * 
 * Implement atoi to convert a string to an integer.
 */

public class StringToInteger {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int input = myAtoi("-2147483649");
		System.out.print(input);
	}
	
    public static int myAtoi(String str) {
        if(str.length()==0) {
        	return 0;
        }
        long currentSum = 0;
        str = str.trim();
        int start = 0;
        boolean negative = false;
        if(str.charAt(0)=='-') {
        	negative = true;
        	start+=1;
        }else if(str.charAt(0)=='+') {
        	start+=1;
        }
        for(int i =start ;i<str.length();i++) {
        	if(!Character.isDigit(str.charAt(i))) {
        		if(negative)
        			return (int) currentSum*-1;
        		else
        			return (int) currentSum;
        	}
        	currentSum= currentSum*10 + Integer.parseInt(String.valueOf(str.charAt(i))); // it shifts it over then adds the column
        	if(negative && -1*currentSum<Integer.MIN_VALUE)
        		return Integer.MIN_VALUE;
        	else if(currentSum >Integer.MAX_VALUE && !negative)
        		return Integer.MAX_VALUE;
        }
        if(negative) {
        	return (int)currentSum*-1;
        }
        return (int)currentSum;
    }

}
