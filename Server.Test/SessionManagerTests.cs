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
        public GroupServer groupServer = new GroupServer("session");
		public TcpClient _client;
        //List<TcpClient> clients = new List<TcpClient>();

        [Test]
		public void JoinGroup_ClientIsInList_ReturnTrue()
		{
			_client = new TcpClient();

            groupServer.JoinGroup(_client);

			Assert.IsTrue(groupServer.Clients.Contains(_client));
		}

		[Test]
		public void JoinGroup_ClientIsNotInList_ReturnFalse()
		{
            _client = new TcpClient();

            groupServer.JoinGroup(new TcpClient());

            Assert.IsFalse(groupServer.Clients.Contains(_client));
        }

		[Test]
		public void LeaveGroup_ClientRemovedFromList_ReturnFalse()
		{
            _client = new TcpClient();

            groupServer.JoinGroup(_client);

            groupServer.LeaveGroup(_client);

            Assert.IsFalse(groupServer.Clients.Contains(_client));

        }

        [Test]
        public void LeaveGroup_OneOfTwoRemovedFromList_ReturnFalse()
        {
            _client = new TcpClient();

            groupServer.JoinGroup(_client);
            groupServer.JoinGroup(new TcpClient());

            groupServer.LeaveGroup(_client);

            Assert.IsFalse(groupServer.Clients.Contains(_client));

        }

        

        

    }
}

