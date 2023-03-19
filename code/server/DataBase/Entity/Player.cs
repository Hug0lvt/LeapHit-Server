
using System.ComponentModel.DataAnnotations;

namespace DataBase.Entity
{
    public class Player
    {
        public string playerId { get; set; }
        public string name { get; set; }
        public int nbBallTouchTotal { get; set; }
        public bool ready { get; set; } = false;
        public int timePlayed { get; set; }
    }
}
