using System;
using System.Net.Sockets;
using ChatApp;
namespace server
{
    public class SessionManager
    {
        private Dictionary<string, Dictionary<string, UserInfo>> sessions = new Dictionary<string, Dictionary<string, UserInfo>>();
        private TimeSpan sessionTimeout = TimeSpan.FromMinutes(30);

        public string CreateSession(string groupId, UserInfo userInfo)
        {
            if (!sessions.TryGetValue(groupId, out Dictionary<string, UserInfo>? value))
            {
                value = new Dictionary<string, UserInfo>();
                sessions[groupId] = value;
            }

            string sessionId = Guid.NewGuid().ToString();
            value[sessionId] = userInfo;
            return sessionId;
        }

    }

    public class GroupServer
    {
        private Dictionary<string, List<TcpClient>> groups = new Dictionary<string, List<TcpClient>>();
        private SessionManager sessionManager = new SessionManager();

        public void JoinGroup(TcpClient tcpClient, string groupId)
        {
            if (!groups.TryGetValue(groupId, out List<TcpClient>? value))
            {
                value = new List<TcpClient>();
                groups[groupId] = value;
            }

            value.Add(tcpClient);
        }

        public void LeaveGroup(TcpClient tcpClient, string groupId)
        {
            if (groups.TryGetValue(groupId, out List<TcpClient>? value))
            {
                value.Remove(tcpClient);
            }
        }

        // detta ska vara i server under HandleClient sen eventuellt om allt funkar som det ska enligt resten av gänget.
        // groupId får skrivas in när någon ansluter?
        public void HandleClientConnection(TcpClient client, string groupId)
        {
            NetworkStream stream = client.GetStream();
            string userNameRequest = "Enter username";
            SocketUtility.MsgSend(stream, userNameRequest);

            string userName = SocketUtility.MsgReceive(stream);

            UserInfo userInfo = AuthenticateUser(userName);

            string sessionId = sessionManager.CreateSession(groupId, userInfo);
            SocketUtility.MsgSend(stream, sessionId);

            //kanske ha något mer här för att validera rätt grupp osv.
        }

        private static UserInfo AuthenticateUser(string username)
        {
            return new UserInfo { UserName = username, LastActive = DateTime.Now };
        }
    }

    

    public class UserInfo
    {
        public string UserName { get; set; }
        public DateTime LastActive { get; set; }
    }
}

