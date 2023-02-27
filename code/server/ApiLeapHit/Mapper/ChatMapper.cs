using DataBase.Entity;
using DTO;

namespace ApiLeapHit.Mapper
{
    public static class ChatMapper
    {
        public static DTOChat ToDto(this Chat chat)
        {
            DTOChat dtoChat = new DTOChat()
            {
                chatId = chat.chatId,
                PlayerId1 = chat.player1,
                PlayerId2 = chat.player2
            };
            return dtoChat;
        }

        public static Chat ToChat(this DTOChat dtoChat)
        {
            return new Chat
            {
                chatId = dtoChat.chatId,
                player1 = dtoChat.PlayerId1,
                player2 = dtoChat.PlayerId2
            };
        }
    }
}
