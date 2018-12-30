using System;
using System.Text;
using System.Net.Sockets;
using System.Collections;

namespace Server {
    class Program {
        public static Hashtable clientsList = new Hashtable();

        static void Main(string[] args) {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;
            serverSocket.Start();
            Console.WriteLine("Chat Server Started ....");
            counter = 0;
            while (true) {
                try {
                    counter += 1;
                    clientSocket = serverSocket.AcceptTcpClient();

                    byte[] bytesFromClient = new byte[65536];
                    string dataFromClient = null;

                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFromClient, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFromClient);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    clientsList.Add(dataFromClient, clientSocket);

                    broadcast(dataFromClient + " Joined ", dataFromClient, false);

                    Console.WriteLine(dataFromClient + " Joined chat room ");
                    HandleClient client = new HandleClient();
                    client.startClient(clientSocket, dataFromClient, clientsList);
                } catch (Exception) {
                    Console.WriteLine("Already logged in");
                }

            }
            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadLine();
        }

        public static void broadcast(string msg, string uName, bool flag) {
            string users = GetUsers();
            if (msg.Length > 2) {
                string command = msg.Substring(0, 2);
                if (command == ".w") {
                    int endOfWhisper = msg.IndexOf(" ");
                    string whisperUser = msg.Substring(2, endOfWhisper - 2);
                    string actualMsg = msg.Substring(endOfWhisper + 1, msg.Length - endOfWhisper - 1);
                    foreach (DictionaryEntry Item in clientsList) {
                        if ((string)Item.Key == whisperUser || (string)Item.Key == uName) {
                            TcpClient broadcastSocket;
                            broadcastSocket = (TcpClient)Item.Value;
                            NetworkStream broadcastStream = broadcastSocket.GetStream();
                            Byte[] broadcastBytes = null;
                                broadcastBytes = Encoding.ASCII.GetBytes(uName + " whispered : " + actualMsg + users);

                            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                            broadcastStream.Flush();
                        }
                    }
                } else if (command == ".r") {
                    foreach (DictionaryEntry Item in clientsList) {
                        if ((string)Item.Key == uName) {
                            clientsList.Remove(Item.Key);
                            Console.WriteLine(uName + " removed");
                            broadcast(uName + " Disconnected ", uName, false);
                        }
                    }
                } else {
                    foreach (DictionaryEntry Item in clientsList) {
                        TcpClient broadcastSocket;
                        broadcastSocket = (TcpClient)Item.Value;
                        NetworkStream broadcastStream = broadcastSocket.GetStream();
                        Byte[] broadcastBytes = null;
                        if (flag == true) {
                            broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg + users);
                        } else {
                            broadcastBytes = Encoding.ASCII.GetBytes(msg + users);
                        }
                        broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                        broadcastStream.Flush();
                    }
                }
            } else {
                foreach (DictionaryEntry Item in clientsList) {
                    TcpClient broadcastSocket;
                    broadcastSocket = (TcpClient)Item.Value;
                    NetworkStream broadcastStream = broadcastSocket.GetStream();
                    Byte[] broadcastBytes = null;
                    if (flag == true) {
                        broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg + users);
                    } else {
                        broadcastBytes = Encoding.ASCII.GetBytes(msg + users);
                    }
                    broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                    broadcastStream.Flush();
                }

            }
        }

        public static string GetUsers() {
            string users = "$~$";
            foreach (DictionaryEntry Item in clientsList) {
                users += (string)Item.Key + " ";
            }
            return users;
        }
    }
}
