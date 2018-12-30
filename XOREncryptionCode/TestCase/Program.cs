using System;
using XOREncryption;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XORBlockEncryption;

namespace TestCase {
    class Program {
        public static int totalScore = 0;

        static void Main(string[] args) {
            XORAddative.SetKeyForEncryption("10110011");
            RunTests();
            Console.WriteLine("\n\nTotal score of tests are a {0} out of 16", totalScore);
            if (totalScore == 16) {
                Console.WriteLine("\n\nAll Tests passed");
            } else {
                Console.WriteLine("\n\n{0} failed", 16 - totalScore);
            }
            Console.ReadLine();
        }

        public static void RunTests() {
            RunEncryptMessagesTests();
            RunAddativeEncryptMessagesTests();
            RunDecryptMessageWithoutKeyTests();
            RunAddativeDecryptMessageWithoutKeyTests();
            RunDecryptMessagesTests();
            RunAddativeDecryptMessagesTests();
            RunEncryptAndDecryptMessagesTests();
            RunAddativeEncryptAndDecryptMessagesTests();
        }

        public static void RunEncryptMessagesTests() {
            Console.WriteLine("Running the Encrypt Messages Tests");
            if (RunEncryptMessagesTest1()) {
                Console.WriteLine("Convert to 'This is a basic test' succeeded");
            } else {
                Console.WriteLine("Convert to 'This is a basic test' failed");
            }
            Console.WriteLine();
            if (RunEncryptMessagesTest2()) {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' succeeded");
            } else {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' failed");
            }
        }

        public static void RunAddativeEncryptMessagesTests() {
            Console.WriteLine("Running the ADDATIVE Encrypt Messages Tests");
            if (RunAddativeEncryptMessagesTest1()) {
                Console.WriteLine("Convert to 'This is a basic test' succeeded");
            } else {
                Console.WriteLine("Convert to 'This is a basic test' failed");
            }
            Console.WriteLine();
            if (RunAddativeEncryptMessagesTest2()) {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' succeeded");
            } else {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' failed");
            }
        }

        public static void RunDecryptMessageWithoutKeyTests() {
            Console.WriteLine("\nRunning the Decrypt Messages Tests");
            if (RunDecryptMessagesWithoutKeyTest1()) {
                Console.WriteLine("Convert to 'This is a basic test' succeeded");
            } else {
                Console.WriteLine("Convert to 'This is a basic test' failed");
            }
            Console.WriteLine();
            if (RunDecryptMessagesWithoutKeyTest2()) {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' succeeded");
            } else {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' failed");
            }
        }

        public static void RunAddativeDecryptMessageWithoutKeyTests() {
            Console.WriteLine("\nRunning the Decrypt Messages Tests");
            if (RunAddativeDecryptMessagesWithoutKeyTest1()) {
                Console.WriteLine("Convert to 'This is a basic test' succeeded");
            } else {
                Console.WriteLine("Convert to 'This is a basic test' failed");
            }
            Console.WriteLine();
            if (RunAddativeDecryptMessagesWithoutKeyTest2()) {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' succeeded");
            } else {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' failed");
            }
        }

        public static void RunDecryptMessagesTests() {
            Console.WriteLine("\nRunning the Decrypt Messages Tests");
            if (RunDecryptMessagesTest1()) {
                Console.WriteLine("Convert to 'This is a basic test' succeeded");
            } else {
                Console.WriteLine("Convert to 'This is a basic test' failed");
            }
            Console.WriteLine();
            if (RunDecryptMessagesTest2()) {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' succeeded");
            } else {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' failed");
            }
        }

        public static void RunAddativeDecryptMessagesTests() {
            Console.WriteLine("\nRunning the ADDATIVE Decrypt Messages Tests");
            if (RunAddativeDecryptMessagesTest1()) {
                Console.WriteLine("Convert to 'This is a basic test' succeeded");
            } else {
                Console.WriteLine("Convert to 'This is a basic test' failed");
            }
            Console.WriteLine();
            if (RunAddativeDecryptMessagesTest2()) {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' succeeded");
            } else {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' failed");
            }
        }

