using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SQL_Lib;

namespace MovieStoreApplication {
    public partial class ConnectionForm : Form {
        public SQLConnection connection;
        bool connectionMade;
        public ConnectionForm() {
            InitializeComponent();
        }

        /// <summary>
        /// On load set up a new object called connection with the string to the database
        /// </summary>
        private void ConnectionFormLoad(object sender, EventArgs e) {
            connection = new SQLConnection("SERVER=localhost;DATABASE=moviestore;UID=root;PASSWORD=;");
        }

        /// <summary>
        /// If the quit button is clicked close the form
        /// </summary>
        private void QuitButtonClick(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Connect button tries to open up the connection to the databse
        /// </summary>
        private void ConnectionButtoncClick(object sender, EventArgs e) {
            bool open = connection.OpenConnection();
            if (open) {
                Logging.LogIn();
                connectionMade = true;
                DisconnectButton.Enabled = true;
                HelpLabel.Text = "Connection Was Made";
                SelectionMenu selectForm = new SelectionMenu(connection);
                selectForm.ShowDialog();
            } else {
                connectionMade = false;
                HelpLabel.Text = "Connection Could Not Be Made";
            }
        }

        /// <summary>
        /// Disconect button is only available if the connection is open and if clicked closes the connection.
        /// </summary>
        private void DisconnectButton_Click(object sender, EventArgs e) {
            if (connectionMade) {
                bool closed = connection.CloseConnection();
                if (closed) {
                    HelpLabel.Text = "Connection Was Disconnected";
                    DisconnectButton.Enabled = false;
                } else {
                    HelpLabel.Text = "Connection Wasn't Disconnected";
                }
            }
        }
    }
}
