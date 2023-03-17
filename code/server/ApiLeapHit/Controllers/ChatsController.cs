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
    public class ChatsController : Controller
    {

        private readonly DbDataManager _dataManager;
        private readonly ILogger<ChatsController> _logger;

        public ChatsController(DbDataManager dataManager, ILogger<ChatsController> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddChat([FromBody] DTOChat dtoChat)
        {
            try
            {
                var player1 = await _dataManager.GetPlayer(dtoChat.PlayerId1);
                var player2 = await _dataManager.GetPlayer(dtoChat.PlayerId2);
                if (player1 == null || player2 == null)
                {
                    var message = "Les ids de player ne sont pas valides.";
                    _logger.LogInformation(message);
                    return BadRequest(message);
                }

                await _dataManager.AddChat(dtoChat.ToChat());

                var success_message = $"Le chat entre {player1.name} et {player2.name} a été ajouté avec succès.";
                _logger.LogInformation(success_message);
                return Ok(success_message);
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de l'ajout du chat.";
                _logger.LogError(ex, error_message);
                return StatusCode((int)HttpStatusCode.InternalServerError, error_message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOChat>>> GetChats()
        {
            try
            {
                var chats = await _dataManager.GetChats();
                if (chats == null)
                {
                    var message = "Aucun chat n'a été trouvé.";
                    _logger.LogInformation(message);
                    return NotFound(message);
                }

                var dtoChats = chats.Select(c => c.ToDto());

                var success_message = $"La récupération des chats a réussi. Nombre de chats : {dtoChats.Count()}";
                _logger.LogInformation(success_message);
                return Ok(dtoChats);
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la récupération des chats.";
                _logger.LogError(ex, error_message);
                return StatusCode((int)HttpStatusCode.InternalServerError, error_message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOChat>> GetChatById(int id)
        {
            try
            {
                var chat = await _dataManager.GetChat(id);
                if (chat == null)
                {
                    var message = "Aucun chat n'a été trouvé.";
                    _logger.LogInformation(message);
                    return NotFound(message);
                }

                var dtoChat = chat.ToDto();

                var success_message = $"La récupération du chat a réussi pour le chat {id}.";
                _logger.LogInformation(success_message);
                return Ok(dtoChat);
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la récupération du chat {id}.";
                _logger.LogError(ex, error_message);
                return StatusCode((int)HttpStatusCode.InternalServerError, error_message);
            }
        }


        [HttpGet("Player/{id}")]
        public async Task<ActionResult<List<DTOChat>>> GetChatsByIdPlayer(string id)
        {
            try
            {
                var chats = await _dataManager.GetChatsByIdPlayer(id);
                if (chats == null || !chats.Any())
                {
                    var message = $"Aucun chat n'a été trouvé pour l'id : {id}.";
                    _logger.LogInformation(message);
                    return NotFound(new ApiResponse(message));
                }

                var dtoChats = chats.Select(c => c.ToDto()).ToList();
                var success_message = $"La récupération des chats a réussi pour l'id : {id}. Nombre de chats : {dtoChats.Count}";
                _logger.LogInformation(success_message);
                return Ok(new ApiResponse<List<DTOChat>>(success_message, dtoChats));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la récupération des chats de l'utilisateur {id}.";
                _logger.LogError(ex, error_message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse(error_message));
            }

        }

        [HttpGet("Players/{idPlayer1}/{idPlayer2}")]
        public async Task<ActionResult<List<DTOChat>>> GetChatsByIdPlayers(string idPlayer1, string idPlayer2)
        {
            try
            {
                var chats = await _dataManager.GetChatsByIdPlayers(idPlayer1, idPlayer2);
                if (chats == null || !chats.Any())
                {
                    var message = $"Aucun chat n'a été trouvé pour les joueurs {idPlayer1} et {idPlayer2}.";
                    _logger.LogInformation(message);
                    return NotFound(new ApiResponse(message));
                }

                var dtoChats = chats.Select(c => c.ToDto()).ToList();
                var success_message = $"La récupération des chats a réussi pour les joueurs {idPlayer1} et {idPlayer2}. Nombre de chats : {dtoChats.Count}";
                _logger.LogInformation(success_message);
                return Ok(new ApiResponse<List<DTOChat>>(success_message, dtoChats));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la récupération des chats pour les joueurs {idPlayer1} et {idPlayer2}.";
                _logger.LogError(ex, error_message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse(error_message));
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveChat(int id)
        {
            try
            {
                var result = await _dataManager.RemoveChat(id);
                if (result)
                {
                    var success_message = $"Le chat avec l'identifiant {id} a été supprimé avec succès.";
                    _logger.LogInformation(success_message);
                    return Ok(new ApiResponse(success_message));
                }

                var warning_message = $"Le chat avec l'identifiant {id} n'a pas été trouvé.";
                _logger.LogInformation(warning_message);
                return NotFound(new ApiResponse(warning_message));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la suppression du chat : {ex.Message}";
                _logger.LogError(ex, error_message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse(error_message));
            }
        }
    }
}
