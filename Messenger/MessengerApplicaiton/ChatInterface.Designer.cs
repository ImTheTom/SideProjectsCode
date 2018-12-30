namespace MessengerApplicaiton {
    partial class ChatInterface {
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
            this.MainChatBox = new System.Windows.Forms.RichTextBox();
            this.MessageTextbox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.UsersListbox = new System.Windows.Forms.ListBox();
            this.AddFriendButton = new System.Windows.Forms.Button();
            this.FriendsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainChatBox
            // 
            this.MainChatBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainChatBox.Location = new System.Drawing.Point(12, 12);
            this.MainChatBox.Name = "MainChatBox";
            this.MainChatBox.ReadOnly = true;
            this.MainChatBox.Size = new System.Drawing.Size(553, 368);
            this.MainChatBox.TabIndex = 0;
            this.MainChatBox.Text = "";
            // 
            // MessageTextbox
            // 
            this.MessageTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageTextbox.Location = new System.Drawing.Point(12, 386);
            this.MessageTextbox.Name = "MessageTextbox";
            this.MessageTextbox.Size = new System.Drawing.Size(553, 26);
            this.MessageTextbox.TabIndex = 2;
            this.MessageTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MessageTextbox_KeyPress);
            // 
            // SendButton
            // 
            this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendButton.Location = new System.Drawing.Point(571, 386);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(120, 26);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // UsersListbox
            // 
            this.UsersListbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsersListbox.FormattingEnabled = true;
            this.UsersListbox.ItemHeight = 20;
            this.UsersListbox.Location = new System.Drawing.Point(571, 12);
            this.UsersListbox.Name = "UsersListbox";
            this.UsersListbox.Size = new System.Drawing.Size(120, 304);
            this.UsersListbox.TabIndex = 6;
            // 
            // AddFriendButton
            // 
            this.AddFriendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddFriendButton.Location = new System.Drawing.Point(571, 322);
            this.AddFriendButton.Name = "AddFriendButton";
            this.AddFriendButton.Size = new System.Drawing.Size(120, 26);
            this.AddFriendButton.TabIndex = 7;
            this.AddFriendButton.Text = "Add";
            this.AddFriendButton.UseVisualStyleBackColor = true;
            this.AddFriendButton.Click += new System.EventHandler(this.AddFriendButton_Click);
            // 
            // FriendsButton
            // 
            this.FriendsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FriendsButton.Location = new System.Drawing.Point(571, 354);
            this.FriendsButton.Name = "FriendsButton";
            this.FriendsButton.Size = new System.Drawing.Size(120, 26);
            this.FriendsButton.TabIndex = 8;
            this.FriendsButton.Text = "Friends";
            this.FriendsButton.UseVisualStyleBackColor = true;
            this.FriendsButton.Click += new System.EventHandler(this.FriendsButton_Click);
            // 
            // ChatInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 421);
            this.Controls.Add(this.FriendsButton);
            this.Controls.Add(this.AddFriendButton);
            this.Controls.Add(this.UsersListbox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.MessageTextbox);
            this.Controls.Add(this.MainChatBox);
            this.Name = "ChatInterface";
            this.Text = "ChatInterface";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox MainChatBox;
        private System.Windows.Forms.TextBox MessageTextbox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.ListBox UsersListbox;
        private System.Windows.Forms.Button AddFriendButton;
        private System.Windows.Forms.Button FriendsButton;
    }
}