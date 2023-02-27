using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOMessage
    {
        public int messageId { get; set; }
        public string message { get; set; }
        public DateTime timestamp { get; set; }
        public int PlayerId { get; set; }
        public int ChatId { get; set; }
    }
}
