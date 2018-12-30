using System;
using System.Net;
using System.Net.Sockets;

namespace MessengerApplicaiton {
    public class IP {
        private string clientIp = "";

        public IP() {
            Console.WriteLine("New IP class created");
            SetClientIP();
        }

        private void SetClientIP() {
            IPHostEntry userHost;
            userHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in userHost.AddressList) {
                if(ip.AddressFamily == AddressFamily.InterNetwork) {
                    clientIp = ip.ToString();
                    return;
                }
            }
            clientIp = "127.0.0.1";
        }

        public string GetClientIP() {
            return clientIp;
        }
    }
}
