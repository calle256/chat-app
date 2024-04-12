using System.Net; 
using System.Net.Sockets;
using ChatApp; 
using server; 

namespace Test;
public class Tests
{
    private Listener _listener;     
    [SetUp]
    public void Setup()
    {
        IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234); 
        _listener = new Listener(ip); 
    }
    
    //Test to ensure Listener start method properly returns a TcpClient instance 
    //when a connection is made. 
    [Test]
    public void ListenerTest()
    {
        TcpClient client = new TcpClient(); 
        TcpClient result = null;  
        Thread listenerThread = new Thread(()=>
            {
                result = _listener.start(); 
            });
        listenerThread.Start(); 
        Thread.Sleep(10);
        client.Connect("127.0.0.1", 1234); 
        listenerThread.Join(); 
        Assert.IsNotNull(result);
    }
}
