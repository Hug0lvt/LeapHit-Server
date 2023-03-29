using DataBase.DataManager;
using DataBase.Entity;
using DTO.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiLeapHit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersConnexionController : Controller
    {
        private readonly DbDataManager _dataManager;
        private readonly ILogger<PlayersConnexionController> _logger;

        public PlayersConnexionController(DbDataManager dataManager, ILogger<PlayersConnexionController> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        [HttpGet("{idIdentification}")]
        public async Task<ActionResult<ApiResponse<string>>> CreatePlayer(string idIdentification)
        {
            try
            {
                if (!idIdentification.Equals("K02q7naLzjmodzAFfoSO4mPydr7W5hydPMrHtA6D"))
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new ApiResponse("Le numéro n'est pas correct."));
                }
                var player = new Player();
                string id;
                do
                {
                    // Générer un id unique avec des chiffres et des lettres
                    id = Guid.NewGuid().ToString("N").Substring(0, 6);
                }
                while (await _dataManager.GetPlayer(id) != null);
                player.playerId = id;
                player.name = id;
                player.timePlayed = 0;
                player.nbBallTouchTotal = 0;
                await _dataManager.AddPlayer(player);

                //var response = new ApiResponse<string>($"Le joueur a été créé avec succès. Id du joueur : {id}.", id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la création du joueur.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse("Une erreur est survenue lors de la création du joueur."));
            }
        }
    }
}
