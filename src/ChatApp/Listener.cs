using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Listener
    {
        PEndPoint server_IPendPoint;
        TcpListener listener;

        public Listener(IPEndPoint server_EndPoint)
        {
            listener = new TcpListener(server_EndPoint);
            this.server_IPendPoint = server_EndPoint;
        }


        
        public TcpClient start()
        {

            Console.WriteLine("Waiting for connection.");
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Found connection");


            return client;
        }
    }
}
