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
    public partial class SearchMoviesForm : Form {
        public SQLConnection connection;
        /// <summary>
        /// Collects the SQL connection from the previous form to allow for the SQL_Lib library to be used.
        /// Aswell as sets up the comboboxs
        /// </summary>
        public SearchMoviesForm(SQLConnection lastConnection) {
            InitializeComponent();
            connection = lastConnection;
            string[] grades = new string[] { "A", "B", "C", "D" };
            string[] genres = new string[] { "Horror", "Action", "Drama", "Thriller", "Fiction" };
            for (int index = 0; index < grades.Length; index++) {
                GradeCombo.Items.Add(grades[index]);
            }
            for (int index = 0; index < genres.Length; index++) {
                GenreCombo.Items.Add(genres[index]);
            }
        }

        /// <summary>
        /// Allows to search the databse depenednt on what the user inputs and edits the grid depedent on the result
        /// </summary>
        private void SearchButton_Click(object sender, EventArgs e) {
            ResultsGrid.Rows.Clear();
            string movieName = "";
            string grade = "";
            string genre = "";
            if (MovieNameText.Text.ToString() != null) {
                movieName = MovieNameText.Text.ToString();
            }
            if (GradeCombo.SelectedIndex > -1) {
                grade = GradeCombo.SelectedItem.ToString();
            }
            if (GenreCombo.SelectedIndex > -1) {
                genre = GenreCombo.SelectedItem.ToString();
            }
            List<string>[] results = connection.SearchMovie(movieName,grade,genre);
            for(int i =0; i < results[0].Count; i++) {
                ResultsGrid.Rows.Add(results[0][i], results[1][i], results[2][i], results[3][i], results[4][i], results[5][i]);
            }
        }
    }
}
