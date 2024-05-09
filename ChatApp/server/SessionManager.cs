using System;
using System.Net.Sockets;
using ChatApp;

namespace server
{
    public class SessionManager
    {
        private List<GroupServer> sessions; 
        //private Dictionary<string, Dictionary<string, UserInfo>> sessions = new Dictionary<string, Dictionary<string, UserInfo>>();
        private TimeSpan sessionTimeout = TimeSpan.FromMinutes(30);

        public void CreateGroup(string groupId)
        {
            GroupServer group = new GroupServer(groupId); 
        }

    }

    public class GroupServer
    {
        private string SessionId; 
        public List<TcpClient> Clients; 
        //private Dictionary<string, List<TcpClient>> groups = new Dictionary<string, List<TcpClient>>();
        public GroupServer(string id)
        {
            this.SessionId = id; 
            this.Clients = new List<TcpClient>(); 
        }
        
        public void JoinGroup(TcpClient client)
        {
            Clients.Add(client); 
        }

        public void LeaveGroup(TcpClient tcpClient)
        {
            Clients.Remove(tcpClient); 
        }

        // detta ska vara i server under HandleClient sen eventuellt om allt funkar som det ska enligt resten av gänget.
        // groupId får skrivas in när någon ansluter?
        public void HandleClientConnection(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            string userNameRequest = "Enter username";
            SocketUtility.MsgSend(stream, userNameRequest);

            string userName = SocketUtility.MsgReceive(stream);

            UserInfo userInfo = AuthenticateUser(userName,client);
            string welcome_msg = "Welcome " + userName + "\nOnline members: " + this.Clients.Count; 
            SocketUtility.MsgSend(stream, welcome_msg);
            JoinGroup(client); 
            while(true){
                try{                
                    string msg = ReceiveMsg(client); 
                    if(msg != String.Empty)
                      SendMsgToAll(userName + ": " + msg, client); 
                }
                catch (Exception e){
                    Console.WriteLine(e);
                    SendMsgToAll(userName + " has disconnected.", client); 
                    stream.Close(); 
                    client.Close(); 
                    Clients.Remove(client); 
                    return; 
                }
            }
            
            //kanske ha något mer här för att validera rätt grupp osv.
        }

        private static UserInfo AuthenticateUser(string username, TcpClient client)
        {
            return new UserInfo { UserName = username, 
                                  Client = client, 
                                  LastActive = DateTime.Now };
        }
       
        private string ReceiveMsg(TcpClient sender)
        {
            string msg = SocketUtility.MsgReceive(sender.GetStream()); 
            return msg; 
        }
        private void SendMsgToAll(string msg, TcpClient sender)
        {
            lock(Clients)
            {
                foreach (var client in Clients)
                {
                    if (client != sender)
                    {
                        var socket = client.GetStream();
                        SocketUtility.MsgSend(socket, msg);
                    }
                }
            }
        }
    }

    

    public class UserInfo
    {
        public string UserName { get; set; }
        public TcpClient Client; 
        public DateTime LastActive { get; set; }
    }
}

