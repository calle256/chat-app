using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
    public static class SocketUtility
    {
        

        public static void MsgSend(Socket socket, String msg)
        {
            // exempel på hur du initiazerar string msg = Console.ReadLine();
            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
                byte[] msgBuffer = Encoding.Default.GetBytes(msg); //omvandlar medelande till bytes
                socket.Send(msgBuffer, 0, msgBuffer.Length, 0); // skickar medelande

        }

        public static string MsgReceive(Socket socket)
        {
            try
            {
                byte[] msgBuffer = new byte[1024]; // siffran går att ändra för vilken storlek man vill ha
                int received = socket.Receive(msgBuffer, 0, msgBuffer.Length, 0);
                Array.Resize(ref msgBuffer, received);

                //string msg_received = Encoding.Default.GetString(msgBuffer);

                return Encoding.Default.GetString(msgBuffer);
            }
            catch 
            {
                Console.Write("ERROR: Message not received!");
                return "ERROR: Message not received!";
            }

        }

        //use ip and port to connect to a server
        public static Socket Connect(string IPAddress, int port)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sck.Connect(IPAddress,port);
            }
            catch
            {
                Console.Write("ERROR: Connection not found!");
                throw; // is here to retry the hole main code if sck.Connect(localEndPoint);
                      // could use a better soluition as this can cause infinity loop, like a retrying function
            }

            return sck;
        }


    }
}
