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
using static System.Formats.Asn1.AsnWriter;

namespace Server
{
    public class Room : INotifyPropertyChanged
    {

        public Room(string id, bool availaible)
        {
            Id = id;
            Availaible = availaible;
        }
        Tuple<int, int> ScoreImp = new(0, 0);

        ObjectTransfert<Tuple<GameEntities, Tuple<int, int>>> data ;

        public bool Availaible { get; set; }

        public bool Free { get; set; }=false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Id { get; set; }

        public KeyValuePair <Player, UdpClient> playerHost;
        public KeyValuePair <Player, UdpClient> playerJoin;
        public int Port { get; set; }
        public bool gameRunning=true ;


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



        public void ReceiveMessages(UdpClient clientSocket1, UdpClient clientSocket2, IPEndPoint endpoint2, Semaphore semaphore,bool isHost)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Thread secondsCount = new Thread(() => CountSeconds() );
            bool isDisconected=false;
            secondsCount.Start();

            while ((ScoreImp.Item1<6 && ScoreImp.Item2 < 6) && gameRunning)
            {
                try
                {
                    byte[] receivedData = clientSocket1.Receive(ref remoteEndPoint);
                    if (isHost) {
                        string fileJson = Encoding.UTF8.GetString(receivedData);
                        try
                        {
                            data = JsonSerializer.Deserialize<ObjectTransfert<Tuple<GameEntities, Tuple<int, int>>>>(fileJson);
                            ScoreImp = data.Data.Item2 != null ? data.Data.Item2 : ScoreImp;
                        }
                        catch (Exception ex) { }
                  
                    }
                    semaphore.WaitOne();
                    clientSocket2.Send(receivedData, receivedData.Length, endpoint2);
                    semaphore.Release();

                } catch (SocketException){
                    semaphore.Release();
                    isDisconected = true;
                    break;
                }
            }
            //Tuple<GameEntities, Tuple<int, int>> game = new Tuple<GameEntities, Tuple<int, int>>(new GameEntities(new Tuple<float,float>(0,0),0), ScoreImp);
            /* ObjectTransfert<Tuple<GameEntities, Tuple<int, int>>> data2 = new ObjectTransfert<Tuple<GameEntities, Tuple<int, int>>>() {
                 Informations = new Informations(Shared.DTO.Action.End, 0, typeof(Tuple<GameEntities, Tuple<int, int>>).ToString()),
                 Data = game
             };*/
            if (!isDisconected)
            {
                data.Informations.Action = Shared.DTO.Action.End;
                string sendFinish = JsonSerializer.Serialize(data);
                byte[] sendFinishByt = Encoding.ASCII.GetBytes(sendFinish);
                try
                {
                    clientSocket2.Send(sendFinishByt, sendFinishByt.Length, endpoint2);
                }
                catch (Exception ex) { }
            }
            if (isHost)
            {
                playerJoin.Value.Close();
            }
            else
            {
                playerHost.Value.Close();
            }
            Free = true;
            Console.WriteLine("Game Finished Am i host " + isHost);
        }

        private void CountSeconds()
        {
            int seconds = 0;
            while (seconds<=122)
            {
                seconds++;
                Thread.Sleep(1000);
            }
            gameRunning = false;
        }

        async public void OnReadyChanged(object sender, PropertyChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                Room room = sender as Room;
                int maxPlayer = room.nbPlayer;


                if (maxPlayer == 2)
                {
                    room.Availaible = false;
                    IPEndPoint remoteEndPointHost = new IPEndPoint(IPAddress.Any, room.Port);
                    IPEndPoint remoteEndPointJoin = new IPEndPoint(IPAddress.Any, room.Port);


                    byte[] receivedDataHost = playerHost.Value.Receive(ref remoteEndPointHost);
                    byte[] receivedDataJoin = playerJoin.Value.Receive(ref remoteEndPointJoin);

                    playerJoin.Value.Send(receivedDataHost, receivedDataHost.Length, remoteEndPointHost);
                    playerHost.Value.Send(receivedDataJoin, receivedDataJoin.Length, remoteEndPointJoin);

                    Semaphore semaphore = new Semaphore(2, 2);


                    Thread receiveThread1 = new Thread(() => ReceiveMessages(playerHost.Value, playerJoin.Value, remoteEndPointJoin, semaphore,true));


                    Thread receiveThread2 = new Thread(() =>
                    {
                        ReceiveMessages(playerJoin.Value, playerHost.Value, remoteEndPointHost, semaphore,false);
                    });
                    receiveThread1.Start();
                    receiveThread2.Start();


                    receiveThread1.Join();
                    receiveThread2.Join();
                }

            });
            
        }
            

    }
}
