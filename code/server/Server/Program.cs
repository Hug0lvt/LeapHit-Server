using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static Dictionary<IPEndPoint, UdpClient> clients = new Dictionary<IPEndPoint, UdpClient>();
    static int nextPort = 3132;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to LeapHit Multiplayer - Server");
        StartServer();
    }

    static void StartServer()
    {
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 3131);
        UdpClient serverSocket = new UdpClient(serverEndPoint);
        Console.WriteLine("Server started, waiting for clients to connect...");

        while (true)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedData = serverSocket.Receive(ref remoteEndPoint);
            string message = Encoding.ASCII.GetString(receivedData);

            if (message == "Connect")
            {
                Console.WriteLine("New connection from " + remoteEndPoint.ToString());

                // Assign a unique port to the client
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, nextPort++); ;
                UdpClient clientSocket = new UdpClient(clientEndPoint);
                clientSocket.Client.Bind(clientEndPoint);
                clients[remoteEndPoint] = clientSocket;

                // Send connection message to client
                string connectionMessage = "Connection established on port " + clientEndPoint.Port.ToString();
                byte[] connectionData = Encoding.ASCII.GetBytes(connectionMessage);
                serverSocket.Send(connectionData, connectionData.Length, remoteEndPoint);

                // Start thread to receive data from client
                Thread receiveThread = new Thread(ReceiveMessages);
                receiveThread.Start(clientSocket);
            }
        }
    }

    static void ReceiveMessages(object obj)
    {
        UdpClient clientSocket = (UdpClient)obj;
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        while (true)
        {
            byte[] receivedData = clientSocket.Receive(ref remoteEndPoint);
            string receivedMessage = Encoding.ASCII.GetString(receivedData);
            Console.WriteLine("Received from " + remoteEndPoint.ToString() + ": " + receivedMessage);
        }
    }
}