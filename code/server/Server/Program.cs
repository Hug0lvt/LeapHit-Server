using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;


class Program
{

    static int playerCount = 0;
    static List<IPEndPoint> clientAddresses = new List<IPEndPoint>(); // Liste des adresses IP des clients connectés
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

            if (!clientAddresses.Contains(clientEndPoint)) // Vérification si l'adresse IP est déjà présente dans la liste
            {
                clientAddresses.Add(clientEndPoint); // Ajout de l'adresse IP à la liste
                playerCount++;

                if (playerCount == 2)
                {
                    Console.WriteLine("Deux joueurs connectés, le jeu va commencer...");
                    // On va mettre le code du pour demarrer le match ici
                }
            }
            else
            {
                Console.WriteLine("Client with IP " + clientEndPoint.Address.ToString() + " has already sent a message and will not be counted.");
            }

        }
        server.Close();
    }
}