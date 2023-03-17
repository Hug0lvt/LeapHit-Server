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

class Program
{
    //sert juste à stocker les connexions pour l'instant
    static Dictionary<IPEndPoint, UdpClient> clients = new Dictionary<IPEndPoint, UdpClient>();
    static int nextPort = 3132;

    static void Main(string[] args)
    {
        PongServer server = new PongServer();
        Console.WriteLine("Welcome to LeapHit Multiplayer - Server");
        server.StartServer();
    }

}