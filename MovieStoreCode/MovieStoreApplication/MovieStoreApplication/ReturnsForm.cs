using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQL_Lib;

namespace MovieStoreApplication {
    public partial class ReturnsForm : Form {
        public SQLConnection connection;

        /// <summary>
        /// Collects the SQL connection from the previous form to allow for the SQL_Lib library to be used.
        /// </summary>
        public ReturnsForm(SQLConnection lastConnection) {
            InitializeComponent();
            connection = lastConnection;
            DateTime today = DateTime.Today;
            string date = DateManipulation.ChangeDateToString(today);
            DateText.Text = date.ToString();
        }

        /// <summary>
        /// Collects the information from the return form and uses the non static object connection method to
        /// send it to the database and update the help label dependent on the return
        /// </summary>
        private void ReturnButton_Click(object sender, EventArgs e) {
            string movie = MovieIDText.Text.ToString();
            string date = DateText.Text.ToString();
            bool result = connection.ReturnMovie(movie, date);
            if (result) {
                HelpLabel.Text = "Success in returning movie";
            } else {
                HelpLabel.Text = "Not successful in returning movie";
            }
        }
    }
}
