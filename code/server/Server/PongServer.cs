﻿using System;
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
                if(choisenRoom.Value != default)
                {
                    Join(data, remoteEndPoint, serverSocket, choisenRoom.Value);
                }
               
            }

            if (data.Informations.Action == Shared.DTO.Action.Connect) // Join = rejoindre un room , et Host ça va juste créer un room
            {
                var choisenRoom = rooms.FirstOrDefault(room => room.Value.Availaible);
                Console.WriteLine("Connection " + choisenRoom.Key);
                if (choisenRoom.Value != default )
                {
                   Join(data, remoteEndPoint, serverSocket, choisenRoom.Value);
                   
                }
                else
                {
                    Host(data, remoteEndPoint, serverSocket, true);
                }

            }
            Console.WriteLine("");

        }

    }

    private void Host(ObjectTransfert<Player> data, IPEndPoint remoteEndPoint, UdpClient serverSocket, bool availaible)
    {
        var choisenRoom = rooms.FirstOrDefault(room => room.Value.Free);
        var chosenPort = nextPort;
        if (choisenRoom.Value != default)
        {
            chosenPort= choisenRoom.Value.Port;
            rooms.Remove(choisenRoom.Key);
            choisenRoom = default;
        }

        Room room = new Room(data.Data.playerId, availaible);


        // Assign a unique port to the client
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, chosenPort);
        UdpClient clientSocket = new UdpClient(clientEndPoint);

        room.playerHost = new KeyValuePair<Player,UdpClient>(data.Data,clientSocket);
        room.NbPlayer++;

        

        Console.WriteLine("New connection Host From " + remoteEndPoint.ToString());

       
        room.Port = chosenPort;
        if(chosenPort==nextPort)
        {
            nextPort++;
        }
       


        Tuple<int, bool> dataToSend = new Tuple<int, bool>(room.Port, true);
        Console.WriteLine(JsonSerializer.Serialize(dataToSend));

        // Send port message to client
        byte[] connectionData = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(dataToSend));
        serverSocket.Send(connectionData, connectionData.Length, remoteEndPoint);

        rooms[data.Data.playerId] = room;
        

        Console.WriteLine("FIN HOST...............");

    }

    private void Join(ObjectTransfert<Player> data, IPEndPoint remoteEndPoint, UdpClient serverSocket, Room room)
    {

        room.playerJoin = new KeyValuePair<Player, UdpClient>(data.Data, room.playerHost.Value);
        

        Console.WriteLine("New connection Client from " + remoteEndPoint.ToString());

        Tuple<int, bool> dataToSend = new Tuple<int, bool>(room.Port, false);
        Console.WriteLine(JsonSerializer.Serialize(dataToSend));

        // Send port message to client
        byte[] connectionData = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(dataToSend));
        serverSocket.Send(connectionData, connectionData.Length, remoteEndPoint);
        
        room.PropertyChanged += room.OnReadyChanged;
        room.NbPlayer++;

        Console.WriteLine("FIN JOIN...............");
    }

}