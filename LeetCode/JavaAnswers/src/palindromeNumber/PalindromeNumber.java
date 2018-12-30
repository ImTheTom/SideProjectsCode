package palindromeNumber;

/*
 * https://leetcode.com/problems/palindrome-number/solution/
 * 
 * Determine whether an integer is a palindrome. Do this without extra space.
 */

public class PalindromeNumber {
	public static void main(String[] args) {
		boolean x = isPalindrome(10);
		System.out.print(x);
		
	}
    public static boolean isPalindrome(int x) {
    	if(x<0 || (x%10 == 0 && x!=0)){
    		return false;
    	}
    	int reversed = 0;
        while(x>reversed) {
        	reversed = reversed*10+x%10;
        	x/=10;
        }
        return x == reversed || x== reversed/10;
    }

}
