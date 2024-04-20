using System;
using ChatApp;

namespace server
{
    class Controller
    {
        static void Main(string[] args)
        {

            Server server = new Server();
            server.StartServer();

        }
    }
}
