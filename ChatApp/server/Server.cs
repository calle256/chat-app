using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp;
using server;

namespace server
{
    public class Server
    {
        private readonly List<TcpClient> clients = new List<TcpClient>();
        private GroupServer groupChat = new GroupServer("groupsession");
        private int port; 
        private IPAddress ip; 

        public Server(int port, IPAddress ip)
        {
            this.port = port; 
            this.ip = ip;  
        }

        public void StartServer()
        {
            IPEndPoint ip = new IPEndPoint(this.ip, this.port);
            Listener listener = new Listener(ip);
            Thread handleDcThread = new Thread(HandleDisconnect);
            handleDcThread.Start();

            while (true)
            {
                TcpClient tcpClient = listener.start();
                clients.Add(tcpClient);
              
                Thread clientThread = new Thread(() => groupChat.HandleClientConnection(tcpClient));
                clientThread.Start();
              

            }
        }

        //helper function for HandleDisconnect.
        public static bool IsConnected(TcpClient client)
        {
            try
            {
                return !(
                    client.Client.Poll(1, SelectMode.SelectRead) && client.Client.Available == 0
                );
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //Check connection status for each client every second and close streams for disconnected
        //clients.
        public void HandleDisconnect()
        {
            while (true)
            {
                Thread.Sleep(1000);
                foreach (TcpClient client in clients)
                {
                    if (!IsConnected(client))
                    {
                        clients.Remove(client);
                        client.Close();
                        break;
                    }
                }
            }
        }
    }
}
