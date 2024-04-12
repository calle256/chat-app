using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace ChatApp
{
    public static class SocketUtility
    {
        public static void MsgSend(NetworkStream stream, string msg)
        {
            if (stream.CanWrite)
            {
                byte[] msgBuffer = Encoding.Default.GetBytes(msg); //omvandlar medelande till bytes
                stream.Write(msgBuffer, 0, msgBuffer.Length); // skickar medelande
            }
        }

        public static string MsgReceive(NetworkStream stream)
        {
            try
            {
                if (stream.CanRead)
                {
                    byte[] msgBuffer = new byte[1024]; // siffran går att ändra för vilken storlek man vill ha
                    int received = stream.Read(msgBuffer, 0, msgBuffer.Length); // läser datan från stream

                    if (received > 0)
                    {
                        return Encoding.Default.GetString(msgBuffer, 0, received);
                    }
                }
                return string.Empty;
            }
            catch
            {
                Console.Write("ERROR: Message not received!");
                return "ERROR: Message not received!";
            }
        }
    }
}
