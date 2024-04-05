using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;


namespace server
{
    public class Server
    {
        static List<ClientData> clients;

        static void Main(string[] args)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Loopback, 1234);
            Listener listener = new Listener(ip);

            
            while (true)
            {
                TcpClient tcpClient = listener.start();
                clients += tcpClient;

                Thread clientThread = new Thread(tcpClient);
                clientThread.Start();
            }

        }
    }

}


