using System.Net;
using System.Net.Sockets;
using ChatApp;

public class Tests
{
    private Client _client;
    private SocketUtility _SocketUtility;

    [SetUp]
    public void Setup()
    {
        _client = new Client("127.0.0.1", 1234);
    }

    [Test]
    public void TestClientConnection()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        TcpListener server = new TcpListener(ip, 1234);
        server.Start();
        int result = _client.Connect();
        Assert.AreEqual(0, result);
    }

    //checks if the function write the correct msg to the stream
    [Test]
    public void TestClientSend()
    {
        var stream = new MemoryStream();
        string msg = "Testing, Send function";
        _SocketUtility.MsgSend(stream, msg);
        string expectedMsg = Encoding.UTF8.GetString(stream.ToArray());
        Arrest.AreEqual(msg, expectedMsg);
    }

    //Receive tester
    /*
     1. Kolla ifall den returnar den f�rv�ntade meddelande n�r den l�ser fr�n stream
     2. Kollar att funktionen returnar en tom str�ng n�r stream �r ledig
      
     */
}
