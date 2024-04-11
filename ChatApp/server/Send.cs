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
    public class Send
    {

        public void MsgSend( Socket socket, String msg)
        {
            // exempel på hur du initiazerar string msg = Console.ReadLine();
            // socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                
                byte[] msgBuffer = Encoding.UTF8.GetBytes(msg); //omvandlar medelande till bytes
                socket.Send(msgBuffer, 0, msgBuffer.Length, 0); // skickar medelande

            }
            catch
            {
                Console.Write("ERROR: Message Couldn't be sent!");
            }


        }
    }
}
