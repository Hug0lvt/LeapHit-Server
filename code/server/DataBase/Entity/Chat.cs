
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Entity
{
    public class Chat
    {
        public int chatId { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }

        [ForeignKey("player1")]
        public Player PlayerId1 { get; set; }

        [ForeignKey("player2")]
        public Player PlayerId2 { get; set; }
    }
}
