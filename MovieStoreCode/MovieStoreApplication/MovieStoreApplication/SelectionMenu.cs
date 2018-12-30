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
    /// <summary>
    /// This form allows for a selection on what to do
    /// </summary>
    public partial class SelectionMenu : Form {
        public SQLConnection connection;

        /// <summary>
        /// Collects the SQL connection from the previous form to allow for the SQL_Lib library to be used.
        /// </summary>
        public SelectionMenu(SQLConnection lastConnection) {
            InitializeComponent();
            connection = lastConnection;
        }

        private void AddUserButton_Click(object sender, EventArgs e) {
            AddUserForm addUserForm = new AddUserForm(connection);
            addUserForm.ShowDialog();
        }

        private void AddMovieButton_Click(object sender, EventArgs e) {
            AddMovieForm addMovieForm = new AddMovieForm(connection);
            addMovieForm.ShowDialog();
        }

        private void SearchMoviesButton_Click(object sender, EventArgs e) {
            SearchMoviesForm searchMovieForm = new SearchMoviesForm(connection);
            searchMovieForm.ShowDialog();
        }

        private void SearchUsersButton_Click(object sender, EventArgs e) {
            SearchUserForm searchUserForm = new SearchUserForm(connection);
            searchUserForm.ShowDialog();
        }

        private void RentalsButton_Click(object sender, EventArgs e) {
            RentalForm rentalForm = new RentalForm(connection);
            rentalForm.ShowDialog();
        }

        private void ReturnButton_Click(object sender, EventArgs e) {
            ReturnsForm returnsForm = new ReturnsForm(connection);
            returnsForm.ShowDialog();
        }
    }
}
