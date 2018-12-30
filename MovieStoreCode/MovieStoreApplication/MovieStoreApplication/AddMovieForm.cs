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
    public partial class AddMovieForm : Form {
        public SQLConnection connection;
        public AddMovieForm(SQLConnection lastConnection) {
            InitializeComponent();
            connection = lastConnection;
            string[] grades = new string[] { "A","B","C","D" };
            string[] genres = new string[] { "Horror","Action","Drama","Thriller","Fiction" };
            for(int index = 0; index < grades.Length; index++) {
                GradeCombo.Items.Add(grades[index]);
            }
            for (int index = 0; index < genres.Length; index++) {
                GenreCombo.Items.Add(genres[index]);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void CreateMovieButton_Click(object sender, EventArgs e) {
            string movieName = MovieNameText.Text.ToString();
            string grade = GradeCombo.SelectedItem.ToString();
            string release = ReleaseText.Text.ToString();
            string runningTime = RunningTimeText.Text.ToString();
            string genre = GenreCombo.SelectedItem.ToString();
            bool success = connection.AddMovie(movieName, grade, release, runningTime, genre);
            if (success) {
                HelpLabel.Text = "Successful movie add";
            } else {
                HelpLabel.Text = "Unsuccessful movie add";
            }
        }
    }
}
