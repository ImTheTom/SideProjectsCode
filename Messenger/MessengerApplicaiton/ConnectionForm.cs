using System;
using System.Windows.Forms;

namespace MessengerApplicaiton {
    public partial class ConnectionForm : Form {
        public ConnectionForm() {
            InitializeComponent();
            PasswordTextbox.UseSystemPasswordChar = true;
            try {
                MySQLLib.ConnectToDatabase();
            } catch (Exception reason) {
                StatusLabel.Text = "No database found";
                Console.WriteLine(reason);
            }
        }

        private void LogInButton_Click(object sender, EventArgs e) {
            if (UsernameTextbox.Text != "" && PasswordTextbox.Text != "") {
                StatusLabel.Text = "Logging In";
                string clientUsername = UsernameTextbox.Text;
                string clientPassword = PasswordTextbox.Text;
                bool result = MySQLLib.CheckUserLogInDetails(clientUsername, clientPassword);
                if (result) {
                    IP clientsIp = new IP();
                    string clientIPAdress = clientsIp.GetClientIP();
                    Console.WriteLine(clientIPAdress);
                    MySQLLib.UpdateIP(clientUsername, clientIPAdress);
                    StatusLabel.Text = "Logged In";
                    ChatInterface chatInterface = new ChatInterface(clientUsername);
                    chatInterface.Show();
                } else {
                    StatusLabel.Text = "Wrong username or password";
                }
            } else {
                StatusLabel.Text = "Fill in textboxs";
            }
        }

        private void CreateButton_Click(object sender, EventArgs e) {
            if (UsernameTextbox.Text != "" && PasswordTextbox.Text != "") {
                string clientPassword = PasswordTextbox.Text;
                int clientSalt = Password.CreateRandomSalt();
                Password password = new Password(clientPassword, clientSalt);
                string hashedClientPassword = password.ComputeSaltedHash();
                bool result = MySQLLib.AddUser(UsernameTextbox.Text, clientSalt, hashedClientPassword);
                if (result) {
                    StatusLabel.Text = "User Created";
                } else {
                    StatusLabel.Text = "Same username as\n someone else";
                }
            } else {
                StatusLabel.Text = "Fill in textboxs";
            }
        }
    }
}
