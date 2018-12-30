using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TrueOrFalse {
    class DatabaseQuery {
        private MySqlConnection connection;
        Random rand;

        public DatabaseQuery(MySqlConnection connection) {
            this.connection = connection;
            rand = new Random();
        }

        public string[] FetchCorrectQuote() {
            string[] result = new string[4];
            int databaseSize = GetSize();
            int randomInt = rand.Next(databaseSize)+1;
            string query = "Select * from quotes where idquotes=" + randomInt.ToString()+";";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            result[0] = dataReader["quote"] + "";
            result[1] = dataReader["author"] + "";
            result[2] = dataReader["author"] + "";
            result[3] = dataReader["type"] + "";
            dataReader.Close();
            return result;
        }

        public string[] FetchInCorrectQuote() {
            string[] result = FetchCorrectQuote();
            string query = "Select * from quotes where(type='" + result[3] + "' AND NOT author='"+result[1]+"');";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            List<string> results = new List<string>();
            while (dataReader.Read()) {
                results.Add(dataReader["author"] + "");
            }
            dataReader.Close();
            int randomInt = rand.Next(results.Count);
            result[2] = results[randomInt];
            return result;
        }

        private int GetSize() {
            MySqlCommand query = new MySqlCommand("SELECT * FROM quotes ORDER BY idquotes DESC", this.connection);
            return int.Parse(query.ExecuteScalar().ToString());
        }
    }
}
