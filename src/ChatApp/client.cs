using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
    public class Client
    {
        static Socket sck;
        static string IP = "127.0.0.1"; // Change to what fits
        static int port = 1234;      // So we start with 1 Server so these will be the same for all clients
                                     // but in the future we will probably want a function that assigns the correct server IP and port to (that function should probably be added to SocketUtility) 
                                     // the client depending on who they want to chat with

        static void Main(string[] args)
        {

            //sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);

            sck = SocketUtility.Connect(IP, port); // connect client to server


            Console.Write("Enter Message to send: ");
            string msg = Console.ReadLine();

            SocketUtility.MsgSend(sck, msg);

            Console.WriteLine("Received message: " + SocketUtility.MsgReceive(sck));

            sck.Shutdown(SocketShutdown.Both);
            sck.Close();
            Console.Read();






        }

    }
}
