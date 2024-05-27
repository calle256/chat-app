using System.Net; 
using System.Net.Sockets;
using ChatApp;
using server;

namespace Test
{
   public class CapacityTest
   {
      [Test]
      public void Test(){
            Server server = new Server(23456, IPAddress.Parse("127.0.0.1"));  
            Thread serverThread = new Thread(() =>{
                server.StartServer(); 
            });
            serverThread.Start(); 
            Thread.Sleep(10); 
            for(int i = 0; i<100; ++i)
            {
                Console.WriteLine("Client number " + i); 
                TcpClient client = new TcpClient(); 
                client.Connect("127.0.0.1", 23456);
                try{
                    //Set new clients username
                    SocketUtility.MsgSend(client.GetStream(), "test"); 
                    //Try to send a message to all connected clients
                    SocketUtility.MsgSend(client.GetStream(), "test"); 
                    Thread.Sleep(10); 
                }

                catch(Exception e){
                  break; 
                }
            }
      }
   }
}
