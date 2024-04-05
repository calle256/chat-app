using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using server; 

namespace server
{
    public class Server
    {
        static List<TcpClient> clients = new List<TcpClient>(); 

        static void Main(string[] args)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Loopback, 1234);
            Listener listener = new Listener(ip);
            Thread handleDcThread = new Thread(HandleDisconnect); 
            handleDcThread.Start(); 
            

            while (true)
            {
                TcpClient tcpClient = listener.start();
                clients.Add(tcpClient);

                Thread clientThread = new Thread(HandleClient);
                clientThread.Start();
            }

        }
        public static void HandleClient(object obj){
            //TODO: Implement send and receive classes to be able to communicate between server and client
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            return; 
        }


        //helper function for HandleDisconnect. 
        public static bool IsConnected(TcpClient client){
            try{
                return !(client.Client.Poll(1, SelectMode.SelectRead) && client.Client.Available == 0); 
            }
            catch(SocketException){ return false; }
        }

        //Check connection status for each client every second and close streams for disconnected 
        //clients. 
        public static void HandleDisconnect(){
            while(true){
                Thread.Sleep(1000); 
                foreach (TcpClient client in clients){
                   if (!IsConnected(client)){
                      clients.Remove(client);  
                      client.Close();
                      break; 
                   } 
                }    
            }
        }

    }
}


