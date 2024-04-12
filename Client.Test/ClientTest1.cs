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
}

    //checks if the function write the correct msg to the stream

    //Receive tester
    /*
     1. Kolla ifall den returnar den f�rv�ntade meddelande n�r den l�ser fr�n stream
     2. Kollar att funktionen returnar en tom str�ng n�r stream �r ledig
 */ 
