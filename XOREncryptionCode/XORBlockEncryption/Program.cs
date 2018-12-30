using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XORBlockEncryption {
    class Program {
        static void Main(string[] args) {
            XORAddative.SetKeyForEncryption("10110011");
            bool runProgram = true;
            while (runProgram) {
                Console.Write("What Message would you like to encrypt? ");
                string userInput = Console.ReadLine();
                string[] encryptedMessage = XORAddative.EncryptMessage(userInput);
                Console.WriteLine("\nPress space to decrypt Without key");
                Console.ReadKey();
                Console.WriteLine();
                string decryptedMessageWithoutKey = XORAddative.DecryptMessageWithoutKey(encryptedMessage);
                Console.WriteLine("\nPress space to decrypt");
                Console.ReadKey();
                Console.WriteLine();
                string decryptedMessage = XORAddative.DecryptMessage(encryptedMessage);
                Console.Write("Enter q and enter to close or enter to continue ");
                string finalInput = Console.ReadLine();
                if (finalInput == "q") {
                    runProgram = false;
                } else {
                    Console.WriteLine();
                }
            }
        }
    }
}

