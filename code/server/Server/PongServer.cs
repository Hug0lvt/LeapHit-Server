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

            if (data.Informations.Action == Shared.DTO.Action.Create)
            {
                Room room = new Room(data.Data.playerId);
                room.playerHost = data.Data;
                room.nbPlayer++;
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
                Thread receiveThread = new Thread(() => Room.ReceiveMessages(clientSocket, data.Data));
                receiveThread.Start();

            }

            if (data.Informations.Action == Shared.DTO.Action.Join)
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
                Thread receiveThread = new Thread(() => ReceiveMessages(clientSocket));
                receiveThread.Start();

            }

            if (room.MaxPlayers.Count == 2)
            {
                Console.WriteLine("Starting game...");
                // Call a function to start the game
            }
        }
    }

    
}