
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Entity
{
    public class Message
    {
        public int messageId { get; set; }
        public string message { get; set; }
        public DateTime timestamp { get; set; }
        public string player { get; set; }
        public int chat { get; set; }

        [ForeignKey("player")]
        public Player PlayerId { get; set; }

        [ForeignKey("chat")]
        public Chat ChatId { get; set; }
    }
}
