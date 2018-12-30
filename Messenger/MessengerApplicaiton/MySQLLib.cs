using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MessengerApplicaiton {
    class MySQLLib {
        private static MySqlConnection connection;

        public static bool ConnectToDatabase() {
            string server = "localhost";
            string database = "messenger";
            string userId = "root";
            string password = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + userId + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            try {
                connection.Open();
                return true;
            } catch {
                return false;
            }
        }

        public static bool AddUser(string username, int salt, string password) {
            string query = "INSERT INTO users(username, salt, password) VALUES(";
            query = query + "'" + username + "',";
            query = query + salt + ",";
            query = query + "'" + password + "');";
            Console.WriteLine(query);
            try {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return true;
            } catch (Exception reason) {
                Console.WriteLine(reason);
                return false;
            }
        }

        public static bool CheckUserLogInDetails(string username, string password) {
            int salt = GetSalt(username);
            Console.WriteLine(salt);
            Password pwd = new Password(password, salt);
            string computedPassword = pwd.ComputeSaltedHash();
            Console.WriteLine(computedPassword);
            string databasePassword = GetPassword(username);
            Console.WriteLine(databasePassword);
            if (computedPassword == databasePassword) {
                return true;
            } else {
                return false;
            }
        }

        private static int GetSalt(string username) {
            int salt = 0;
            try {
                string query = "select salt from users where username = '" + username + "';";
                Console.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read()) {
                    salt = (int)dataReader.GetValue(0);
                }
                dataReader.Close();
            } catch (Exception reason) {
                Console.WriteLine(reason);
            }
            return salt;
        }

        private static string GetPassword(string username) {
            string password = "";
            try {
                string query = "select password from users where username = '" + username + "';";
                Console.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read()) {
                    password = (string)dataReader.GetValue(0);
                }
                dataReader.Close();
            } catch (Exception reason) {
                Console.WriteLine(reason);
            }
            return password;
        }

        public static bool UpdateIP(string username, string ipAddress) {
            string query = "Update users set IP = '" + ipAddress + "' where username = '" + username + "';";
            Console.WriteLine(query);
            try {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return true;
            } catch (Exception reason) {
                Console.WriteLine(reason);
                return false;
            }
        }

        public static string GetIP(string username) {
            string ip = "";
            try {
                string query = "select IP from users where username = '" + username + "';";
                Console.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read()) {
                    ip = (string)dataReader.GetValue(0);
                }
                dataReader.Close();
            } catch (Exception reason) {
                Console.WriteLine(reason);
            }
            return ip;
        }

        public static bool AddFriend(string username, string friendUsername) {
            if (!CheckIfAlreadyFriend(username, friendUsername)) {
                string query = "Insert into friends(username, friend) Values(";
                query += "'" + username + "',";
                query += "'" + friendUsername + "');";
                Console.WriteLine(query);
                try {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    return true;
                } catch (Exception reason) {
                    Console.WriteLine(reason);
                    return false;
                }
            }
            return false;
        }

        public static bool CheckIfAlreadyFriend(string username, string friendUsername) {
            string query = "select * from friends where username = '" + username + "' AND friend = '" + friendUsername + "';";
            Console.WriteLine(query);
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read()) {
                Console.WriteLine("Already friends");
                dataReader.Close();
                return true;
            } else {
                dataReader.Close();
                return false;
            }
        }

        public static List<string> GetFriends(string username) {
            string query = "Select friend from friends where username = '" + username + "';";
            Console.WriteLine(query);
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<string> Results = new List<string>();
            while (dataReader.Read()) {
                Results.Add(dataReader["friend"] + "");
            }
            dataReader.Close();
            return Results;
        }
    }
}
