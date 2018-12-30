using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace MessengerApplicaiton {
    public partial class ChatInterface : Form {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string messageFromServerWithoutUsers = null;
        string stringOfOnlineUsers = null;
        string[] arrayOfOnlineUsers;
        string clientUsername = "";

        public ChatInterface(string username) {
            InitializeComponent();
            this.ActiveControl = MessageTextbox;
            clientUsername = username;
            ConnectToServer(clientUsername);
            this.FormClosed += new FormClosedEventHandler(ChatInterfaceFormCloseEvent);
        }

        private void ConnectToServer(string username) {
            clientSocket.Connect("127.0.0.1", 8888);
            serverStream = clientSocket.GetStream();
            byte[] sendDataOut = System.Text.Encoding.ASCII.GetBytes(username + "$");
            serverStream.Write(sendDataOut, 0, sendDataOut.Length);
            serverStream.Flush();
            Thread retrieveMessagesFromServerThread = new Thread(GetMessageFromServer);
            retrieveMessagesFromServerThread.Start();
        }

        private void GetMessageFromServer() {
            while (true) {
                serverStream = clientSocket.GetStream();
                int bufferSize = 0;
                byte[] inStreamBytes = new byte[65536];
                bufferSize = clientSocket.ReceiveBufferSize;
                serverStream.Read(inStreamBytes, 0, bufferSize);
                string recievedDataInAscii = System.Text.Encoding.ASCII.GetString(inStreamBytes);
                int indexOfUsers = recievedDataInAscii.IndexOf("$~");
                messageFromServerWithoutUsers = recievedDataInAscii.Substring(0, indexOfUsers);
                stringOfOnlineUsers = recievedDataInAscii.Substring(indexOfUsers + 3, 500);
                CreateNewMessage();
            }
        }

        private void CreateNewMessage() {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(CreateNewMessage));
            else
                MainChatBox.Text = MainChatBox.Text + Environment.NewLine + " >> " + messageFromServerWithoutUsers;
            UpdateUsers();
        }

        private void UpdateUsers() {
            try {
                arrayOfOnlineUsers = stringOfOnlineUsers.Split(' ');
                UsersListbox.DataSource = arrayOfOnlineUsers;
            } catch (Exception reason) {
                Console.WriteLine(reason);
            }
        }

        void ChatInterfaceFormCloseEvent(object sender, FormClosedEventArgs e) {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(".r" + clientUsername + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        private void SendButton_Click(object sender, EventArgs e) {
            if (MessageTextbox.Text.Length > 0) { // See if the messagebox isn't empty
                if(MessageTextbox.Text.Length < 2) { // See if there is enough in the textbox for a command
                    byte[] sendDataOut = System.Text.Encoding.ASCII.GetBytes(MessageTextbox.Text + "$");
                    serverStream.Write(sendDataOut, 0, sendDataOut.Length);
                    serverStream.Flush();
                } else if (MessageTextbox.Text.Substring(0, 2) != ".r") { // Check if the first two characters are a command to remove a peron
                    byte[] sendDataOut = System.Text.Encoding.ASCII.GetBytes(MessageTextbox.Text + "$");
                    serverStream.Write(sendDataOut, 0, sendDataOut.Length);
                    serverStream.Flush();
                }
                MessageTextbox.Clear();
                MessageTextbox.Focus();
            }
        }

        private void MessageTextbox_KeyPress(object sender, KeyPressEventArgs keyPress) {
            if (keyPress.KeyChar == (char)Keys.Enter) {
                SendButton_Click(sender, keyPress);
            }
        }

        private void AddFriendButton_Click(object sender, EventArgs e) {
            string friendToAdd = UsersListbox.GetItemText(UsersListbox.SelectedValue.ToString());
            MySQLLib.AddFriend(clientUsername, friendToAdd);
        }

        private void FriendsButton_Click(object sender, EventArgs e) {
            FriendsForm friendsForm = new FriendsForm(clientUsername, arrayOfOnlineUsers);
            friendsForm.ShowDialog();
        }
    }
}
