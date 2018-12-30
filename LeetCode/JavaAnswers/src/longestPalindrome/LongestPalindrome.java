package longestPalindrome;

/*
 * https://leetcode.com/problems/longest-palindrome/description/
 * 
 * Given a string which consists of lowercase or uppercase letters, find the length of the longest palindromes that can be built with those letters.
 * 
 * https://www.cs.cmu.edu/~pattis/15-1XX/common/handouts/ascii.html
 */

public class LongestPalindrome {
	public static void main(String[] args) {
		int testcase = longestPalindrome("ccc");
		System.out.print(testcase);
	}
	
    public static int longestPalindrome(String s) {
    	int[] charactersCount = new int[52];
    	char[] characters = s.toCharArray();
    	for(char c:characters) {
    		if(Character.isUpperCase(c)) {
    			charactersCount[c-65]++;
    		}else {
    			charactersCount[c-71]++;
    		}
    	}
    	int total = 0;
    	for(int i =0;i<charactersCount.length;i++) {
    		total +=(charactersCount[i]/2) * 2; // 5/2*2 = 4
    		if(total %2 == 0 && charactersCount[i]%2==1)
    			total++;
    	}
        return total;
    }

}
