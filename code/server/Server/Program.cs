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
    static void Main(string[] args)
    {
        PongServer server = new PongServer();
        Console.WriteLine("Welcome to LeapHit Multiplayer - Server");
        server.StartServer();
    }

}