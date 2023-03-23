using DataBase.Entity;
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



        public void ReceiveMessages(UdpClient clientSocket1, UdpClient clientSocket2)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                byte[] receivedData = clientSocket1.Receive(ref remoteEndPoint);

                clientSocket2.Send(receivedData, receivedData.Length, remoteEndPoint);

            }
        }

        public void OnReadyChanged(object sender, PropertyChangedEventArgs e)
        {

            Room nbPlayer = sender as Room;
            int maxPlayer = nbPlayer.nbPlayer;

            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, Port);
            //UdpClient serverSocket = new UdpClient(serverEndPoint);


            if (maxPlayer == 2)
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                byte[] receivedDataHost = playerHost.Value.Receive(ref remoteEndPoint);
                playerJoin.Value.Send(receivedDataHost, receivedDataHost.Length, remoteEndPoint);

                byte[] receivedDataJoin = playerJoin.Value.Receive(ref remoteEndPoint);
                playerHost.Value.Send(receivedDataJoin, receivedDataJoin.Length, remoteEndPoint);

                Thread receiveThread1 = new Thread(() => ReceiveMessages(playerHost.Value, playerJoin.Value));


                Thread receiveThread2 = new Thread(() => ReceiveMessages(playerJoin.Value, playerHost.Value));

                receiveThread1.Start();
                receiveThread2.Start();

                receiveThread1.Join();
                receiveThread2.Join();

                
            }

        }

    }
}
