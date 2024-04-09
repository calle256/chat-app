using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChatApp
{
    public class Receive
    {
        public string MsgReceive(Socket socket)
        {
            try
            {
                byte[] msgBuffer = new byte[1024]; // siffran går att ändra för vilken storlek man vill ha
                int received = socket.Receive(msgBuffer, 0, msgBuffer.Length, 0);
                Array.Resize(ref msgBuffer, received);

                string msg_received = Encoding.Default.GetString(msgBuffer);

                return msg_received;
            }
            catch 
            {
                Console.Write("ERROR: Message not received!");
                return "ERROR: Message not received!";
            }

        }
    }
}
