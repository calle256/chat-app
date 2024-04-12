using System;
using ChatApp;

class Program
{
    static void Main()
    {
        Client client = new Client("127.0.0.1", 1234);
        client.Connect();
        client.RunClient(); 
    }
}
