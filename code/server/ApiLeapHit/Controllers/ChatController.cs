using ApiLeapHit.Mapper;
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
    public class ChatController : Controller
    {

        private readonly DbDataManager _dataManager;
        private readonly ILogger<ChatController> _logger;

        public ChatController(DbDataManager dataManager, ILogger<ChatController> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> AddChat([FromBody] DTOChat dtoChat)
        {
            try
            {
                var player1 = await _dataManager.GetPlayer(dtoChat.PlayerId1);
                var player2 = await _dataManager.GetPlayer(dtoChat.PlayerId2);

                await _dataManager.AddChat(dtoChat.ToChat());

                var success_message = $"Le chat entre {player1.name} et {player2.name} a été ajouté avec succès.";
                _logger.LogInformation(success_message);
                return Ok(new ApiResponse<object>(success_message));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de l'ajout du chat : {ex.Message}";
                _logger.LogError(ex, error_message);
                return StatusCode(500, new ApiResponse<object>(error_message));
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
                    _logger.LogWarning(message);
                    return NotFound(new ApiResponse<object>(message));
                }

                var dtoChats = new List<DTOChat>();
                foreach (var chat in chats)
                {
                    //var player1 = await _dataManager.GetPlayer(chat.player1);
                    //var player2 = await _dataManager.GetPlayer(chat.player2);

                    var dtoChat = chat.ToDto();

                    dtoChats.Add(dtoChat);
                }

                var success_message = $"La récupération des chats a réussi. Nombre de chats : {dtoChats.Count}";
                _logger.LogInformation(success_message);
                return Ok(new ApiResponse<List<DTOChat>>(success_message,dtoChats));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la récupération des chats : {ex.Message}";
                _logger.LogError(ex, error_message);
                return StatusCode(500, new ApiResponse<object>(error_message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DTOChat>>> GetChatById(int id)
        {
            try
            {
                var chat = await _dataManager.GetChat(id);
                if (chat == null)
                {
                    var message = "Aucun chat n'a été trouvé.";
                    _logger.LogWarning(message);
                    return NotFound(new ApiResponse<object>(message));
                }

                //var player1 = await _dataManager.GetPlayer(chat.player1);
                //var player2 = await _dataManager.GetPlayer(chat.player2);

                var dtoChat = chat.ToDto();

                var success_message = $"La récupération du chat a réussi pour le chat {id}.";
                _logger.LogInformation(success_message);
                return Ok(new ApiResponse<DTOChat>(success_message, dtoChat));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la récupération du chat {id} : {ex.Message}";
                _logger.LogError(ex, error_message);
                return StatusCode(500, new ApiResponse<object>(error_message));
            }
        }

        [HttpGet("byPlayer/{id}")]
        public async Task<ActionResult<IEnumerable<DTOChat>>> GetChatsByIdPlayer(int id)
        {
            try
            {
                var chats = await _dataManager.GetChatsByIdPlayer(id);
                if (chats == null || chats.Count() == 0)
                {
                    var message = "Aucun chat n'a été trouvé pour l'id : {id}.";
                    _logger.LogWarning(message);
                    return NotFound(new ApiResponse<object>(message));
                }

                var dtoChats = new List<DTOChat>();
                foreach (var chat in chats)
                {
                    dtoChats.Add(chat.ToDto());
                }

                var success_message = $"La récupération des chats a réussi pour l'id : {id}. Nombre de chats : {dtoChats.Count}";
                _logger.LogInformation(success_message);
                return Ok(new ApiResponse<List<DTOChat>>(success_message, dtoChats));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la récupération des chats de l'utilisateur {id} : {ex.Message}";
                _logger.LogError(ex, error_message);
                return StatusCode(500, new ApiResponse<object>(error_message));
            }
        }

        [HttpGet("players/{idPlayer1}/{idPlayer2}")]
        public async Task<ActionResult<IEnumerable<DTOChat>>> GetChatsByIdPlayers(int idPlayer1, int idPlayer2)
        {
            try
            {
                var chats = await _dataManager.GetChatsByIdPlayers(idPlayer1,idPlayer2);
                if (chats == null ||chats.Count() == 0)
                {
                    var message = $"Aucun chat n'a été trouvé pour les joueurs {idPlayer1} et {idPlayer2}.";
                    _logger.LogWarning(message);
                    return NotFound(new ApiResponse<object>(message));
                }

                var dtoChats = new List<DTOChat>();
                foreach (var chat in chats)
                {
                    //var player1 = await _dataManager.GetPlayer(chat.player1);
                    //var player2 = await _dataManager.GetPlayer(chat.player2);

                    dtoChats.Add(chat.ToDto());
                }

                var success_message = $"La récupération des chats a réussi pour les joueurs {idPlayer1} et {idPlayer2}. Nombre de chats : {dtoChats.Count}";
                _logger.LogInformation(success_message);
                return Ok(new ApiResponse<List<DTOChat>>(success_message, dtoChats));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la récupération des chats pour les joueurs {idPlayer1} et {idPlayer2} : {ex.Message}";
                _logger.LogError(ex, error_message);
                return StatusCode(500, new ApiResponse<object>(error_message));
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
                    return Ok(new ApiResponse<object>(success_message));
                }

                var warning_message = $"Le chat avec l'identifiant {id} n'a pas été trouvé.";
                _logger.LogWarning(warning_message);
                return NotFound(new ApiResponse<object>(warning_message));
            }
            catch (Exception ex)
            {
                var error_message = $"Une erreur est survenue lors de la suppression du chat : {ex.Message}";
                _logger.LogError(ex, error_message);
                return StatusCode(500, new ApiResponse<object>(error_message));
            }
        }
    }
}
