using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using DTO.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLeapHit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {

        private readonly DbDataManager _dataManager;
        private readonly ILogger<MessageController> _logger;

        public MessageController(DbDataManager dataManager, ILogger<MessageController> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] DTOMessage dtoMessage)
        {
            try
            {
                var player = await _dataManager.GetPlayer(dtoMessage.PlayerId.playerId);
                if (player == null)
                {
                    _logger.LogWarning($"Le joueur avec l'identifiant {dtoMessage.PlayerId.playerId} n'existe pas.");
                    return NotFound(new ApiResponse<object>($"Le joueur avec l'identifiant {dtoMessage.PlayerId.playerId} n'existe pas."));
                }

                var message = new Message
                {
                    messageId = dtoMessage.messageId,
                    message = dtoMessage.message,
                    timestamp = dtoMessage.timestamp,
                    player = player.playerId
                };

                await _dataManager.SendMessage(message);

                _logger.LogInformation($"Le message avec l'identifiant {message.messageId} a été envoyé avec succès.");
                return Ok(new ApiResponse<object>($"Le message avec l'identifiant {message.messageId} a été envoyé avec succès."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors de l'envoi du message : {ex.Message}");
                return StatusCode(500, new ApiResponse<object>($"Une erreur est survenue lors de l'envoi du message : {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveMessage(int id)
        {
            try
            {
                var result = await _dataManager.RemoveMessage(id);
                if (result)
                {
                    _logger.LogInformation($"Le message avec l'identifiant {id} a été supprimé avec succès.");
                    return Ok(new ApiResponse<object>($"Le message avec l'identifiant {id} a été supprimé avec succès."));
                }
                else
                {
                    _logger.LogWarning($"Le message avec l'identifiant {id} n'existe pas.");
                    return NotFound(new ApiResponse<object>($"Le message avec l'identifiant {id} n'existe pas."));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Une erreur est survenue lors de la suppression du message avec l'identifiant {id} : {ex.Message}");
                return StatusCode(500, new ApiResponse<object>($"Une erreur est survenue lors de la suppression du message avec l'identifiant {id} : {ex.Message}"));
            }
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
            var chat = await _dataManager.GetChat(message.chat);



            return Ok(message);
        }

    }
}
