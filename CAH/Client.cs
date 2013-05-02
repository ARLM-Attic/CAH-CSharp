using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace CAH
{
    public class Client
    {

        /**
         *          0= defult
         *          1=send card to all except the client that sent
         *          2=send card from game to indiviual
         *          3= send card to game
         *          4= tell clients to flip
         *          5=inform the game that a player has been added
         *          6=start game
         *          7 = score is sent
         *          8 = recive induvual card
         *          9 =  recive group card
         *          50= check if username is taken
         *          51= username not taken
         *          52 = username taken
         *          100= disconnet
         */



        private String userName;
        public delegate void StringEventHandler(object sender, String s);
        public event EventHandler recivedWhiteCard, recivedBlackCard, needToFlipCards, recivedDeckCard,gameStart;
        public event StringEventHandler playerAdded;
        private Card latest;
        TcpClient server = null;

        public Client(String name)
        {
             userName = name;
        }
       
        public void connect(String ip)
        {
            server = new TcpClient();

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), Server.PORT);
            server.Connect(serverEndPoint);
            Debug.WriteLine("connected");
             ASCIIEncoding encoder = new ASCIIEncoding();
             byte[] message = encoder.GetBytes(userName);
             server.GetStream().Write(message, 0, message.Length);
             server.GetStream().Flush();
             Thread t = new Thread(new ThreadStart(respondToMessages));
             t.Start();
        }

        public void send(byte[] message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            NetworkStream clientStream = server.GetStream();
                clientStream.Write(message, 0, message.Length);
                clientStream.Flush();
                Debug.WriteLine("sent");
        }

        private void respondToMessages()
        {
            NetworkStream serverStream = server.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;
                message = new byte[4096];
                try
                {
                    //blocks until a client sends a message
                    bytesRead = serverStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                switch (message[0])
                {
                    case 9:
                        Debug.WriteLine(encoder.GetString(message, 1, bytesRead - 1));
                        setCardFromString(encoder.GetString(message,1,bytesRead-1));
                        recivedWhiteCard.Invoke(this, EventArgs.Empty);
                        break;
                    case 8:
                        Debug.WriteLine(encoder.GetString(message, 1, bytesRead - 1));
                        setCardFromString(encoder.GetString(message, 1, bytesRead - 1));
                        recivedDeckCard(this, EventArgs.Empty);
                        break;
                    case 4:
                        needToFlipCards(this, EventArgs.Empty);
                        break;
                    case 5:
                        playerAdded(this, encoder.GetString(message, 1, bytesRead - 1));
                        break;
                    case 6:
                        Debug.WriteLine("the game was started");
                        //gameStart.Invoke(this, EventArgs.Empty);
                        break;
                    default:
                        Debug.WriteLine("Well nothing happened here b/c there was a value that was pass that we didnt account for");
                        break;
                }



               
            }
        }

        private void setCardFromString(String s)
        {
            CardEncoder ce = new CardEncoder();
            latest = ce.decodeCard(s);
        }


        public Card getLatestCard()
        {
            return latest;
        }

        public void gameConnect()
        {
            server = new TcpClient();

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Server.PORT);

            server.Connect(serverEndPoint);
            Debug.WriteLine("connected");
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] message = encoder.GetBytes(userName);
            server.GetStream().Write(message, 0, message.Length);
            server.GetStream().Flush();
        }

        public void gameSendStart()
        {
            byte[] message = { 6 };
            send(message);
        }

        public void sendACard(String name,Card c)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            CardEncoder ce = new CardEncoder();
            String card = ce.encodeCard(c);
            NetworkStream serverStream = server.GetStream();
            byte[] message = new byte[1];
            message[0] = 2;
            serverStream.Write(message,0,message.Length);
            serverStream.Flush();
            serverStream.Write(encoder.GetBytes(card), 0, encoder.GetBytes(card).Length);
            serverStream.Flush();
            serverStream.Write(encoder.GetBytes(name), 0, encoder.GetBytes(name).Length);
            serverStream.Flush();

        }

        public String[] getOpenIps()
        {
            List<String> ips = new List<String>();
            IPHostEntry hostInfo = Dns.GetHostByName(String.Empty);
            String[] ipv4=
            hostInfo.AddressList[0].ToString().Split(new Char[]{'.'});
            int port = 2999;
           
            for (int i = 100; i < 254; i++)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                String s = ipv4[0] + "." + ipv4[1] + "." + ipv4[2] + "." + i;
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(s), port);
                try
                {
                    IAsyncResult result = socket.BeginConnect(IPAddress.Parse(s), port, null, null);

                    bool success = result.AsyncWaitHandle.WaitOne(1000, true);
                    if(success)
                    ips.Add(s);
                    else
                        Console.WriteLine(i + " Did not connect");
                }
                catch
                {
                    Console.WriteLine(i + " Did not connect");
                    socket.Close();
                    continue;
                        
                }

            } 
            
            return null;
        }

        

       





    }


}
