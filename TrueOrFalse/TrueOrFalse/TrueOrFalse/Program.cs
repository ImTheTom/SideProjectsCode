using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TrueOrFalse {
    class Program {
        public static Random rnd = new Random();
        static void Main(string[] args) {
            MySqlConnection connection = InitialiseConnection();
            DatabaseQuery dq = new DatabaseQuery(connection);
            bool playing = true;
            while (playing) {
                string[] quoteInfo = FetchNewQuote(dq);
                WriteOutQuoteInfo(quoteInfo);
                string input = Console.ReadLine();
                if((input=="1" && quoteInfo[1]==quoteInfo[2])||(input == "0" && quoteInfo[1] != quoteInfo[2])){
                    WriteOutCorrectStatement(quoteInfo);
                } else {
                    WriteOutIncorrectStatement(quoteInfo);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static MySqlConnection InitialiseConnection() {
            SQLConnection database = new SQLConnection("SERVER=localhost;DATABASE=quotes;UID=root;PASSWORD=test;");
            database.OpenConnection();
            return database.GetConnection();
        }

        public static string[] FetchNewQuote(DatabaseQuery dq) {
            int randomNumber = rnd.Next(2);
            if (randomNumber == 1) {
                return dq.FetchCorrectQuote();
            } else {
                return dq.FetchInCorrectQuote();
            }
        }

        public static void WriteOutQuoteInfo(string[] quoteInfo) {
            string text = String.Format("{0} by {1}", quoteInfo[0], quoteInfo[2]);
            Console.WriteLine(text);
        }

        public static void WriteOutCorrectStatement(string[] quoteInfo) {
            string text = String.Format("Correct the quote was said by {0}", quoteInfo[1]);
            Console.WriteLine(text);
        }

        public static void WriteOutIncorrectStatement(string[] quoteInfo) {
            string text = String.Format("Incorrect the quote was said by {0}", quoteInfo[1]);
            Console.WriteLine(text);
        }
    }
}
