using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLeapHit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly DbDataManager _dataManager;


        public MessageController(DbDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOMessage>> ReceiveMessage(int id)
        {
            var message = await _dataManager.ReceiveMessage(id);
            if (message == null)
            {
                return NotFound();
            }

            var player = await _dataManager.GetPlayer(message.player);


            var dtoMessage = new DTOMessage
            {
                messageId = message.messageId,
                message = message.message,
                timestamp = message.timestamp,
                PlayerId = new DTOPlayer
                {
                    playerId = player.playerId,
                    name = player.name,
                    nbBallTouchTotal = player.nbBallTouchTotal,
                    timePlayed = player.timePlayed
                }
            };
            return Ok(dtoMessage);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] DTOMessage dtoMessage)
        {
            var player = await _dataManager.GetPlayer(dtoMessage.PlayerId.playerId);

            var message = new Message
            {
                messageId = dtoMessage.messageId,
                message = dtoMessage.message,
                timestamp = dtoMessage.timestamp,
                player = player.playerId
            };

            await _dataManager.SendMessage(message);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveMessage(int id)
        {
            var result = await _dataManager.RemoveMessage(id);
            if (result)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
