package detectCapital;

/*
 * https://leetcode.com/problems/detect-capital/description/
 * 
 * Given a word, you need to judge whether the usage of capitals in it is right or not. 
 */

public class DetectCapital {
	
	public static void main(String[] args) {
		boolean testcase = detectCapitalUse("g");
		System.out.print(testcase);
	}

	
    public static boolean detectCapitalUse(String word) {
    	char[] characters = word.toCharArray();
    	int totalCapitals=0;
    	int index=0;
    	for(int i =0; i<characters.length;i++) {
    		if(Character.isUpperCase(characters[i])) {
    			totalCapitals+=1;
    			index = i;
    		}
    	}
    	if(totalCapitals==word.length())
    		return true;
    	if(totalCapitals==1 && index ==0)
    		return true;
    	if(totalCapitals==0) {
    		return true;
    	}
    	return false;
    }
}
