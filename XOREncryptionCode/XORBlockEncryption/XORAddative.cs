using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XORBlockEncryption {
    static public class XORAddative {
        public static string key = "";

        public static string[] EncryptMessage(string inputString) {
            string[] formattedInputSting = CreateStringForEncryption(inputString);
            string[] encryptedStringArray =  XORBlockCipherEncrypt(formattedInputSting);
            Console.Write("\nThe encrypted message translates to: ");
            foreach(string i in encryptedStringArray) {
                Console.Write(i);
            }
            Console.WriteLine();
            return encryptedStringArray;
        }

        public static string DecryptMessage(string[] inputString) {
            string[] decreyptedMessage = XORBlockCipherDecrypt(inputString);
            string decryptedMessageString =  CreateStringFromBinaryString(decreyptedMessage);
            Console.WriteLine("After the decryption it translates to: "+decryptedMessageString + "\n");
            return decryptedMessageString;
        }

        public static string DecryptMessageWithoutKey(string[] inputString) {
            string decryptedMessageString = CreateStringFromBinaryString(inputString);
            Console.WriteLine("After the decryption it translates to: " + decryptedMessageString);
            return decryptedMessageString;
        }

        public static void GenerateKeyForEncryption() {
            Random randomNumbers = new Random();
            int randomNumber = randomNumbers.Next(256); // 0->255
            string[] createdKey = CreateStringForEncryption(randomNumber.ToString());
            key = createdKey[0];
            Console.WriteLine("They Key that was generated for the intial block is: " + key + "\n");
        }

        public static void SetKeyForEncryption(string keyForEncryption) {
            key = keyForEncryption;
        }

        // This function calls other functions to turn a string into its binary form
        private static string[] CreateStringForEncryption(string inputString) {
            int[] arrayOfIntegers = ConvertStringCharactersToIntegers(inputString);
            string[] arrayOfBinaryIntegers = ConvertArrayOfIntegersToBinary(arrayOfIntegers);
            return AddLeftPadding(arrayOfBinaryIntegers);
        }

        // This function turns a string into an array characters of their ascii values
        private static int[] ConvertStringCharactersToIntegers(string inputString) {
            int[] outputArrayOfIntegers = new int[inputString.Length];
            for (int i = 0; i < inputString.Length; i++) {
                outputArrayOfIntegers[i] = (int)inputString[i];
            }
            return outputArrayOfIntegers;
        }

        private static string[] ConvertArrayOfIntegersToBinary(int[] inputArrayOfIntegers) {
            string[] outputArrayOfBinaryIntegers = new string[inputArrayOfIntegers.Length];
            for (int index = 0; index < inputArrayOfIntegers.Length; index++) {
                int currentInteger = inputArrayOfIntegers[index];
                const int mask = 1;
                var binaryNumber = string.Empty;
                while (currentInteger > 0) {
                    binaryNumber = (currentInteger & mask) + binaryNumber;
                    currentInteger = currentInteger >> 1;
                }
                outputArrayOfBinaryIntegers[index] = binaryNumber;
            }
            return outputArrayOfBinaryIntegers;
        }

        private static string[] AddLeftPadding(string[] InputArrayOfBinaryIntegers) {
            string[] OutputArrayOfBinaryIntegersPadded = new string[InputArrayOfBinaryIntegers.Length];
            for (int i = 0; i < InputArrayOfBinaryIntegers.Length; i++) {
                string currentString = InputArrayOfBinaryIntegers[i];
                while (currentString.Length < 8) {
                    currentString = "0" + currentString;
                }
                OutputArrayOfBinaryIntegersPadded[i] = currentString;
            }
            return OutputArrayOfBinaryIntegersPadded;
        }

        private static string[] XORBlockCipherEncrypt(string[] InputArrayOfBinaryIntegers) {
            string[] OutputArrayOfEncryptedIntegersPadded = new string[InputArrayOfBinaryIntegers.Length];
            string currentKey = key;
            string nextKey;
            for (int i = 0; i < InputArrayOfBinaryIntegers.Length; i++) {
                nextKey = InputArrayOfBinaryIntegers[i];
                string currentWord = InputArrayOfBinaryIntegers[i];
                StringBuilder EncryptedString = new StringBuilder("");
                for (int j = 0; j < 8; j++) {
                    int currentInteger = Int32.Parse(currentWord[j].ToString());
                    int currentKeyInteger = Int32.Parse(currentKey[j].ToString());
                    EncryptedString.Append(currentInteger ^ currentKeyInteger);
                }
                OutputArrayOfEncryptedIntegersPadded[i] = EncryptedString.ToString();
                currentKey = nextKey;
            }
            return OutputArrayOfEncryptedIntegersPadded;
        }

        private static string[] XORBlockCipherDecrypt(string[] InputArrayOfBinaryIntegers) {
            string[] OutputArrayOfEncryptedIntegersPadded = new string[InputArrayOfBinaryIntegers.Length];
            string currentKey = key;
            for (int i = 0; i < InputArrayOfBinaryIntegers.Length; i++) {
                string currentWord = InputArrayOfBinaryIntegers[i];
                StringBuilder EncryptedString = new StringBuilder("");
                for (int j = 0; j < 8; j++) {
                    int currentInteger = Int32.Parse(currentWord[j].ToString());
                    int currentKeyInteger = Int32.Parse(currentKey[j].ToString());
                    EncryptedString.Append(currentInteger ^ currentKeyInteger);
                }
                OutputArrayOfEncryptedIntegersPadded[i] = EncryptedString.ToString();
                currentKey = EncryptedString.ToString();
            }
            return OutputArrayOfEncryptedIntegersPadded;
        }

        // This function calls other functions to turn binary string into a string
        private static string CreateStringFromBinaryString(string[] inputString) {
            int[] arrayOfIntegers = ConvertArrayOfStringToArrayOfIntegers(inputString);
            return convertIntegersToString(arrayOfIntegers);
        }

        // This function turns an array of binary values into an array of integer values
        private static int[] ConvertArrayOfStringToArrayOfIntegers(string[] stringArray) {
            int[] arrayOfIntegers = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++) {
                arrayOfIntegers[i] = ConvertBitsToIntegers(stringArray[i]);
            }
            return arrayOfIntegers;
        }

        // This function turns binary numbers into their normal ascii integer form
        private static int ConvertBitsToIntegers(string bits) {
            var reversedBits = bits.Reverse().ToArray();
            var currentInteger = 0;
            for (var power = 0; power < reversedBits.Count(); power++) {
                var currentBit = reversedBits[power];
                if (currentBit == '1') {
                    var currentNum = (int)Math.Pow(2, power);
                    currentInteger += currentNum;
                }
            }
            return currentInteger;
        }

        // This function turns ascii values into their normal form
        private static string convertIntegersToString(int[] inputArrayOfIntegers) {
            StringBuilder convertedString = new StringBuilder("");
            for (int i = 0; i < inputArrayOfIntegers.Length; i++) {
                convertedString.Append((char)(inputArrayOfIntegers[i]));
            }
            return convertedString.ToString();
        }
    }
}
