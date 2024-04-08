using System.Net;
using System.Net.Sockets;


namespace ChatApp
{
    public class Client
    {
        private Socket sck;
        private string IP = "127.0.0.1"; // Change to what fits
        private int    port = 1234;      // So we start with 1 Server so these will be the same for all clients
                                     // but in the future we will probably want a function that assigns the correct server IP and port to (that function should probably be added to SocketUtility) 
                                     // the client depending on who they want to chat with

        public Client(string IPAddress, int port) 
        {
            this.IP = IPAddress;
            this.port = port;
        }

        public void Connect()
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var IPEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            sck.Connect(IPEndPoint);
        }

        public void RunClient()
        {
            this.Connect();
            
            //read msg and send
            Console.Write("Enter message: ");
            string msg = Console.ReadLine();
            SocketUtility.MsgSend(sck, msg);

            //recieve message print in terminal
            Console.Write("Recieved message: " + SocketUtility.MsgReceive(sck));

            sck.Shutdown(SocketShutdown.Both);
            sck.Close();



        }

    }
}
