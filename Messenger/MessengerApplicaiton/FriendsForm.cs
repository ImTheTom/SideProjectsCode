using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MessengerApplicaiton {
    public partial class FriendsForm : Form {
        string clientUsername = "";
        string[] arrayOfOnlineUsers;
        public FriendsForm(string username, string[] OnlineUsers) {
            InitializeComponent();
            clientUsername = username;
            arrayOfOnlineUsers = OnlineUsers;
        }

        private void FriendsForm_Load(object sender, EventArgs e) {
            List<string> listOfFriends = MySQLLib.GetFriends(clientUsername);
            string[] arrayOfFriends = listOfFriends.ToArray();
            List<string> listOfOnlineFriends = new List<string>();
            foreach (string friend in listOfFriends) {
                if (arrayOfOnlineUsers.Contains(friend)) {
                    listOfOnlineFriends.Add(friend);
                }
            }
            string[] arrayOfOnlineFriends = listOfOnlineFriends.ToArray();
            FriendsListbox.DataSource = arrayOfOnlineFriends;
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
