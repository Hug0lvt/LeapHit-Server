﻿using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;
using System.ComponentModel;
using System.Text.Json;

namespace Server
{
    public class Room : INotifyPropertyChanged
    {

        public Room(string id, bool availaible)
        {
            Id = id;
            Availaible = availaible;
        }

        public bool Availaible { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Id { get; set; }

        public KeyValuePair <Player, UdpClient> playerHost;
        public KeyValuePair <Player, UdpClient> playerJoin;
        public int Port { get; set; }


        public int NbPlayer
        {
            get
            {
                return nbPlayer;
            }
            set
            {
                nbPlayer = value;
                if (value >= 2)
                {
                    NotifyPropertyChanged("nbPlayer");
                }
            }
        }
        private int nbPlayer;

        
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void ReceiveMessages(UdpClient clientSocket1, UdpClient clientSocket2, IPEndPoint endpoint2)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                byte[] receivedData = clientSocket1.Receive(ref remoteEndPoint);

                clientSocket2.Send(receivedData, receivedData.Length, endpoint2);

                //Thread.Sleep(50);

            }
        }

        public void OnReadyChanged(object sender, PropertyChangedEventArgs e)
        {

            Room room = sender as Room;
            int maxPlayer = room.nbPlayer;

            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, Port);
            //UdpClient serverSocket = new UdpClient(serverEndPoint);


            if (maxPlayer == 2)
            {
                IPEndPoint remoteEndPointHost = new IPEndPoint(IPAddress.Any, room.Port);
                IPEndPoint remoteEndPointJoin = new IPEndPoint(IPAddress.Any, room.Port);


                byte[] receivedDataHost = playerHost.Value.Receive(ref remoteEndPointHost);
                byte[] receivedDataJoin = playerJoin.Value.Receive(ref remoteEndPointJoin);
                
                playerJoin.Value.Send(receivedDataHost, receivedDataHost.Length, remoteEndPointHost);
                playerHost.Value.Send(receivedDataJoin, receivedDataJoin.Length, remoteEndPointJoin);

                Thread receiveThread1 = new Thread(() => ReceiveMessages(playerHost.Value, playerJoin.Value, remoteEndPointJoin));


                Thread receiveThread2 = new Thread(() => ReceiveMessages(playerJoin.Value, playerHost.Value, remoteEndPointHost));

                receiveThread1.Start();
                receiveThread2.Start();

                receiveThread1.Join();
                receiveThread2.Join();

                
            }

        }

    }
}
