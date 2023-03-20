
using System.ComponentModel.DataAnnotations;

namespace DataBase.Entity
{
    public class Player
    {
        public string playerId { get; set; }
        public string name { get; set; }
        public int nbBallTouchTotal { get; set; }
        public int timePlayed { get; set; }
    }
}
