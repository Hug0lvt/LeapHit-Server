
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Entity
{
    public class Chat
    {
        public int chatId { get; set; }
        public int sender { get; set; }
        public int recipient { get; set; }

        [ForeignKey("sender")]
        public Player PlayerSender { get; set; }

        [ForeignKey("recipient")]
        public Player PlayerRecipient { get; set; }
    }
}
