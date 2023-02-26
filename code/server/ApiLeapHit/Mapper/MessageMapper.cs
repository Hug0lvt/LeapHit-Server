using DataBase.Entity;
using DTO;

namespace ApiLeapHit.Mapper
{
    public static class MessageMapper
    {
        public static DTOMessage ToDto(this Message message, Player player,  Chat chat)
        {
            DTOMessage dtoMessage = new DTOMessage()
            {
                messageId = message.messageId,
                message = message.message,
                timestamp = message.timestamp,
                PlayerId = player.ToDto(),
                //ChatId = chat.ToDto()
            };
            return dtoMessage;
        }
    }
}
