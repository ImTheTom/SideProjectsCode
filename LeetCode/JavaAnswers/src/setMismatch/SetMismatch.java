package setMismatch;

/*
 * https://leetcode.com/problems/set-mismatch/description/
 */

public class SetMismatch {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int [] testInput = new int[] {1,2,2,4};
		int[] result = findErrorNums(testInput);
		for(int i=0;i<result.length;i++)
			System.out.println(result[i]);
	}
	
    public static int[] findErrorNums(int[] nums) {
    	int[] resultArray = new int[2];
        int index = findDuplicate(nums);
        resultArray[0] = index;
        resultArray[1] = findNewNumber(index, nums);
        return resultArray;
    }
    
    public static int findDuplicate(int[] nums) {
    	for(int i = 0;i<nums.length-1;i++) {
    		for(int j = i+1;j<nums.length;j++) {
    			if(nums[i] == nums[j]) {
    				return j;
    			}
    		}
    	}
    	return nums[nums.length-1];
    }
    
    public static int findNewNumber(int index,int[] nums) {
    	if(index == nums.length-1) {
    		int current =  (nums[nums.length-1]+nums[nums.length-2])/2;
    		if(current == nums[index])
    			current +=1;
    		return current;
    	}
    	return (nums[index+1]+nums[index-1])/2;
    }

}
