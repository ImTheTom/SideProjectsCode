namespace MovieStoreApplication {
    partial class SelectionMenu {
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
            this.MainLabel = new System.Windows.Forms.Label();
            this.RentalsButton = new System.Windows.Forms.Button();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.SearchMoviesButton = new System.Windows.Forms.Button();
            this.SearchUsersButton = new System.Windows.Forms.Button();
            this.AddUserButton = new System.Windows.Forms.Button();
            this.AddMovieButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainLabel
            // 
            this.MainLabel.AutoSize = true;
            this.MainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainLabel.Location = new System.Drawing.Point(48, 9);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(119, 20);
            this.MainLabel.TabIndex = 0;
            this.MainLabel.Text = "Selection Menu";
            // 
            // RentalsButton
            // 
            this.RentalsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RentalsButton.Location = new System.Drawing.Point(52, 32);
            this.RentalsButton.Name = "RentalsButton";
            this.RentalsButton.Size = new System.Drawing.Size(115, 51);
            this.RentalsButton.TabIndex = 1;
            this.RentalsButton.Text = "Rentals";
            this.RentalsButton.UseVisualStyleBackColor = true;
            this.RentalsButton.Click += new System.EventHandler(this.RentalsButton_Click);
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReturnButton.Location = new System.Drawing.Point(52, 89);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(115, 51);
            this.ReturnButton.TabIndex = 2;
            this.ReturnButton.Text = "Returns";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // SearchMoviesButton
            // 
            this.SearchMoviesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchMoviesButton.Location = new System.Drawing.Point(52, 146);
            this.SearchMoviesButton.Name = "SearchMoviesButton";
            this.SearchMoviesButton.Size = new System.Drawing.Size(115, 51);
            this.SearchMoviesButton.TabIndex = 3;
            this.SearchMoviesButton.Text = "Search Movies";
            this.SearchMoviesButton.UseVisualStyleBackColor = true;
            this.SearchMoviesButton.Click += new System.EventHandler(this.SearchMoviesButton_Click);
            // 
            // SearchUsersButton
            // 
            this.SearchUsersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchUsersButton.Location = new System.Drawing.Point(52, 203);
            this.SearchUsersButton.Name = "SearchUsersButton";
            this.SearchUsersButton.Size = new System.Drawing.Size(115, 51);
            this.SearchUsersButton.TabIndex = 4;
            this.SearchUsersButton.Text = "Search Users";
            this.SearchUsersButton.UseVisualStyleBackColor = true;
            this.SearchUsersButton.Click += new System.EventHandler(this.SearchUsersButton_Click);
            // 
            // AddUserButton
            // 
            this.AddUserButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddUserButton.Location = new System.Drawing.Point(52, 260);
            this.AddUserButton.Name = "AddUserButton";
            this.AddUserButton.Size = new System.Drawing.Size(115, 51);
            this.AddUserButton.TabIndex = 5;
            this.AddUserButton.Text = "Add Users";
            this.AddUserButton.UseVisualStyleBackColor = true;
            this.AddUserButton.Click += new System.EventHandler(this.AddUserButton_Click);
            // 
            // AddMovieButton
            // 
            this.AddMovieButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddMovieButton.Location = new System.Drawing.Point(52, 317);
            this.AddMovieButton.Name = "AddMovieButton";
            this.AddMovieButton.Size = new System.Drawing.Size(115, 51);
            this.AddMovieButton.TabIndex = 6;
            this.AddMovieButton.Text = "Add Movie";
            this.AddMovieButton.UseVisualStyleBackColor = true;
            this.AddMovieButton.Click += new System.EventHandler(this.AddMovieButton_Click);
            // 
            // SelectionMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 376);
            this.Controls.Add(this.AddMovieButton);
            this.Controls.Add(this.AddUserButton);
            this.Controls.Add(this.SearchUsersButton);
            this.Controls.Add(this.SearchMoviesButton);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.RentalsButton);
            this.Controls.Add(this.MainLabel);
            this.MaximizeBox = false;
            this.Name = "SelectionMenu";
            this.Text = "SelectionMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MainLabel;
        private System.Windows.Forms.Button RentalsButton;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button SearchMoviesButton;
        private System.Windows.Forms.Button SearchUsersButton;
        private System.Windows.Forms.Button AddUserButton;
        private System.Windows.Forms.Button AddMovieButton;
    }
}