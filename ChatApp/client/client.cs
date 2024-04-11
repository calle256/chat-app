using System.Net;
using System.Net.Sockets;
using System; 
namespace ChatApp
{
    public class Client
    {
        private TcpClient tcpClient;
        private string IP = "127.0.0.1"; // Change to what fits
        private int    port = 1234;      // So we start with 1 Server so these will be the same for all clients
                                     // but in the future we will probably want a function that assigns the correct server IP and port to (that function should probably be added to SocketUtility) 
                                     // the client depending on who they want to chat with

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
                Console.Write("Connection Succesful");
                return 0; 
            }
            catch 
            {
                Console.Write("ERROR: Connection not made to server!");
                return 1; 
            }
        }

        public void RunClient()
        {

            if (tcpClient.Connected)
            {
                NetworkStream stream = tcpClient.GetStream();

                Console.Write("Enter message to be sent: ");
                string msg = Console.ReadLine(); // läser in medelande
                if(msg == null){
                    return; 
                }
                SocketUtility.MsgSend(stream, msg); // skickar medelande

                String receive_msg = SocketUtility.MsgReceive(stream); // tar emot medelande från avsändare
                Console.Write("Received message: " + receive_msg); // printar medelandet

                stream.Close();
                tcpClient.Close();

            }
            else
            {
                Console.Write("ERROR: Failed Connection, trying again... ");
                this.Connect();
            }
        }
    }
}
