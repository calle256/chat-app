using System;
using server;

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

