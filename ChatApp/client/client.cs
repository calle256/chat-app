using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace ChatApp
{
    public class Client
    {
        private TcpClient tcpClient;
        private string IP = "127.0.0.1";
        private int port = 1234;



        public Client(string IPAddress, int port)
        {
            this.IP = IPAddress;
            this.port = port;
            this.tcpClient = new TcpClient();
        }



        public int Connect()
        {
            try
            {
                tcpClient.Connect(IP, port);
                Console.Write("Connection succesful!\n");
                return 0;
            }
            catch
            {
                Console.Write("ERROR: Connect!\n");
                return 1;
            }
        } 




        public void RunClient()
        {
            if (tcpClient.Connected)
            {
                Stream stream = tcpClient.GetStream();
                Thread msgSend = new Thread(() => MsgSend(stream)); 
                Thread msgReceive = new Thread(() => MsgRecieve(stream));
                msgSend.Start(); 
                msgReceive.Start(); 
            }
            else
            {
                Console.Write("ERROR: Failed Connection, trying again... \n");
                this.Connect();
            }
        }
        public void MsgRecieve(Stream stream){
            while(tcpClient.Connected){
                if(tcpClient.Client.Poll(1000, SelectMode.SelectRead) && tcpClient.Client.Available == 0)
                    break; 
                string msg = SocketUtility.MsgReceive(stream); 
                Console.Write(msg + "\n"); 
            }
        }

        public void MsgSend(Stream stream){
            while (true){
                if(tcpClient.Client.Poll(1000, SelectMode.SelectRead) && tcpClient.Client.Available == 0)
                    break; 
                string msg = Console.ReadLine(); 
                if (msg != null){
                    SocketUtility.MsgSend(stream, msg); 
                }
            }
        }
    }
}
