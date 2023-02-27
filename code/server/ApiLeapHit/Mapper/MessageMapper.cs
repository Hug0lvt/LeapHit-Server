using DataBase.Entity;
using DTO;

namespace ApiLeapHit.Mapper
{
    public static class MessageMapper
    {
        public static DTOMessage ToDto(this Message message)
        {
            DTOMessage dtoMessage = new DTOMessage()
            {
                messageId = message.messageId,
                message = message.message,
                timestamp = message.timestamp,
                PlayerId = message.player,
                ChatId = message.chat
            };
            return dtoMessage;
        }

        public static Message ToMessage(this DTOMessage dtoMessage)
        {
            Message message = new Message()
            {
                messageId = dtoMessage.messageId,
                message = dtoMessage.message,
                timestamp = dtoMessage.timestamp,
                player = dtoMessage.PlayerId,
                chat = dtoMessage.ChatId
            };
            return message;
        }
    }
}
