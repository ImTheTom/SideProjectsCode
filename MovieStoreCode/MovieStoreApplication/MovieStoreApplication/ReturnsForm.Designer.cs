namespace MovieStoreApplication {
    partial class ReturnsForm {
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
            this.MovieIDLabel = new System.Windows.Forms.Label();
            this.MovieIDText = new System.Windows.Forms.TextBox();
            this.DateLabel = new System.Windows.Forms.Label();
            this.DateText = new System.Windows.Forms.TextBox();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.HelpLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MovieIDLabel
            // 
            this.MovieIDLabel.AutoSize = true;
            this.MovieIDLabel.Location = new System.Drawing.Point(22, 22);
            this.MovieIDLabel.Name = "MovieIDLabel";
            this.MovieIDLabel.Size = new System.Drawing.Size(50, 13);
            this.MovieIDLabel.TabIndex = 20;
            this.MovieIDLabel.Text = "Movie ID";
            // 
            // MovieIDText
            // 
            this.MovieIDText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MovieIDText.Location = new System.Drawing.Point(106, 14);
            this.MovieIDText.Name = "MovieIDText";
            this.MovieIDText.Size = new System.Drawing.Size(113, 26);
            this.MovieIDText.TabIndex = 19;
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Location = new System.Drawing.Point(244, 22);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(70, 13);
            this.DateLabel.TabIndex = 18;
            this.DateLabel.Text = "Today\'s Date";
            // 
            // DateText
            // 
            this.DateText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateText.Location = new System.Drawing.Point(328, 14);
            this.DateText.Name = "DateText";
            this.DateText.Size = new System.Drawing.Size(113, 26);
            this.DateText.TabIndex = 17;
            // 
            // ReturnButton
            // 
            this.ReturnButton.BackColor = System.Drawing.SystemColors.Control;
            this.ReturnButton.Location = new System.Drawing.Point(185, 68);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(84, 28);
            this.ReturnButton.TabIndex = 21;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = false;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // HelpLabel
            // 
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.Location = new System.Drawing.Point(279, 76);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new System.Drawing.Size(0, 13);
            this.HelpLabel.TabIndex = 22;
            // 
            // ReturnsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 130);
            this.Controls.Add(this.HelpLabel);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.MovieIDLabel);
            this.Controls.Add(this.MovieIDText);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.DateText);
            this.Name = "ReturnsForm";
            this.Text = "ReturnsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MovieIDLabel;
        private System.Windows.Forms.TextBox MovieIDText;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.TextBox DateText;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Label HelpLabel;
    }
}