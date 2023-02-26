using DataBase.Entity;
using DTO;

namespace ApiLeapHit.Mapper
{
    public static class ChatMapper
    {
        public static DTOChat ToDto(this Chat chat, Player player1, Player player2)
        {
            DTOChat dtoChat = new DTOChat()
            {
                chatId = chat.chatId,
                PlayerId1 = player1.ToDto(),
                PlayerId2 = player2.ToDto()
            };
            return dtoChat;
        }

        public static Chat ToChat(this DTOChat dtoChat, Player player1, Player player2)
        {
            return new Chat
            {
                chatId = dtoChat.chatId,
                player1 = player1.playerId,
                player2 = player2.playerId
            };
        }
    }
}
