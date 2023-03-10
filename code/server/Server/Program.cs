using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static Dictionary<IPEndPoint, UdpClient> clients = new Dictionary<IPEndPoint, UdpClient>();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to LeapHit Multiplayer - Server");
        StartServer();
    }

    static void StartServer()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 3131);
        UdpClient server = new UdpClient(endPoint);
        Thread receiveThread = new Thread(ServerReceiveMessages);
        receiveThread.Start(server);
        Console.WriteLine("Server started, waiting for clients to connect...");
        Console.WriteLine(endPoint.Address.ToString());

        // Stop Server
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
        receiveThread.Abort();
        foreach (UdpClient clientSocket in clients.Values)
        {
            clientSocket.Close();
        }
        server.Close();
    }

    static void ServerReceiveMessages(object obj)
    {
        UdpClient serverSocket = (UdpClient)obj;

        while (true)
        {
            IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = serverSocket.Receive(ref remoteEndpoint);

            if (!clients.ContainsKey(remoteEndpoint))
            {
                clients[remoteEndpoint] = new UdpClient();
                clients[remoteEndpoint].Client.Bind(new IPEndPoint(IPAddress.Any, 31323));
            }

            string connectionMessage = "Connection established.";
            byte[] connectionData = System.Text.Encoding.ASCII.GetBytes(connectionMessage);
            clients[remoteEndpoint].Send(connectionData, connectionData.Length, remoteEndpoint.Address.ToString(), remoteEndpoint.Port);

            // Receive data on the connection socket
            while (true)
            {
                byte[] receivedData = clients[remoteEndpoint].Receive(ref remoteEndpoint);
                string receivedMessage = System.Text.Encoding.ASCII.GetString(receivedData);
                Console.WriteLine("Received from " + remoteEndpoint + ": " + receivedMessage);
            }
        }
    }










}




