using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace ChatApp
{
    public class Client
    {
        private TcpClient tcpClient;
        private string IP = "127.0.0.1";
        private int port = 1234;



        public Client(string IPAddress, int port)
        {
            this.IP = IPAddress;
            this.port = port;
            this.tcpClient = new TcpClient();
        }



        public int Connect()
        {
            try
            {
                tcpClient.Connect(IP, port);
                Console.Write("Connection Succesful\n");
                return 0;
            }
            catch
            {
                Console.Write("ERROR: Connect()\n");
                return 1;
            }
        } // end Connect

        public async Task<int> ConnectAsynchronous()
        {
            try
            {
                await tcpClient.ConnectAsync(IP, port);
                Console.Write("Connection Succesful\n");
                return 0;
            }
            catch
            {
                Console.Write("ERROR: ConnectAsynchronous()\n");
                return 1;
            }
        }// end ConnectAsynchronous



        public void RunClient()
        {
            if (tcpClient.Connected)
            {
                NetworkStream stream = tcpClient.GetStream();

                Console.Write("Enter message to be sent: ");
                string? msg = Console.ReadLine();
                if (msg == null)
                {
                    return;
                }
                SocketUtility.MsgSend(stream, msg);

                String receive_msg = SocketUtility.MsgReceive(stream);
                Console.Write("Received message: " + receive_msg);

                stream.Close();
                tcpClient.Close();
            }
            else
            {
                Console.Write("ERROR: Failed Connection, trying again... \n");
                this.Connect();
            }
        }// end RunClient


        public async Task RunClientAsynchronous()
        {
            try
            {
                if (!tcpClient.Connected) //tries to establish connection if not connected, so should garantee that it always looks for a connection if not connected
                {
                    await ConnectAsynchronous();
                }
                
                
                var sendAsyncTask       = MsgSender(tcpClient.GetStream());
                var receiveAsyncTask    = MsgReceiver(tcpClient.GetStream());

                //does this when above is done
                await Task.WhenAll(sendAsyncTask, receiveAsyncTask);
            }
            catch
            {
                Console.Write("ERROR: Connection failed! \n");
            }
            //closes the program
            finally
            {
                if (tcpClient.Connected)
                {
                    
                    tcpClient.Close();
                }
            }
        } // end RunClientAsynchronous


        public async Task MsgSender(NetworkStream stream)
        {

            while (tcpClient.Connected)
            {
                string? msg = Console.ReadLine();
                Console.Write("Enter message to send: ");


                // if this happens then it jumps to finally block and cl0ses the stream
                if (msg == "exit") // vi kan ändra till ngt mera passade om vi vill
                {
                    Console.Write("Exiting Client...\n ");
                    tcpClient.Close();
                    break;
                }

                if (msg != null)
                {
                    await SocketUtility.MsgSendAsync(stream, msg);
                }

            }
        }

        public async Task MsgReceiver(NetworkStream stream)
        {
            while (tcpClient.Connected)
            {
                string receive_msg = await SocketUtility.MsgReceiveAsync(stream);

                if (receive_msg != string.Empty)
                {
                    Console.Write("Received message: " + receive_msg + "\n");
                }
                else
                {
                    break;
                }
                
            }


        }


    }
}
