namespace MovieStoreApplication {
    partial class SearchMoviesForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ResultsGrid = new System.Windows.Forms.DataGridView();
            this.MovieName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReleaseYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunningTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Genre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MovieNameText = new System.Windows.Forms.TextBox();
            this.MovieNameLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.GradeLabel = new System.Windows.Forms.Label();
            this.GradeCombo = new System.Windows.Forms.ComboBox();
            this.GenreCombo = new System.Windows.Forms.ComboBox();
            this.GenreLabel = new System.Windows.Forms.Label();
            this.IsRented = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultsGrid
            // 
            this.ResultsGrid.AllowUserToAddRows = false;
            this.ResultsGrid.AllowUserToDeleteRows = false;
            this.ResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MovieName,
            this.Grade,
            this.ReleaseYear,
            this.RunningTime,
            this.Genre,
            this.IsRented});
            this.ResultsGrid.Location = new System.Drawing.Point(12, 111);
            this.ResultsGrid.Name = "ResultsGrid";
            this.ResultsGrid.ReadOnly = true;
            this.ResultsGrid.Size = new System.Drawing.Size(643, 336);
            this.ResultsGrid.TabIndex = 0;
            // 
            // MovieName
            // 
            this.MovieName.HeaderText = "Movie Name";
            this.MovieName.Name = "MovieName";
            this.MovieName.ReadOnly = true;
            // 
            // Grade
            // 
            this.Grade.HeaderText = "Grade";
            this.Grade.Name = "Grade";
            this.Grade.ReadOnly = true;
            // 
            // ReleaseYear
            // 
            this.ReleaseYear.HeaderText = "Release Year";
            this.ReleaseYear.Name = "ReleaseYear";
            this.ReleaseYear.ReadOnly = true;
            // 
            // RunningTime
            // 
            this.RunningTime.HeaderText = "Running Time";
            this.RunningTime.Name = "RunningTime";
            this.RunningTime.ReadOnly = true;
            // 
            // Genre
            // 
            this.Genre.HeaderText = "Genre";
            this.Genre.Name = "Genre";
            this.Genre.ReadOnly = true;
            // 
            // MovieNameText
            // 
            this.MovieNameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovieNameText.Location = new System.Drawing.Point(96, 12);
            this.MovieNameText.Name = "MovieNameText";
            this.MovieNameText.Size = new System.Drawing.Size(367, 26);
            this.MovieNameText.TabIndex = 1;
            // 
            // MovieNameLabel
            // 
            this.MovieNameLabel.AutoSize = true;
            this.MovieNameLabel.Location = new System.Drawing.Point(12, 20);
            this.MovieNameLabel.Name = "MovieNameLabel";
            this.MovieNameLabel.Size = new System.Drawing.Size(67, 13);
            this.MovieNameLabel.TabIndex = 2;
            this.MovieNameLabel.Text = "Movie Name";
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(483, 10);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(84, 28);
            this.SearchButton.TabIndex = 3;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // GradeLabel
            // 
            this.GradeLabel.AutoSize = true;
            this.GradeLabel.Location = new System.Drawing.Point(12, 63);
            this.GradeLabel.Name = "GradeLabel";
            this.GradeLabel.Size = new System.Drawing.Size(36, 13);
            this.GradeLabel.TabIndex = 6;
            this.GradeLabel.Text = "Grade";
            // 
            // GradeCombo
            // 
            this.GradeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GradeCombo.FormattingEnabled = true;
            this.GradeCombo.Location = new System.Drawing.Point(96, 60);
            this.GradeCombo.Name = "GradeCombo";
            this.GradeCombo.Size = new System.Drawing.Size(128, 26);
            this.GradeCombo.TabIndex = 7;
            // 
            // GenreCombo
            // 
            this.GenreCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenreCombo.FormattingEnabled = true;
            this.GenreCombo.Location = new System.Drawing.Point(335, 60);
            this.GenreCombo.Name = "GenreCombo";
            this.GenreCombo.Size = new System.Drawing.Size(128, 26);
            this.GenreCombo.TabIndex = 9;
            // 
            // GenreLabel
            // 
            this.GenreLabel.AutoSize = true;
            this.GenreLabel.Location = new System.Drawing.Point(251, 63);
            this.GenreLabel.Name = "GenreLabel";
            this.GenreLabel.Size = new System.Drawing.Size(36, 13);
            this.GenreLabel.TabIndex = 8;
            this.GenreLabel.Text = "Genre";
            // 
            // IsRented
            // 
            this.IsRented.HeaderText = "Is Rented";
            this.IsRented.Name = "IsRented";
            this.IsRented.ReadOnly = true;
            // 
            // SearchMoviesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 457);
            this.Controls.Add(this.GenreCombo);
            this.Controls.Add(this.GenreLabel);
            this.Controls.Add(this.GradeCombo);
            this.Controls.Add(this.GradeLabel);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.MovieNameLabel);
            this.Controls.Add(this.MovieNameText);
            this.Controls.Add(this.ResultsGrid);
            this.Name = "SearchMoviesForm";
            this.Text = "SearchMoviesForm";
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ResultsGrid;
        private System.Windows.Forms.TextBox MovieNameText;
        private System.Windows.Forms.Label MovieNameLabel;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label GradeLabel;
        private System.Windows.Forms.ComboBox GradeCombo;
        private System.Windows.Forms.ComboBox GenreCombo;
        private System.Windows.Forms.Label GenreLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grade;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReleaseYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunningTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Genre;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsRented;
    }
}