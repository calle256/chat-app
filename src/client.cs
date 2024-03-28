using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.IO; 
using System.Net; 
using System.Net.Sockets; 
namespace client{
  public class Client {
  
    public void Connect(){
      try{
        TcpClient tcpInt = new TcpClient(); 
        Console.WriteLine("Connecting...");
        tcpInt.Connect("127.0.0.1", 11000); 
        Console.WriteLine("Connected!"); 
        String msg = Console.ReadLine(); 
        Stream stm = tcpInt.GetStream(); 
        ASCIIEncoding asen = new ASCIIEncoding(); 
        byte[] bytearr = asen.GetBytes(msg); 
        stm.Write(bytearr, 0, bytearr.Length); 
      }
      catch (Exception e){
        Console.WriteLine(e.StackTrace); 
      }
    }
  }
}
