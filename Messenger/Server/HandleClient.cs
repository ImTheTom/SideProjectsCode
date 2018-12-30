using System;
using System.Threading;
using System.Net.Sockets;
using System.Collections;

namespace Server {
    public class HandleClient {
        TcpClient clientSocket;
        string clientUsername;
        Hashtable clientsList;

        public void startClient(TcpClient inClientSocket, string clientNo, Hashtable cList) {
            this.clientSocket = inClientSocket;
            this.clientUsername = clientNo;
            this.clientsList = cList;
            Thread getInformationFromClientsThread = new Thread(GetChat);
            getInformationFromClientsThread.Start();
        }

        private void GetChat() {
            int requestCount = 0;
            string dataFromClient = null;
            requestCount = 0;

            while (true) {
                try {
                    byte[] bytesFromClient = new byte[65536];
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFromClient, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFromClient);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine("From client - " + clientUsername + " : " + dataFromClient);
                    Program.broadcast(dataFromClient, clientUsername, true);
                } catch (InvalidOperationException) {
                    Console.WriteLine("Invalid Operation Occured during player disconnection");
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