        public static void RunEncryptAndDecryptMessagesTests() {
            Console.WriteLine("\nRunning the Encrypt and Decrypt Messages Tests");
            if (RunEncryptAndDecryptMessagesTest1()) {
                Console.WriteLine("Convert to 'This is a basic test' succeeded");
            } else {
                Console.WriteLine("Convert to 'This is a basic test' failed");
            }
            Console.WriteLine();
            if (RunEncryptAndDecryptMessagesTest2()) {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' succeeded");
            } else {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' failed");
            }
        }

        public static void RunAddativeEncryptAndDecryptMessagesTests() {
            Console.WriteLine("\nRunning the ADDATIVE Encrypt and Decrypt Messages Tests");
            if (RunAddativeEncryptAndDecryptMessagesTest1()) {
                Console.WriteLine("Convert to 'This is a basic test' succeeded");
            } else {
                Console.WriteLine("Convert to 'This is a basic test' failed");
            }
            Console.WriteLine();
            if (RunAddativeEncryptAndDecryptMessagesTest2()) {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' succeeded");
            } else {
                Console.WriteLine("Convert to '9Qqs0)(>?a,+=' failed");
            }
        }

        public static bool RunEncryptMessagesTest1() {
            Console.WriteLine("Running first encrypt message test");
            string encryptTest = XOR.EncryptMessage("This is a basic test", "1011011000100111001100011011010111101110101111011001100110000001100001001000011110000101000001001101011000111101001101111110110000010000011011001001110101000010");
            if (encryptTest != "1110001001001111010110001100011011001110110101001110101010100001111001011010011111100111011001011010010101010100010101001100110001100100000010011110111000110110")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunEncryptMessagesTest2() {
            Console.WriteLine("Running second encrypt message test");
            string encryptTest = XOR.EncryptMessage("9Qqs0)(>?a,+=", "01010100000111110100010000010011100100001000100001011010101011001111111100010001110001000011010011010101");
            if (encryptTest != "01101101010011100011010101100000101000001010000101110010100100101100000001110000111010000001111111101000")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunAddativeEncryptMessagesTest1() {
            Console.WriteLine("Running first addative encrypt message test");
            string[] encryptTest = XORAddative.EncryptMessage("This is a basic test");
            if (encryptTest[1] != "00111100" || encryptTest[5] != "01001001" || encryptTest[7] != "01010011" || encryptTest[8] != "01000001")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunAddativeEncryptMessagesTest2() {
            Console.WriteLine("Running second addative encrypt message test");
            string[] encryptTest = XORAddative.EncryptMessage("9Qqs0)(>?a,+=");
            if (encryptTest[1] != "01101000" || encryptTest[5] != "00011001" || encryptTest[7] != "00010110" || encryptTest[8] != "00000001" || encryptTest[12] != "00010110")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunDecryptMessagesWithoutKeyTest1() {
            Console.WriteLine("Running first decrypt message without key test");
            string decryptTest = XOR.EncryptMessage("This is a basic test");
            decryptTest = XOR.DecryptMessageWithoutKey(decryptTest);
            if (decryptTest == "This is a basic test")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunDecryptMessagesWithoutKeyTest2() {
            Console.WriteLine("Running second decrypt message without key test");
            string decryptTest = XOR.EncryptMessage("9Qqs0)(>?a,+=");
            decryptTest = XOR.DecryptMessageWithoutKey(decryptTest);
            if (decryptTest == "9Qqs0)(>?a,+=")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunAddativeDecryptMessagesWithoutKeyTest1() {
            Console.WriteLine("Running first ADDATIVE decrypt message without key test");
            string[] decryptTest = XORAddative.EncryptMessage("This is a basic test");
            string decryptedTest = XORAddative.DecryptMessageWithoutKey(decryptTest);
            if (decryptedTest == "This is a basic test")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunAddativeDecryptMessagesWithoutKeyTest2() {
            Console.WriteLine("Running first ADDATIVE decrypt message without key test");
            string[] decryptTest = XORAddative.EncryptMessage("9Qqs0)(>?a,+=");
            string decryptedTest = XORAddative.DecryptMessageWithoutKey(decryptTest);
            if (decryptedTest == "9Qqs0)(>?a,+=")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunDecryptMessagesTest1() {
            Console.WriteLine("Running first decrypt message test");
            string decryptTest = XOR.DecryptMessage("1110001001001111010110001100011011001110110101001110101010100001111001011010011111100111011001011010010101010100010101001100110001100100000010011110111000110110", "1011011000100111001100011011010111101110101111011001100110000001100001001000011110000101000001001101011000111101001101111110110000010000011011001001110101000010");
            if (decryptTest != "This is a basic test")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunDecryptMessagesTest2() {
            Console.WriteLine("Running second decrypt message test");
            string decryptTest = XOR.DecryptMessage("01101101010011100011010101100000101000001010000101110010100100101100000001110000111010000001111111101000", "01010100000111110100010000010011100100001000100001011010101011001111111100010001110001000011010011010101");
            if (decryptTest != "9Qqs0)(>?a,+=")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunAddativeDecryptMessagesTest1() {
            Console.WriteLine("Running first addative decrypt message test");
            string[] firstAddativeTest = new string[] {"11100111", "00111100", "00000001", "00011010", "01010011",
                                                         "01001001", "00011010", "01010011", "01000001", "01000001",
                                                         "01010100", "00010001", "00010110", "00000111"};
            string decryptTest = XORAddative.DecryptMessage(firstAddativeTest);
            if (decryptTest != "This is a test")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunAddativeDecryptMessagesTest2() {
            Console.WriteLine("Running second addative decrypt message test");
            string[] secondAddativeTest = new string[] {"10001010", "01101000", "00100000", "00000010", "01000011",
                                                         "00011001", "00000001", "00010110", "00000001", "01011110",
                                                         "01001101", "00000111", "00010110"};
            string decryptTest = XORAddative.DecryptMessage(secondAddativeTest);
            if (decryptTest != "9Qqs0)(>?a,+=")
                return false;
            totalScore += 1;
            return true;
        }


        public static bool RunEncryptAndDecryptMessagesTest1() {
            Console.WriteLine("Running first encrypt and decrypt message test");
            string encryptAndDecryptTest = XOR.EncryptMessage("This is a basic test");
            encryptAndDecryptTest = XOR.DecryptMessage(encryptAndDecryptTest);
            if (encryptAndDecryptTest != "This is a basic test")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunEncryptAndDecryptMessagesTest2() {
            Console.WriteLine("Running second encrypt and decrypt message test");
            string encryptAndDecryptTest = XOR.EncryptMessage("9Qqs0)(>?a,+=");
            encryptAndDecryptTest = XOR.DecryptMessage(encryptAndDecryptTest);
            if (encryptAndDecryptTest != "9Qqs0)(>?a,+=")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunAddativeEncryptAndDecryptMessagesTest1() {
            Console.WriteLine("Running first addative encrypt and decrypt message test");
            string[] encryptAndDecryptTest = XORAddative.EncryptMessage("This is a basic test");
            string DecryptTest = XORAddative.DecryptMessage(encryptAndDecryptTest);
            if (DecryptTest != "This is a basic test")
                return false;
            totalScore += 1;
            return true;
        }

        public static bool RunAddativeEncryptAndDecryptMessagesTest2() {
            Console.WriteLine("Running first addative encrypt and decrypt message test");
            string[] encryptAndDecryptTest = XORAddative.EncryptMessage("9Qqs0)(>?a,+=");
            string DecryptTest = XORAddative.DecryptMessage(encryptAndDecryptTest);
            if (DecryptTest != "9Qqs0)(>?a,+=")
                return false;
            totalScore += 1;
            return true;
        }
    }
}
