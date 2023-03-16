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
    public class MessagesController : Controller
    {

        private readonly DbDataManager _dataManager;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(DbDataManager dataManager, ILogger<MessagesController> logger)
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
                    var message = $"Le joueur avec l'identifiant {dtoMessage.PlayerId} n'existe pas.";
                    _logger.LogWarning(message);
                    return StatusCode((int)HttpStatusCode.NotFound, new ApiResponse(message));
                }

                await _dataManager.SendMessage(dtoMessage.ToMessage());

                var message_success = $"Le message avec l'identifiant {dtoMessage.messageId} a été envoyé avec succès.";
                _logger.LogInformation(message_success);
                return StatusCode((int)HttpStatusCode.Created, new ApiResponse(message_success));
            }
            catch (Exception ex)
            {
                var erroe_message = "Une erreur est survenue lors de l'envoi du message.";
                _logger.LogError(ex,erroe_message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse(erroe_message));
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
                    var message_success = $"Le message avec l'identifiant {id} a été supprimé avec succès.";
                    _logger.LogInformation(message_success);
                    return StatusCode((int)HttpStatusCode.OK, new ApiResponse(message_success));
                }
                else
                {
                    var message = $"Le message avec l'identifiant {id} n'existe pas.";
                    _logger.LogInformation(message);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse(message));
                }
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la suppression du message avec l'identifiant {id}.";
                _logger.LogError(ex, error_message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse(error_message));
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
                    var message_notFound = $"Aucun message avec l'idée {id} n'a été trouvé.";
                    _logger.LogInformation(message_notFound);
                    return StatusCode((int)HttpStatusCode.NotFound, new ApiResponse(message_notFound));
                }

                var message_success = $"Le message avec l'identifiant {id} a été reçu avec succès.";
                _logger.LogInformation(message_success);
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse<DTOMessage>(message_success, message.ToDto()));
            }
            catch (Exception ex)
            {
                var message_error = $"Une erreur est survenue lors de la récupération du message avec l'id {id}.";
                _logger.LogError(ex, message_error);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse(message_error));
            }
        }

        //[HttpGet]
        //public async Task<ActionResult<DTOMessage>> ReceiveAllMessages()
        //{
        //    try
        //    {
        //        var messages = await _dataManager.ReceiveAllMessages();

        //        if (messages == null || messages.Count() == 0)
        //        {
        //            _logger.LogWarning($"Aucun message n'a été trouvé.");
        //            return NotFound(new ApiResponse("Aucun message n'a pas été trouvé."));
        //        }

        //        var dtosMessages = messages.Select(message => message.ToDto()).ToList();

        //        _logger.LogInformation($"Les messages ont été reçus avec succès.");
        //        return Ok(new ApiResponse<List<DTOMessage>>("Messages reçus avec succès.", dtosMessages));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Une erreur est survenue lors de la récupération des messages.");
        //        return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse($"Une erreur est survenue lors de la récupération des messages. : {ex.Message}"));
        //    }
        //}

    }
}
