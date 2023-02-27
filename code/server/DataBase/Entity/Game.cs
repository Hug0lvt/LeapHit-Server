
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Entity
{
    public class Game
    {
        public int gameId { get; set; }
        public int durationGame { get; set; }
        public int nbMaxEchanges { get; set; }
        public int winner { get; set; }
        public int loser { get; set; }

        public int scoreWinner { get; set; }

        public int scoreLoser { get; set; }

        [ForeignKey("winner")]
        public Player PlayerWinner { get; set; }

        [ForeignKey("loser")]
        public Player PlayerLoser { get; set; }
    }
}
