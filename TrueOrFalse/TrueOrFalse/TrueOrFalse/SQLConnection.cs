using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TrueOrFalse {
    class SQLConnection {
        private string connectionString;
        private MySqlConnection connection;

        public SQLConnection(string connectionstring) {
            connectionString = connectionstring;
        }

        public bool OpenConnection() {
            connection = new MySqlConnection(connectionString);
            try {
                connection.Open();
                return true;
            } catch {
                return false;
            }
        }

        public bool CloseConnection() {
            try {
                connection.Close();
                return true;
            } catch {
                return false;
            }
        }

        public MySqlConnection GetConnection() {
            return this.connection;
        }
    }
}
