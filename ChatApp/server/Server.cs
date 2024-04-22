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
        private readonly Send sender = new Send();
        private GroupServer groupChat = new GroupServer("groupsession"); 

        public void StartServer()
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            Listener listener = new Listener(ip);
            Thread handleDcThread = new Thread(HandleDisconnect);
            handleDcThread.Start();

            while (true)
            {
                TcpClient tcpClient = listener.start();
                clients.Add(tcpClient);
              
                Thread clientThread = new Thread(() => groupChat.HandleClientConnection(tcpClient));
                clientThread.Start();
              
                Thread clientHandlerThread = new Thread(() => ClientHandler(tcpClient));
                clientHandlerThread.Start();

            }
        }

        // Method to manage Client sessions
        public void ClientHandler(TcpClient client)
        {
            try
            {
                using(NetworkStream stream = client.GetStream())
                {
                    while(true)
                    {
                        string receivedMsg = SocketUtility.MsgReceive(stream);

                        if(string.IsNullOrEmpty(receivedMsg))
                        {
                            break;
                        }
                        Console.WriteLine("Recieved: {0}", receivedMsg);

                        // Send the message back to the client
                        SocketUtility.MsgSend(stream, "Response: " + receivedMsg);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Client Handling Error!!: {0}", e.Message);
            }

            client.Close();
            clients.Remove(client);
        }

        // method to broadcast message to all users
        private void SendMsgToAll(string msg, TcpClient sender)
        {
            lock(clients)
            {
                foreach (var client in clients)
                {
                    if (client != sender)
                    {
                        var socket = client.Client;
                        this.sender.MsgSend(socket, msg);
                    }
                }
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
            catch (SocketException)
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
