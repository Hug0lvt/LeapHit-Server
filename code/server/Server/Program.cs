using System;
using System.Net;
using System.Net.Sockets;


class Program
{

    static int playerCount = 0;
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to LeapHit Multiplayer - Server");
        StartServer();
    }

    static void StartServer()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 3131);
        UdpClient server = new UdpClient(endPoint);
        Console.WriteLine("Server started, waiting for clients to connect...");
        Console.WriteLine(endPoint.Address.ToString());

        while (playerCount < 2)
        {
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = server.Receive(ref clientEndPoint);
            string dataReceived = System.Text.Encoding.ASCII.GetString(data);
            Console.WriteLine("Data received from client: " + dataReceived + " from " + clientEndPoint.ToString());
         
            playerCount++;

            if (playerCount == 2)
            {
                Console.WriteLine("Deux joueurs connectés, le jeu va commencer...");
                // On va mettre le code du pour demarrer le match ici
            }

        }
        server.Close();
    }
}