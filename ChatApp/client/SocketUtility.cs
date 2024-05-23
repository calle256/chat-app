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
        public static void MsgSend(Stream stream, string msg)
        {
            if (stream.CanWrite)
            {
                byte[] msgBuffer = Encoding.UTF8.GetBytes(msg); 
                stream.Write(msgBuffer, 0, msgBuffer.Length); 
            }
        }

        public static string MsgReceive(Stream stream)
        {
            try
            {
                if (stream.CanRead)
                {
                    byte[] msgBuffer = new byte[1024];
                    int received = stream.Read(msgBuffer, 0, msgBuffer.Length); 

                    if (received > 0)
                    {
                        return Encoding.UTF8.GetString(msgBuffer, 0, received);
                    }
                }
                return string.Empty;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
        }
    }
}
