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
    public class PlayerController : ControllerBase
    {
        private readonly DbDataManager _dataManager;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(DbDataManager dataManager, ILogger<PlayerController> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOPlayer>> GetPlayer(int id)
        {
            try
            {
                var player = await _dataManager.GetPlayer(id);
                if (player == null)
                {
                    return NotFound(new ApiResponse<object>("Joueur non trouvé."));
                }

                var response = new ApiResponse<DTOPlayer>($"Le joueur avec l'id {id} a été récupéré avec succès.", player.ToDto());

                // Ajout des liens HATEOAS
                response.Links.Add(new ApiLink(
                    Url.Action("GetPlayer", "Player", new { id }),
                    "self",
                    "GET"
                ));
                response.Links.Add(new ApiLink(
                    Url.Action("RemovePlayer", "Player", new { id }),
                    "delete",
                    "DELETE"
                ));
                response.Links.Add(new ApiLink(
                    Url.Action("Put", "Player", new { id }),
                    "update",
                    "PUT"
                ));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la récupération du joueur avec l'id {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>("Une erreur est survenue lors de la récupération du joueur."));
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddPlayer([FromBody] DTOPlayer dtoPlayer)
        {
            try
            {
                var player = dtoPlayer.ToPlayer();

                await _dataManager.AddPlayer(player);

                // Ajout des liens HATEOAS
                var response = new ApiResponse<object>("Joueur ajouté avec succès.");
                response.Links.Add(new ApiLink(
                    Url.Action("GetPlayer", "Player", new { id = player.playerId }),
                    "self",
                    "GET"
                ));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de l'ajout du joueur.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>("Une erreur est survenue lors de l'ajout du joueur."));
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOPlayer>>> GetPlayers()
        {
            try
            {
                var players = await _dataManager.GetPlayers();
                if (players == null || players.Count() == 0)
                {
                    return NotFound(new ApiResponse<IEnumerable<DTOPlayer>>("Aucun joueur trouvé."));
                }

                var dtoPlayers = players.Select(p => p.ToDto()).ToList();

                var response = new ApiResponse<IEnumerable<DTOPlayer>>($"La récupération des players a réussi. Nombre de players : {dtoPlayers.Count}", dtoPlayers);

                // Ajout des liens HATEOAS
                response.Links.Add(new ApiLink(
                    Url.Action("GetPlayers", "Player"),
                    "self",
                    "GET"
                ));
                response.Links.Add(new ApiLink(
                    Url.Action("AddPlayer", "Player"),
                    "create",
                    "POST"
                ));

                foreach (var player in dtoPlayers)
                {
                    response.Links.Add(new ApiLink(
                        Url.Action("GetPlayer", "Player", new { id = player.playerId }),
                        "get_player",
                        "GET"
                    ));
                    response.Links.Add(new ApiLink(
                        Url.Action("RemovePlayer", "Player", new { id = player.playerId }),
                        "delete_player",
                        "DELETE"
                    ));
                    response.Links.Add(new ApiLink(
                        Url.Action("Put", "Player", new { id = player.playerId }),
                        "update_player",
                        "PUT"
                    ));
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la récupération des joueurs.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>("Une erreur est survenue lors de la récupération des joueurs."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePlayer(int id)
        {
            try
            {
                var result = await _dataManager.RemovePlayer(id);
                if (result)
                {
                    // Ajout des liens HATEOAS
                    var response = new ApiResponse<object>("Joueur supprimé avec succès.");
                    response.Links.Add(new ApiLink(
                        Url.Action("GetPlayers", "Player"),
                        "self",
                        "GET"
                    ));

                    return Ok(response);
                }
                return NotFound(new ApiResponse<object>("Joueur non trouvé."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la suppression du joueur avec l'id {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>("Une erreur est survenue lors de la suppression du joueur."));
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DTOPlayer dtoPlayer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>("Les données du joueur sont invalides."));
                }

                var player = dtoPlayer.ToPlayer();

                var playerTest = await _dataManager.GetPlayer(id);
                if (playerTest == null)
                {
                    return NotFound(new ApiResponse<object>("Joueur non trouvé."));
                }

                await _dataManager.UpdatePlayer(id, player.name);

                // Ajout des liens HATEOAS
                var response = new ApiResponse<object>("Joueur mis à jour avec succès.");
                response.Links.Add(new ApiLink(
                    Url.Action("GetPlayer", "Player", new { id }),
                    "self",
                    "GET"
                ));
                response.Links.Add(new ApiLink(
                    Url.Action ("RemovePlayer", "Player", new { id }),
                    "delete",
                    "DELETE"
                    ));

        
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la modification du joueur avec l'id {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>("Une erreur est survenue lors de la modification du joueur."));
            }

        }
    }
}
