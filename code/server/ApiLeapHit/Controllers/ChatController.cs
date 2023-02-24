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
