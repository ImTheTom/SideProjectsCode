package rotateFunction;

/*
 * https://leetcode.com/problems/rotate-function/description/
 */

public class RotateFunction {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int[] A = new int[] {4, 3, 2, 6};
		int result = maxRotateFunction(A);
		System.out.println(result);
	}
	
    public static int maxRotateFunction(int[] A) {
    	if(A.length<=1)
    		return 0;
        int max = Integer.MIN_VALUE;
        for(int i=0; i<A.length;i++) {
        	int temp = 0;
        	for(int j = 0; j<A.length;j++) {
        		temp +=A[j]*j;
        	}
        	if(temp>max)
        		max =temp;
        	A = shiftArray(A);
        }
        return max;
    }
    
    public static int[] shiftArray(int[] A) {
    	int temp = A[0];
    	int temp2 = A[1];
    	for(int i=1; i<A.length-1;i++) {
    		A[i] = temp;
    		temp = temp2;
    		temp2 = A[i+1];
    	}
    	A[A.length-1] = temp;
    	A[0]=temp2;
    	return A;
    }

}
