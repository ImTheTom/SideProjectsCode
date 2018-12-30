using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SQL_Lib {
    /// <summary>
    /// Static class for the logging functions. Really basic stuff.
    /// </summary>
    public static class Logging {
        public static void LogIn() {
            DateTime localDate = DateTime.Now;
            string dateAndTime = localDate.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Users\Tom\Desktop\My Folder\Code\MovieStore\MovieStoreApplication\Logs.txt", true)) {
                file.WriteLine(dateAndTime+" connection was made");
            }
        }

        public static void LogOut() {
            DateTime localDate = DateTime.Now;
            string dateAndTime = localDate.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Users\Tom\Desktop\My Folder\Code\MovieStore\MovieStoreApplication\Logs.txt", true)) {
                file.WriteLine(dateAndTime + " connection was disconnected");
            }
        }

        public static void AddedUser(string firstName, string lastName, string DOB, string address, string postcode, string email) {
            DateTime localDate = DateTime.Now;
            string dateAndTime = localDate.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Users\Tom\Desktop\My Folder\Code\MovieStore\MovieStoreApplication\Logs.txt", true)) {
                file.WriteLine(dateAndTime + " Created user with the following credentials: "+firstName+lastName+ DOB+address+postcode+email);
            }
        }

        public static void AddedMovie(string movieName, string grade, string release, string runningTime, string genre) {
            DateTime localDate = DateTime.Now;
            string dateAndTime = localDate.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Users\Tom\Desktop\My Folder\Code\MovieStore\MovieStoreApplication\Logs.txt", true)) {
                file.WriteLine(dateAndTime + " Created a new movie with the following credentials: " + movieName + grade + release + runningTime + genre);
            }
        }

        public static void MovieQuery(string query) {
            DateTime localDate = DateTime.Now;
            string dateAndTime = localDate.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Users\Tom\Desktop\My Folder\Code\MovieStore\MovieStoreApplication\Logs.txt", true)) {
                file.WriteLine(dateAndTime + " A query on the movies table was made: " +query);
            }
        }

        public static void CustomerQuery(string query) {
            DateTime localDate = DateTime.Now;
            string dateAndTime = localDate.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Users\Tom\Desktop\My Folder\Code\MovieStore\MovieStoreApplication\Logs.txt", true)) {
                file.WriteLine(dateAndTime + " A query on the customer table was made: " + query);
            }
        }

        public static void RentedAMovie(string movieID, string customerID) {
            DateTime localDate = DateTime.Now;
            string dateAndTime = localDate.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Users\Tom\Desktop\My Folder\Code\MovieStore\MovieStoreApplication\Logs.txt", true)) {
                file.WriteLine(dateAndTime + " The movie " + movieID +" was rented by the customer ID of "+customerID);
            }
        }

        public static void MovieReturned(string movieID) {
            DateTime localDate = DateTime.Now;
            string dateAndTime = localDate.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Users\Tom\Desktop\My Folder\Code\MovieStore\MovieStoreApplication\Logs.txt", true)) {
                file.WriteLine(dateAndTime + " The movie " + movieID + " was returned");
            }
        }

    }
}
