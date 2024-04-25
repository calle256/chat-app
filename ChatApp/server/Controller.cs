using System;
using ChatApp;
using System.Net; 

namespace server
{
    class Controller
    {
        static void Main(string[] args)
        {
            int port; 
            string ipAddress; 
            if(args.Length < 2)
            {
                port = 1234; 
            }
            else
            {
                port = Int32.Parse(args[1]); 
            }
            if(args.Length < 1)
            {
                ipAddress = "192.168.0.207"; 
            }
            else
            {
                ipAddress = args[0]; 
            }

            Server server = new Server(port, IPAddress.Parse(ipAddress));
            server.StartServer();
        }
    }
}
