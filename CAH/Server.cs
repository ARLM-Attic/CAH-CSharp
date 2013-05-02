using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace CAH
{
    class Server
    {
        /**
         *  server
         *      get the name
         *      wait for card
         *  sending cards
         *      next bytes
         *          string of text
         *          store length of bytes
         *      if black
         *          the next bytes will be the number of requred spaces
         *  sending information
         *      firest send/recive
         *          0= defult
         *          1=send card to all except the client that sent
         *          2=send card from game to indiviual
         *          3= send card to game
         *          4= tell clients to flip
         *          5=inform the game that a player has been added
         *          6=start game
         *          7 = score is sent
         *          50= check if username is taken
         *          51= username not taken
         *          52 = username taken
         *          100= disconnet
         *          
         *      next bytes 
         *          what you want
         *      recive byte,b, string
         */





        private TcpListener tcpListener;
    private Thread listenThread;
    private Dictionary<TcpClient, String> clients = new Dictionary<TcpClient, String>();
    private List<Thread> threads = new List<Thread>();
    private TcpClient game = new TcpClient();

    public static readonly Int16 PORT = 2485;

    public Server()
    {
      this.tcpListener = new TcpListener(IPAddress.Any,Server.PORT);
      this.listenThread = new Thread(new ThreadStart(ListenForClients));
      this.listenThread.Start();
    }


    private void ListenForClients()
    {
        this.tcpListener.Start();

        while (true)
        {
            //blocks until a client has connected to the server
            TcpClient client = this.tcpListener.AcceptTcpClient();

            //create a thread to handle communication 
            //with connected client
            Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
            clientThread.Start(client);
            threads.Add(clientThread);
        }
    }

    private void HandleClientComm(object client)
    {
        TcpClient tcpClient = (TcpClient)client;
        NetworkStream clientStream = tcpClient.GetStream();
        byte[] message=new byte[4096];
        int bytesRead = 0;
        string name = "";
        ASCIIEncoding encoder = new ASCIIEncoding();

        //wait for a packet with the name
        bytesRead = clientStream.Read(message, 1, 4095);
        name = encoder.GetString(message, 1, bytesRead);
        name.TrimEnd();
        if (name.ToLower().Equals("game"))
        {
            game = tcpClient;
        }
        else
        {

            clients.Add(tcpClient, name);
            message[0] = 5;
            informGameOfNewPlayer(message);
        }
         

        while (true)
        {
            bytesRead = 0;
            message = new byte[4096];
            try
            {
                //blocks until a client sends a message
                bytesRead += clientStream.Read(message, 0, 1);
                
                switch (message[0])
                {
                    default:
                        break;
                    case 1:
                        bytesRead += clientStream.Read(message, bytesRead, 4096 - bytesRead);
                        push(tcpClient, message, bytesRead);
                        break;
                    case 2:
                        bytesRead += clientStream.Read(message, bytesRead, 4096 - bytesRead);
                        byte[] extra = new byte[200];
                        String nameToSendTo = "";
                        clientStream.Read(extra, 0, 200);
                        nameToSendTo = encoder.GetString(extra);
                        Debug.WriteLine("the server sent :"+encoder.GetString(message,1,message.Length-1)+" : to "+ nameToSendTo);
                        Debug.WriteLine("");Debug.WriteLine("");Debug.WriteLine("");
                        sendToSpecificClient(nameToSendTo, message,bytesRead);
                        break;
                    case 3:
                        bytesRead += clientStream.Read(message, bytesRead, 4096 - bytesRead);
                        sendToGame(message);
                        break;
                    case 4:
                        pushToClients(message);
                        break;
                    case 6 :
                        pushToClients(message);
                        break;
                    case 7:
                        bytesRead += clientStream.Read(message, bytesRead, 4096 - bytesRead);
                        pushToClients(message);
                        break;





                    case 50:
                       byte[] more = new byte[200];
                        String nameTocheck = "";
                        clientStream.Read(more, 0, 200);
                        nameTocheck = encoder.GetString(more);
                        checkUsername(nameTocheck, tcpClient);
                        return;
                    
                }
                
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
        }





    }

    private void sendToGame(byte[] message)
    {
        game.GetStream().Write(message, 0, message.Length);
        game.GetStream().Flush();
    }

    private void push(TcpClient sender, byte[] input,int length)
    {
        List<TcpClient> list = new List<TcpClient>(clients.Keys);
        input[0] = 9;
        foreach (TcpClient client in list)
        {
            if (client!=(sender) )
            {
                NetworkStream stream = client.GetStream();
                stream.Write(input, 0, length);
                stream.Flush();
                
            }
        }
        game.GetStream().Write(input, 0, input.Length);
        game.GetStream().Flush();
    }

    private void pushToClients(byte[] input)
    {
        input[0] = 8;
        List<TcpClient> list = new List<TcpClient>(clients.Keys);
        foreach (TcpClient client in list)
        {

            NetworkStream stream = client.GetStream();
            stream.Write(input, 0, input.Length);
            stream.Flush();


        }
    }

    private void sendToSpecificClient(String name, byte[] message,int bytestosend)
    {
        message[0] = 30;
        var list = new List<String>(clients.Values);
        int listlocation = -1,counter=0;
        foreach (String s in list)
        {

            if (s.Equals(name))
            {
                listlocation = counter;
                break;
            }
            counter++;
        }
        if (listlocation == -1)
            return;
        var list2 = new List<TcpClient>(clients.Keys);
        Debug.WriteLine(message[0]);
        list2[listlocation].GetStream().Write(message, 0, bytestosend);
        list2[listlocation].GetStream().Flush();
    }

    private void informGameOfNewPlayer(byte[] message)
    {
        ASCIIEncoding encoder = new ASCIIEncoding();
        String s = encoder.GetString(message, 1, message.Length - 1);

        game.GetStream().Write(message, 0, message.Length);
        game.GetStream().Flush();
    }

    public void close()
    {
        foreach(Thread t in threads){
            t.Abort();
        }
        
    }

    private void checkUsername(String s, TcpClient t)
    {
        byte[] info = new byte[1];
        if(clients.ContainsValue(s))
            info[0]=52;
        else
            info[0]=51;
        t.GetStream().Write(info, 0, 1);
        t.GetStream().Flush();
    }

    







  }

    




    }

