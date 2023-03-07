using ApiLeapHit.Mapper;
using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using DTO.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                var player = await _dataManager.GetPlayer(dtoMessage.PlayerId);
                if (player == null)
                {
                    _logger.LogWarning($"Le joueur avec l'identifiant {dtoMessage.PlayerId} n'existe pas.");
                    return NotFound(new ApiResponse<object>($"Le joueur avec l'identifiant {dtoMessage.PlayerId} n'existe pas."));
                }

                await _dataManager.SendMessage(dtoMessage.ToMessage());

                _logger.LogInformation($"Le message avec l'identifiant {dtoMessage.messageId} a été envoyé avec succès.");
                return Ok(new ApiResponse<object>($"Le message avec l'identifiant {dtoMessage.messageId} a été envoyé avec succès."));
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
            try
            {
                var message = await _dataManager.ReceiveMessage(id);

                if (message == null)
                {
                    _logger.LogWarning($"Aucun message avec l'idée {id} n'a été trouvé.");
                    return NotFound(new ApiResponse<object>("Le message n'a pas été trouvé."));
                }

                var response = new ApiResponse<DTOMessage>("Joueur ajouté avec succès.");
                //response.Links.Add(new ApiLink(
                //    Url.Action("GetPlayer", "Player", new { id = player.playerId }),
                //    "self",
                //    "GET"
                //));

                _logger.LogInformation($"Le message avec l'identifiant {id} a été reçu avec succès.");
                return Ok(new ApiResponse<DTOMessage>("Message reçu avec succès.", message.ToDto()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la récupération du message avec l'id {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>($"Une erreur est survenue lors de la récupération du message. : {ex.Message}"));
            }
        }

        [HttpGet]
        public async Task<ActionResult<DTOMessage>> ReceiveAllMessages()
        {
            try
            {
                var messages = await _dataManager.ReceiveAllMessages();

                if (messages == null || messages.Count() == 0)
                {
                    _logger.LogWarning($"Aucun message n'a été trouvé.");
                    return NotFound(new ApiResponse<object>("Aucun message n'a pas été trouvé."));
                }

                var dtosMessages = messages.Select(message => message.ToDto()).ToList();

                _logger.LogInformation($"Les messages ont été reçus avec succès.");
                return Ok(new ApiResponse<List<DTOMessage>>("Messages reçus avec succès.", dtosMessages));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la récupération des messages.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>($"Une erreur est survenue lors de la récupération des messages. : {ex.Message}"));
            }
        }

    }
}
