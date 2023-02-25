using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLeapHit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {

        private readonly DbDataManager _dataManager;


        public ChatController(DbDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddChat([FromBody] DTOChat dtoChat)
        {
            var player1 = await _dataManager.GetPlayer(dtoChat.PlayerId2.playerId);
            var player2 = await _dataManager.GetPlayer(dtoChat.PlayerId2.playerId);

            var chat = new Chat
            {
                chatId = dtoChat.chatId,
                player1 = player1.playerId,
                player2 = player2.playerId
            };

            await _dataManager.AddChat(chat);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOChat>>> GetChats()
        {
            var chats = await _dataManager.GetChats();
            if (chats == null)
            {
                return NotFound();
            }

            var dtoChats = new List<DTOChat>();
            foreach (var chat in chats)
            {
                var player1 = await _dataManager.GetPlayer(chat.player1);
                var player2 = await _dataManager.GetPlayer(chat.player2);

                var dtoChat = new DTOChat
                {
                    chatId = chat.chatId,
                    PlayerId1 = new DTOPlayer { playerId = player1.playerId, name = player1.name },
                    PlayerId2 = new DTOPlayer { playerId = player2.playerId, name = player2.name }
                };

                dtoChats.Add(dtoChat);
            }

            return Ok(dtoChats);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveChat(int id)
        {
            var result = await _dataManager.RemoveChat(id);
            if (result)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
