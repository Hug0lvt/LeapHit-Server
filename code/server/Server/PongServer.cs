using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DataBase.Entity;
using Shared.DTO;
using System.Text.Json;
using Server;
using System.ComponentModel;

public class PongServer
{
    //sert juste à stocker les connexions pour l'instant
    static Dictionary<IPEndPoint, UdpClient> clients = new Dictionary<IPEndPoint, UdpClient>();
    Dictionary<string, Room> rooms = new Dictionary<string, Room>();

    int nextPort;
    int listenPort;

    public PongServer(int port = 3131)
    {
        nextPort = port + 1;
        listenPort = port;
    }

    public void StartServer()
    {

        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
        UdpClient serverSocket = new UdpClient(serverEndPoint);
        Console.WriteLine("Server started, waiting for clients to connect...");

        while (true)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedData = serverSocket.Receive(ref remoteEndPoint);
            string fileJson = Encoding.UTF8.GetString(receivedData);
            ObjectTransfert<Player> data = JsonSerializer.Deserialize<ObjectTransfert<Player>>(fileJson);

            if (data.Informations.Action == Shared.DTO.Action.Host)
            {
                Host(data, remoteEndPoint, serverSocket, false);

            }

            if (data.Informations.Action == Shared.DTO.Action.Join)
            {
                var choisenRoom = rooms.FirstOrDefault(room => room.Key == data.Informations.IdRoom);
                if(choisenRoom.Value != default && choisenRoom.Value.Availaible)
                {
                    Join(data, remoteEndPoint, serverSocket, choisenRoom.Value);
                }
               
            }

            if (data.Informations.Action == Shared.DTO.Action.Connect) // Join = rejoindre un room , et Host ça va juste créer un room
            {
                var choisenRoom = rooms.FirstOrDefault(room => room.Value.Availaible);
                if (choisenRoom.Value != default )
                {
                    Join(data, remoteEndPoint, serverSocket, choisenRoom.Value);
                }
                else
                {
                    Host(data, remoteEndPoint, serverSocket, true);
                }

            }

        }

    }

    private void Host(ObjectTransfert<Player> data, IPEndPoint remoteEndPoint, UdpClient serverSocket, bool availaible)
    {
        Room room = new Room(data.Data.playerId, availaible);
        room.playerHost = data.Data;
        room.nbPlayer++;

        room.PropertyChanged += OnReadyChanged;

        Console.WriteLine("New connection from " + remoteEndPoint.ToString());

        // Assign a unique port to the client
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, nextPort);
        room.Port = nextPort;
        nextPort++;
        UdpClient clientSocket = new UdpClient(clientEndPoint);
        clients[remoteEndPoint] = clientSocket;

        // Send connection message to client             
        byte[] connectionData = Encoding.ASCII.GetBytes(room.Id);
        serverSocket.Send(connectionData, connectionData.Length, remoteEndPoint);

        // Start thread to receive data from client
        Thread receiveThread = new Thread(() => room.ReceiveMessages(clientSocket, data.Data));
        receiveThread.Start();
    }

    private void Join(ObjectTransfert<Player> data, IPEndPoint remoteEndPoint, UdpClient serverSocket, Room room)
    {

        Console.WriteLine("New connection from " + remoteEndPoint.ToString());

        // Assign a unique port to the client
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, nextPort++);
        UdpClient clientSocket = new UdpClient(clientEndPoint);
        clients[remoteEndPoint] = clientSocket;

        // Send connection message to client
        string connectionMessage = clientEndPoint.Port.ToString();
        byte[] connectionData = Encoding.ASCII.GetBytes(connectionMessage);
        serverSocket.Send(connectionData, connectionData.Length, remoteEndPoint);

        // Start thread to receive data from client
        Thread receiveThread = new Thread(() => room.ReceiveMessages(clientSocket, data.Data));
        receiveThread.Start();
    }

    private void OnReadyChanged(object sender, PropertyChangedEventArgs e)
    {
       
        Room nbPlayer = sender as Room;
        bool maxPlayer = nbPlayer.maxPlayer;

        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
        UdpClient serverSocket = new UdpClient(serverEndPoint);

        if (maxPlayer)
        {
            while (true)
            {
                //Faut finir ça mnt
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] receivedData = serverSocket.Receive(ref remoteEndPoint);


            }
        }
        
    }


}