using ChatApp; 
using System.Net.Sockets; 
using System.Net;
using System.Text; 
public class Tests
{
    private Client _client;
    [SetUp]
    public void Setup()
    {
        _client = new Client("127.0.0.1", 1122); 
    }

    [Test]
    public void TestClientConnection()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1"); 
        TcpListener server = new TcpListener(ip, 1122); 
        server.Start(); 
        int result = _client.Connect(); 
        Assert.AreEqual(0, result); 
    }

    //checks if the function write the correct msg to the stream
    [Test]
    public void TestClientSend()
    {
        TcpClient client = new TcpClient();
        TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 2345); 
        Thread listenThread = new Thread(() => {
            Console.WriteLine("hello"); 
            listener.Start(1);
            listener.AcceptSocket(); 
        }); 
        listenThread.Start(); 
        Console.WriteLine("we are here"); 
        Thread.Sleep(10); 
        client.Connect("127.0.0.1", 2345); 
        var stream = client.GetStream();
        string msg = "Testing, Send function";
        byte[] buf = new byte[100]; 
        SocketUtility.MsgSend(stream, msg);
        int res = stream.Read(buf, 0, 100); 
        string expectedMsg = Encoding.UTF8.GetString(buf);
        listenThread.Join(); 
        listener.Stop(); 
        Assert.AreEqual(msg, expectedMsg);
    }

    //Receive tester
    /*
     1. Kolla ifall den returnar den förväntade meddelande när den läser från stream
     2. Kollar att funktionen returnar en tom sträng när stream är ledig
      
     */

    

    
}
