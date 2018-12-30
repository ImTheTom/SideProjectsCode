using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOREncryption {
    class Program {
        static void Main(string[] args) {
            bool runProgram = true;
            while (runProgram) {
                Console.Write("What Message would you like to encrypt? ");
                string userInput = Console.ReadLine();
                string encryptedMessage = XOR.EncryptMessage(userInput);
                Console.WriteLine("\nPress space to decrypt without XORing");
                Console.ReadKey();
                string decryptedMessageWithoutKey = XOR.DecryptMessageWithoutKey(encryptedMessage);
                Console.WriteLine("\nPress space to decrypt");
                Console.ReadKey();
                Console.WriteLine();
                string decryptedMessage = XOR.DecryptMessage(encryptedMessage);
                Console.Write("\nEnter q and enter to close or enter to continue ");
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
