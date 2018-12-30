package reverseInteger;

/*
 * https://leetcode.com/problems/reverse-integer/description/
 * 
 * Given a 32-bit signed integer, reverse digits of an integer.
 */

public class ReverseInteger {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int flipped = reverse(-96463);
		System.out.println(flipped);
	}

	public static int reverse(int x) {
		try {
			Boolean negative = false;
			if (x < 0)
				negative = true;
			String numAsString = Integer.toString(x);
			char[] reversedArray = new char[numAsString.length()];
			int j = 0;
			for (int i = numAsString.length() - 1; i >= 0; i--) {
				if (negative && i == 0) {
					reversedArray[j] = '0';
				} else {
					reversedArray[j] = numAsString.charAt(i);
				}
				j++;
			}
			if (negative) {
				char last = 0;
				char next = 0;
				for (int i = 0; i < numAsString.length() - 1; i++) {
					if (i == 0) {
						last = reversedArray[i + 1];
						reversedArray[i + 1] = reversedArray[i];
					} else {
						next = reversedArray[i + 1];
						reversedArray[i + 1] = last;
						last = next;
					}
				}
				reversedArray[0] = '-';
			}

			int reversed = Integer.parseInt(new String(reversedArray));

			return reversed;
		} catch (Exception e) {
			return 0;
		}
	}

}
