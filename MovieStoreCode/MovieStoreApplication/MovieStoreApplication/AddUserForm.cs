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
    public partial class AddUserForm : Form {
        public SQLConnection connection;
        /// <summary>
        /// Collects the SQL connection from the previous form to allow for the SQL_Lib library to be used.
        /// </summary>
        public AddUserForm(SQLConnection lastConnection) {
            InitializeComponent();
            connection = lastConnection;
        }

        /// <summary>
        /// Creates a user in the customer table and updates the help label dependent on result
        /// </summary>
        private void CreateUserButton_Click(object sender, EventArgs e) {
            string firstName = FirstNameText.Text.ToString();
            string lastName = LastNameText.Text.ToString();
            string DOB = DateOfBirthText.Text.ToString();
            string address = AddressText.Text.ToString();
            string postcode = PostCodeText.Text.ToString();
            string email = emailText.Text.ToString();
            bool success = connection.AddUser(firstName, lastName, DOB, address, postcode, email);
            if (success) {
                StatusLabel.Text = "Successful user add";
            } else {
                StatusLabel.Text = "Unsuccessful user add";
            }
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
