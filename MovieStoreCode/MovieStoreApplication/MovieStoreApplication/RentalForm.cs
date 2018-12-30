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
    public partial class RentalForm : Form {
        public SQLConnection connection;

        /// <summary>
        /// Collects the SQL connection from the previous form to allow for the SQL_Lib library to be used.
        /// </summary>
        public RentalForm(SQLConnection lastConnection) {
            InitializeComponent();
            connection = lastConnection;
            DateTime today = DateTime.Today;
            string date = DateManipulation.ChangeDateToString(today);
            DateText.Text = date.ToString();
        }

        /// <summary>
        /// Allows for the movie to be rented and updates a label dependent on the result
        /// </summary>
        private void RentButton_Click(object sender, EventArgs e) {
            if(MovieIDText.Text != "") {
                string movie = MovieIDText.Text.ToString();
                string customer = CustomerIDText.Text.ToString();
                string date = DateText.Text.ToString();
                bool success = connection.RentMovie(movie, customer, date);
                if (success) {
                    HelpLabel.Text = "Movie Successfully rented";
                } else {
                    HelpLabel.Text = "Movie wasn't Successfully rented";
                }
            }
            if (MovieIDText2.Text != "") {
                string movie = MovieIDText2.Text.ToString();
                string customer = CustomerIDText.Text.ToString();
                string date = DateText.Text.ToString();
                bool success = connection.RentMovie(movie, customer, date);
                if (success) {
                    HelpLabel.Text = "Movie Successfully rented";
                } else {
                    HelpLabel.Text = "Movie wasn't Successfully rented";
                }
            }
            if (MovieIDText3.Text != "") {
                string movie = MovieIDText3.Text.ToString();
                string customer = CustomerIDText.Text.ToString();
                string date = DateText.Text.ToString();
                bool success = connection.RentMovie(movie, customer, date);
                if (success) {
                    HelpLabel.Text = "Movie Successfully rented";
                } else {
                    HelpLabel.Text = "Movie wasn't Successfully rented";
                }
            }
            if (MovieIDText4.Text != "") {
                string movie = MovieIDText4.Text.ToString();
                string customer = CustomerIDText.Text.ToString();
                string date = DateText.Text.ToString();
                bool success = connection.RentMovie(movie, customer, date);
                if (success) {
                    HelpLabel.Text = "Movie Successfully rented";
                } else {
                    HelpLabel.Text = "Movie wasn't Successfully rented";
                }
            }
            if (MovieIDText5.Text != "") {
                string movie = MovieIDText5.Text.ToString();
                string customer = CustomerIDText.Text.ToString();
                string date = DateText.Text.ToString();
                bool success = connection.RentMovie(movie, customer, date);
                if (success) {
                    HelpLabel.Text = "Movie Successfully rented";
                } else {
                    HelpLabel.Text = "Movie wasn't Successfully rented";
                }
            }
        }
    }
}
