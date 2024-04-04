using System;
using System.Net.Sockets;
using System.Net;

namespace server
{
	public class ClientData
	{
		string userID;
		TcpClient tcpClient;

		public ClientData(string userID, TcpClient tcpClient)
		{
			this.tcpClient = tcpClient;
			this.userID = userID;
		}
	}
}

