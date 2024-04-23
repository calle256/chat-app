using System;
using ChatApp;

class Program
{
    static void Main(string[] args)
    {
        int port; 
        string ip; 
        if(args.Length < 2)
            port = 1234; 
        else
        {
            port = Int32.Parse(args[1]); 
        }
        if(args.Length < 1)
            ip = "83.252.212.217"; 
        else
        {
            ip = args[0]; 
        }
        Client client = new Client(ip,port);
        client.Connect();
        client.RunClient();
    }
}
