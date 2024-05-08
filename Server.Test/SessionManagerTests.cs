using System;
using System.Net;
using System.Net.Sockets;
using server;
using ChatApp;

namespace Test
{
	[TestFixture]
	public class SessionManagerTests
	{
		public TcpClient _client;
        List<TcpClient> clients = new List<TcpClient>();

        [Test]
		public void JoinGroup_ClientIsInList_ReturnTrue()
		{
			_client = new TcpClient();

            JoinGroup(_client);

			Assert.IsTrue(clients.Contains(_client));
		}

		[Test]
		public void JoinGroup_ClientIsNotInList_ReturnFalse()
		{
            _client = new TcpClient();

            JoinGroup(new TcpClient());

            Assert.IsFalse(clients.Contains(_client));
        }

		[Test]
		public void LeaveGroup_ClientRemovedFromList_ReturnFalse()
		{
            _client = new TcpClient();

            JoinGroup(_client);

            LeaveGroup(_client);

            Assert.IsFalse(clients.Contains(_client));

        }

        [Test]
        public void LeaveGroup_OneOfTwoRemovedFromList_ReturnFalse()
        {
            _client = new TcpClient();

            JoinGroup(_client);
            JoinGroup(new TcpClient());

            LeaveGroup(_client);

            Assert.IsFalse(clients.Contains(_client));

        }

        public void JoinGroup(TcpClient client)
        {
            clients.Add(client);
        }

        public void LeaveGroup(TcpClient tcpClient)
        {
            clients.Remove(tcpClient);
        }

    }
}

