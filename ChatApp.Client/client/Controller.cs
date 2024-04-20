using System;
using ChatApp;

namespace ChatApp.Client
{
    class Controller
    {
        static void Main(string[] args)
        {


            Client client = new Client("127.0.0.1", 1234);
            client.Connect();
            client.RunClient();

        }
    }
}
