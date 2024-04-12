using System;
using System.Net;
using System.Net.Sockets;

namespace server
{
    public class ClientData
    {
        //string userID;
        TcpClient tcpClient;

        public ClientData(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }
    }
}
