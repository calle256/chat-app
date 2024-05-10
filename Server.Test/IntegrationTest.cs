using System.Net; 
using System.Net.Sockets;
using ChatApp;
using server;

namespace Test
{
    public class ServerIntegrationTest
    {
        //The test connects a mock client to the server and 
        //asserts that the client is added to the GroupSession
        //upon connection, requiring the Server, GroupSession and
        //SocketUtility components to work properly. 
        [Test]
        public void IntTest()
        {
            Server server = new Server(12345, IPAddress.Parse("127.0.0.1"));  
            Thread serverThread = new Thread(() =>{
                server.StartServer(); 
            });
            serverThread.Start();
            Thread.Sleep(10); 
            TcpClient _client = new TcpClient(); 
            _client.Connect("127.0.0.1", 12345);
            string _str1 = SocketUtility.MsgReceive(_client.GetStream());
            SocketUtility.MsgSend(_client.GetStream(), "test"); 
            Thread.Sleep(10); 
            Assert.IsTrue(server.groupChat.Clients.Count == 1); 
        }
    } 
}
