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
    public class PlayersController : ControllerBase
    {
        private readonly DbDataManager _dataManager;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(DbDataManager dataManager, ILogger<PlayersController> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        [HttpGet("/clePlayer/{idIdentification}")]
        public async Task<ActionResult<ApiResponse<string>>> CreatePlayer(string idIdentification)
        {
            try
            {
                if(!idIdentification.Equals("K02q7naLzjmodzAFfoSO4mPydr7W5hydPMrHtA6D"))
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new ApiResponse("Le numéo n'est pas correct."));
                }
                var player = new Player();
                string id;
                do
                {
                    // Générer un id unique avec des chiffres et des lettres
                    id = Guid.NewGuid().ToString("N");
                }
                while (await _dataManager.GetPlayer(id) != null); 
                player.playerId = id;
                await _dataManager.AddPlayer(player);

                var response = new ApiResponse<string>($"Le joueur a été créé avec succès. Id du joueur : {id}.", id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la création du joueur.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse("Une erreur est survenue lors de la création du joueur."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DTOPlayer>>> GetPlayer(string id)
        {
            try
            {
                var player = await _dataManager.GetPlayer(id);
                if (player == null)
                {
                    return NotFound(new ApiResponse("Joueur non trouvé."));
                }

                var response = new ApiResponse<DTOPlayer>($"Le joueur avec l'id {id} a été récupéré avec succès.", player.ToDto());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la récupération du joueur avec l'id {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse("Une erreur est survenue lors de la récupération du joueur."));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> AddPlayer([FromBody] DTOPlayer dtoPlayer)
        {
            try
            {
                var player = dtoPlayer.ToPlayer();

                await _dataManager.AddPlayer(player);

                var response = new ApiResponse("Joueur ajouté avec succès.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de l'ajout du joueur.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse($"Une erreur est survenue lors de l'ajout du joueur. {ex.Message}"));
            }
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<DTOPlayer>>> GetPlayers()
        //{
        //    try
        //    {
        //        var players = await _dataManager.GetPlayers();
        //        if (players == null || players.Count() == 0)
        //        {
        //            return NotFound(new ApiResponse<IEnumerable<DTOPlayer>>("Aucun joueur trouvé."));
        //        }

        //        var dtoPlayers = players.Select(p => p.ToDto()).ToList();

        //        var response = new ApiResponse<IEnumerable<DTOPlayer>>($"La récupération des players a réussi. Nombre de players : {dtoPlayers.Count}", dtoPlayers);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Une erreur est survenue lors de la récupération des joueurs.");
        //        return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse("Une erreur est survenue lors de la récupération des joueurs."));
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePlayer(string id)
        {
            try
            {
                var result = await _dataManager.RemovePlayer(id);
                if (result)
                {
                    var response = new ApiResponse("Joueur supprimé avec succès.");

                    return Ok(response);
                }
                return NotFound(new ApiResponse("Joueur non trouvé."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la suppression du joueur avec l'id {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse("Une erreur est survenue lors de la suppression du joueur."));
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] DTOPlayer dtoPlayer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse("Les données du joueur sont invalides."));
                }

                var player = dtoPlayer.ToPlayer();

                var playerTest = await _dataManager.GetPlayer(id);
                if (playerTest == null)
                {
                    return NotFound(new ApiResponse("Joueur non trouvé."));
                }

                await _dataManager.UpdatePlayer(id, player.name);

                var response = new ApiResponse("Joueur mis à jour avec succès.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la modification du joueur avec l'id {id}.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse("Une erreur est survenue lors de la modification du joueur."));
            }

        }
    }
}
