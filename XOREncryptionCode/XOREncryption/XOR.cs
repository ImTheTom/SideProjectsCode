using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOREncryption {
    static public class XOR {
        private static string key = "";

        public static string EncryptMessage(string inputString, string inputKey = "") {
            string binaryEncodedMessage = ConvertStringToBinary(inputString);
            Console.WriteLine("Converted To Binary results in: " + binaryEncodedMessage);
            if(inputKey == "") {
                inputKey = CreateKeyForEncryption(inputString);
                Console.WriteLine("Key that was generated: " + inputKey);
            }
            string encryptedMessage = XORString(binaryEncodedMessage, inputKey);
            Console.WriteLine("Encrypted Message: " + encryptedMessage);
            return encryptedMessage;
        }

        public static string DecryptMessage(string inputString, string inputKey = "") {
            if(inputKey == "") {
                inputKey = key;
            }
            string decryptedMessage = XORString(inputString, inputKey);
            Console.WriteLine("Decrypted Message: " + decryptedMessage);
            string plaintextMessage = ConvertBinaryToString(decryptedMessage);
            Console.WriteLine("PlainText Message: " + plaintextMessage);
            return plaintextMessage;
        }

        public static string DecryptMessageWithoutKey(string inputString) {
            string plaintextMessage = ConvertBinaryToString(inputString);
            Console.WriteLine("\nPlainText Message: " + plaintextMessage);
            return plaintextMessage;
        }

        // This function calls other functions to convert a string into its binary form
        private static string ConvertStringToBinary(string inputString) {
            int[] arrayOfIntegers = ConvertStringCharactersToIntegers(inputString);
            string[] arrayOfBinaryIntegers = ConvertArrayOfIntegersToBinaryIntegers(arrayOfIntegers);
            arrayOfBinaryIntegers = AddLeftPadding(arrayOfBinaryIntegers);
            return ConvertArrayToSingleString(arrayOfBinaryIntegers);
        }

        // This function calls other functions to convert a binary form string into its normal form
        private static string ConvertBinaryToString(string inputString) {
            string[] arrayOfBinaryStrings = ConvertSingleBinaryStringToArrayOfBinaryStrings(inputString);
            int[] arrayOfIntegers = ConvertArrayOfBinaryStringToArrayOfIntegers(arrayOfBinaryStrings);
            return convertIntegersToString(arrayOfIntegers);
        }

        // This function takes a string input and outputs an arrray of its ascii integer values
        private static int[] ConvertStringCharactersToIntegers(string inputString) {
            int[] outputArrayOfIntegers = new int[inputString.Length];
            for (int i = 0; i < inputString.Length; i++) {
                outputArrayOfIntegers[i] = (int)inputString[i];
            }
            return outputArrayOfIntegers;
        }

        private static string[] ConvertArrayOfIntegersToBinaryIntegers(int[] inputArrayOfIntegers) {
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

        private static string CreateKeyForEncryption(string inputString) {
            string[] keyArray = GenerateKeyForEncryption(inputString);
            key = ConvertArrayToSingleString(keyArray);
            return key;
        }

        private static string[] GenerateKeyForEncryption(string input) {
            int[] ArrayOfRandomIntegers = new int[input.Length];
            Random randomNumbers = new Random();
            for (int i = 0; i < input.Length; i++) {
                ArrayOfRandomIntegers[i] = randomNumbers.Next(256); // 0->255
            }
            string[] GeneratedKeyArray = ConvertArrayOfIntegersToBinaryIntegers(ArrayOfRandomIntegers);
            return AddLeftPadding(GeneratedKeyArray);
        }

        // This function XORs the string with the key to encrypt the message
        private static string XORString(string StringToBeXORed, string keyForEncryption) {
            StringBuilder EncryptedString = new StringBuilder("");
            for (int i = 0; i < StringToBeXORed.Length; i++) {
                EncryptedString.Append(StringToBeXORed[i] ^ keyForEncryption[i]);
            }
            return EncryptedString.ToString();
        }

        // This function converts a single long string into an array of seperate bytes (every 8 bits)
        private static string[] ConvertSingleBinaryStringToArrayOfBinaryStrings(string inputString) {
            string[] outputArray = new string[inputString.Length / 8];
            for (int i = 0; i < outputArray.Length; i++) {
                StringBuilder tempString = new StringBuilder("");
                for (int j = 0; j < 8; j++) {
                    tempString.Append(inputString[(i * 8) + j]);
                }
                outputArray[i] = tempString.ToString();
            }
            return outputArray;
        }

        // Converts array of binary values into its integer value
        private static int[] ConvertArrayOfBinaryStringToArrayOfIntegers(string[] stringArray) {
            int[] arrayOfIntegers = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++) {
                arrayOfIntegers[i] = ConvertBitsToIntegers(stringArray[i]);
            }
            return arrayOfIntegers;
        }

        public static int ConvertBitsToIntegers(string bits) {
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

        // This function turns integer values to their ascii value
        private static string convertIntegersToString(int[] inputArrayOfIntegers) {
            StringBuilder convertedString = new StringBuilder("");
            for (int i = 0; i < inputArrayOfIntegers.Length; i++) {
                convertedString.Append((char)(inputArrayOfIntegers[i]));
            }
            return convertedString.ToString();
        }

        private static string ConvertArrayToSingleString(string[] input) {
            return string.Join("", input);
        }
    }
}
