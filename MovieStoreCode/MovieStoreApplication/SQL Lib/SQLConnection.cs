using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

/// <summary>
/// This class will hold most of the function that will be used in the form applicaiton
/// </summary>
namespace SQL_Lib
{
    public class SQLConnection
    {
        private string connectionString;
        private MySqlConnection connection;

        /// <summary>
        /// Set the privat variable to have the connection string to what the method variable
        /// </summary>
        /// <param name="connectionstring">The connection string assoicated with the connection</param>
        public SQLConnection(string connectionstring) {
            connectionString = connectionstring;
        }

        /// <summary>
        /// Trys to open up the connection
        /// </summary>
        /// <returns>Returns true if successful or false if not</returns>
        public bool OpenConnection() {
            connection = new MySqlConnection(connectionString);
            try {
                connection.Open();
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Closes the connection
        /// </summary>
        /// <returns>Returns true if successful or false if not</returns>
        public bool CloseConnection() {
            try {
                connection.Close();
                Logging.LogOut();
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Creates a user in the database based on the values passed in the function
        /// </summary>
        /// <param name="firstName">First name in the new user</param>
        /// <param name="lastName">Last name of the new user</param>
        /// <param name="DOB">Date of birth of the new user</param>
        /// <param name="address">address of the new user</param>
        /// <param name="postcode">postcode of the new user</param>
        /// <param name="email">email of the new user</param>
        /// <returns>Returns true if successful or not</returns>
        public bool AddUser(string firstName, string lastName, string DOB, string address, string postcode, string email) {
            string query = "INSERT INTO customers(firstName, lastName, DOB, mailingAddress,email,postcode) VALUES(";
            query = query + "'" + firstName + "',";
            query = query + "'" + lastName + "',";
            query = query + "'" + DOB + "',";
            query = query + "'" + address + "',";
            query = query + "'" + email + "',";
            query = query + "'" + postcode + "');";
            try {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Logging.AddedUser(firstName, lastName, DOB, address, postcode, email);
                return true;
            } catch (Exception reason){
                Console.WriteLine(reason);
                return false;
            }
        }

        /// <summary>
        /// Adds a new movie into the database
        /// </summary>
        /// <param name="movieName">Name of the new movie</param>
        /// <param name="grade">Grade associated with the movie</param>
        /// <param name="release">Release year of the associated movie</param>
        /// <param name="runningTime">Running time of the movie</param>
        /// <param name="genre">genre of the movie</param>
        /// <returns>Returns true if the movie is successfully added</returns>
        public bool AddMovie(string movieName, string grade, string release, string runningTime, string genre) {
            string query = "INSERT INTO movie(movieName, grade, releaseYear, runningTime, genre) VALUES(";
            query = query + "'" + movieName + "',";
            query = query + "'" + grade + "',";
            query = query + "'" + release + "',";
            query = query + runningTime + ",";
            query = query + "'" + genre + "');";
            Console.WriteLine(query);
            try {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Logging.AddedMovie(movieName, grade, release, runningTime, genre);
                return true;
            } catch (Exception reason) {
                Console.WriteLine(reason);
                return false;
            }
        }

        /// <summary>
        /// This function powers the search movie form and is specific for that form
        /// </summary>
        /// <param name="movieName">Text that is searched</param>
        /// <param name="grade">Grade that is searched</param>
        /// <param name="genre">Genre that is searched</param>
        /// <returns>Returns a list of all the information recieved from the query</returns>
        public List<string>[] SearchMovie(string movieName, string grade, string genre) {
            string query = "";
            if (movieName != "" && grade == "" && genre == "") {
                query = "Select * from movie where movieName Like '%" + movieName + "%';";
            } else if (movieName != "" && grade != "" && genre == "") {
                query = "Select * from movie where movieName Like '%" + movieName + "%' AND grade = '" + grade + "';";
            } else if (movieName != "" && grade != "" && genre != "") {
                query = "Select * from movie where movieName Like '%" + movieName + "%' AND grade= '" + grade + "' AND genre='" + genre + "';";
            } else if (movieName != "" && grade == "" && genre != "") {
                query = "Select * from movie where movieName Like '%" + movieName + "%' AND genre = '" + genre + "';";
            } else if (movieName == "" && grade != "" && genre == "") {
                query = "Select * from movie where grade = '" + grade + "';";
            } else if (movieName == "" && grade != "" && genre != "") {
                query = "Select * from movie where grade = '" + grade + "' AND genre = '" + genre + "';";
            }else if(movieName == "" && grade == "" && genre != "") {
                query = "Select * from movie where genre = '" + genre + "';";
            } else {
                query = "Select * from movie";
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<string>[] Results = new List<string>[6];
            for (int i = 0; i < 6; i++) {
                Results[i] = new List<string>();
            }
            while (dataReader.Read()) {
                Results[0].Add(dataReader["movieName"] + "");
                Results[1].Add(dataReader["grade"] + "");
                Results[2].Add(dataReader["releaseYear"] + "");
                Results[3].Add(dataReader["runningTime"] + "");
                Results[4].Add(dataReader["genre"] + "");
                Results[5].Add(dataReader["isRented"] + "");
            }
            dataReader.Close();
            Logging.MovieQuery(query);
            return Results;
        }

        /// <summary>
        /// Searches through the users table in the databse
        /// </summary>
        /// <param name="name">First name that is searched</param>
        /// <returns>Returns the list of the results</returns>
        public List<string>[] SearchCustomers(string name) {
            string query = "";
            if (name != "") {
                query = "Select * from customers where firstName Like '%" + name + "%';";
            } else {
                query = "Select * from customers";
            }
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<string>[] Results = new List<string>[5];
            for (int i = 0; i < 5; i++) {
                Results[i] = new List<string>();
            }
            while (dataReader.Read()) {
                Results[0].Add(dataReader["customerID"] + "");
                Results[1].Add(dataReader["firstName"] + "");
                Results[2].Add(dataReader["lastName"] + "");
                Results[3].Add(dataReader["DOB"] + "");
                Results[4].Add(dataReader["overdue"] + "");
            }
            dataReader.Close();
            Logging.CustomerQuery(query);
            return Results;
        }

        /// <summary>
        /// Rents a movie to a specific user and associated date
        /// </summary>
        /// <param name="movieID">movie id that is being rented</param>
        /// <param name="customerID">customer id that is renting the movie</param>
        /// <param name="date">Date of the rent</param>
        /// <returns>Returns true if successful</returns>
        public bool RentMovie(string movieID, string customerID, string date) {
            string testQuery = "Select * from rentals where movieID =" + movieID + " AND dateReturned IS NULL;";
            MySqlCommand testcmd = new MySqlCommand(testQuery, connection);
            MySqlDataReader testdataReader = testcmd.ExecuteReader();
            List<string>[] TestResults = new List<string>[1];
            TestResults[0] = new List<string>();
            while (testdataReader.Read()) {
                TestResults[0].Add(testdataReader["rentalID"] + "");
            }
            testdataReader.Close();
            if (TestResults[0].Count!=0) {
                return false;
            }
            string query = "Select grade from movie where movieID = " + movieID + ";";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<string>[] Results = new List<string>[1];
            Results[0] = new List<string>();
            while (dataReader.Read()) {
                Results[0].Add(dataReader["grade"] + "");
            }
            dataReader.Close();
            DateTime rentalDate = DateTime.Parse(date);
            if (Results[0][0] == "A") {
                Console.WriteLine("Added one day");
                rentalDate = rentalDate.AddDays(1);
            }else if(Results[0][0] == "B") {
                rentalDate=rentalDate.AddDays(2);
            } else if (Results[0][0] == "C") {
                rentalDate=rentalDate.AddDays(4);
            } else if (Results[0][0] == "D") {
                rentalDate=rentalDate.AddDays(7);
            }
            string returnDate = rentalDate.ToString("yyyy-MM-dd");
            return InsertData(movieID, customerID, date, returnDate);
        }

        /// <summary>
        /// Inserts the data into the rentals table
        /// </summary>
        /// <param name="movieID">Movie that is being rented</param>
        /// <param name="customerID">customer ID renting the movie</param>
        /// <param name="date">Date that is being rented on</param>
        /// <param name="returnByDate">Date the movie needs to be returned by</param>
        /// <returns>Returns true if successful</returns>
        private bool InsertData(string movieID, string customerID, string date, string returnByDate) {
            string insertQuery = "INSERT INTO rentals(movieID, customerID, dateRented, returnByDate) VALUES(";
            insertQuery = insertQuery + movieID + ",";
            insertQuery = insertQuery + customerID + ",";
            insertQuery = insertQuery + "'" + date + "',";
            insertQuery = insertQuery + "'" + returnByDate + "');";
            Console.WriteLine(insertQuery);
            try {
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);
                insertCmd.ExecuteNonQuery();
                string alterQuery = "Update movie Set isRented = 'yes' where movieID = '" + movieID + "';";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = alterQuery;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                Logging.RentedAMovie(movieID, customerID);
                return true;
            } catch (Exception reason) {
                Console.WriteLine(reason);
                return false;
            }

        }

        /// <summary>
        /// Allows for a movie to be returned and checks if it is overdue
        /// </summary>
        /// <param name="movieID">Movie Id that is being returned</param>
        /// <param name="date">Datre of return</param>
        /// <returns>Returns true if successful</returns>
        public bool ReturnMovie(string movieID, string date) {
            try {
                string query = "Select * from rentals where movieID =" + movieID + " AND dateReturned IS NULL;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                List<string>[] Results = new List<string>[2];
                for (int i = 0; i < 2; i++) {
                    Results[i] = new List<string>();
                }
                while (dataReader.Read()) {
                    Results[0].Add(dataReader["customerID"] + "");
                    Results[1].Add(dataReader["returnByDate"] + "");
                }
                dataReader.Close();
                string customerID = Results[0][0];
                string dateReturnBy = Results[1][0];
                DateTime TodayDate = DateTime.Parse(date);
                DateTime dateReturnByDate = DateTime.Parse(dateReturnBy);
                string alterQuery = "Update rentals Set dateReturned = '" + date + "' where movieID = " + movieID + " AND dateReturned Is NULL;";
                MySqlCommand alterCmd = new MySqlCommand();
                alterCmd.CommandText = alterQuery;
                alterCmd.Connection = connection;
                alterCmd.ExecuteNonQuery();
                string alterQuery2 = "Update movie Set isRented = 'no' where movieID = " + movieID + ";";
                MySqlCommand alterCmd2 = new MySqlCommand();
                alterCmd2.CommandText = alterQuery2;
                alterCmd2.Connection = connection;
                alterCmd2.ExecuteNonQuery();
                int comparison = DateTime.Compare(TodayDate, dateReturnByDate);
                if (comparison > 0) {
                    string alterQuery3 = "Update customers Set overdue = overdue+5 where customerID = " + customerID + ";";
                    MySqlCommand alterCmd3 = new MySqlCommand();
                    alterCmd3.CommandText = alterQuery3;
                    alterCmd3.Connection = connection;
                    alterCmd3.ExecuteNonQuery();
                }
                Logging.MovieReturned(movieID);
                return true;
            } catch {
                return false;
            }
        }
    }
}
