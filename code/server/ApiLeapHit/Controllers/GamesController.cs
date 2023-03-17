using ApiLeapHit.Mapper;
using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using DTO.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace ApiLeapHit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly DbDataManager _dataManager;

        private readonly ILogger<GamesController> _logger;

        public GamesController(DbDataManager dataManager, ILogger<GamesController> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOGame>> GetGame(int id)
        {
            try
            {
                _logger.LogInformation("Récupération de la game avec l'identifiant {id}", id);

                var game = await _dataManager.GetGame(id);
                if (game == null)
                {
                    var message = $"La game avec l'identifiant {id} n'existe pas";
                    _logger.LogWarning(message);
                    return NotFound(new ApiResponse<object>(message));
                }

                _logger.LogInformation("Récupération des joueurs pour la game avec l'identifiant {id}", id);

                return Ok(new ApiResponse<DTOGame>("Récupération de la game réussie.", game.ToDto()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Une erreur est survenue lors de la récupération des données pour la game avec l'identifiant {id}");
                return StatusCode(500, new ApiResponse<object>($"Une erreur est survenue lors de la récupération des données : {ex.Message}"));
            }
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<DTOGame>>> GetGames()
        //{
        //    try
        //    {
        //        _logger.LogInformation("Récupération de toutes les games.");

        //        var games = await _dataManager.GetGames();
        //        if (games == null)
        //        {
        //            var message = "Aucune game n'a été trouvée.";
        //            _logger.LogWarning(message);
        //            return NotFound(new ApiResponse<object>(message));
        //        }

        //        var dtoGames = new List<DTOGame>();

        //        foreach (var game in games)
        //        {
        //            var winner = await _dataManager.GetPlayer(game.winner);
        //            var loser = await _dataManager.GetPlayer(game.loser);

        //            //ce cas n'est jamais censé arrivé
        //            if (winner == null || loser == null)
        //            {
        //                _logger.LogError($"Le joueur gagnant ou le joueur perdant n'existe pas pour le jeu avec l'identifiant {game.gameId}.");
        //                continue;
        //            }

        //            dtoGames.Add(game.ToDto());
        //        }

        //        _logger.LogInformation("{Count} games ont été récupérées.", dtoGames.Count);
        //        return Ok(new ApiResponse<IEnumerable<DTOGame>>("La récupération des games a réussi.", dtoGames));
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = "Une erreur est survenue lors de la récupération des données.";
        //        _logger.LogError(ex, message);
        //        return StatusCode(500, new ApiResponse<object>($"{message} {ex.Message}"));
        //    }
        //}

        [HttpGet("Player/{id}")]
        public async Task<ActionResult<IEnumerable<DTOGame>>> GetGameByIdPlayer(string id)
        {
            try
            {
                var games = await _dataManager.GetGameById(id);

                if (games == null || games.Count == 0)

                {
                    var message = $"Aucune game trouvée pour le joueur avec l'id {id}.";
                    _logger.LogInformation(message);
                    return NotFound(new ApiResponse<object>(message));
                }

                var dtoGames = new List<DTOGame>();

                foreach (var game in games)
                {
                    var winner = await _dataManager.GetPlayer(game.winner);
                    var loser = await _dataManager.GetPlayer(game.loser);

                    //ce cas n'est jamais censé arrivé
                    if (winner == null || loser == null)
                    {
                        _logger.LogError($"Le joueur gagnant ou le joueur perdant n'existe pas pour le jeu avec l'identifiant {game.gameId}.");
                        continue;
                    }
                    dtoGames.Add(game.ToDto());
                }

                var successMessage = $"Récupération réussie des games pour le joueur avec l'id {id}.";
                _logger.LogInformation(successMessage);
                return Ok(new ApiResponse<IEnumerable<DTOGame>>(successMessage, dtoGames));
            }
            catch (Exception ex)
            {
                var errorMessage = $"Une erreur est survenue lors de la récupération des games pour le joueur avec l'id {id}.";
                _logger.LogError(errorMessage + " Error message: " + ex.Message);
                return StatusCode(500, new ApiResponse<object>(errorMessage));
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddGame([FromBody] DTOGame dtoGame)
        {
            try
            {
                var winner = await _dataManager.GetPlayer(dtoGame.playerWinner);
                var loser = await _dataManager.GetPlayer(dtoGame.playerLoser);

                if (winner == null || loser == null)
                {
                    var errorMessage = "Le joueur gagnant ou le joueur perdant n'existe pas pour la partie avec l'identifiant " + dtoGame.gameId + ".";
                    _logger.LogError(errorMessage);
                    return NotFound(new ApiResponse<Game>(errorMessage));
                }

                var game = dtoGame.ToGame();
                await _dataManager.AddGame(game);

                var successMessage = "La partie avec l'identifiant " + game.gameId + " a été ajoutée avec succès.";
                _logger.LogInformation(successMessage);
                return Ok(new ApiResponse<Game>(successMessage, game));
            }
            catch (Exception ex)
            {
                var errorMessage = "Une erreur est survenue lors de l'ajout de la partie : " + ex.Message;
                _logger.LogError(errorMessage);
                return StatusCode(500, new ApiResponse<object>(errorMessage));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveGame(int id)
        {
            try
            {
                var result = await _dataManager.RemoveGame(id);
                if (result)
                {
                    var successMessage = $"La game avec l'identifiant {id} a été supprimée avec succès.";
                    _logger.LogInformation(successMessage);
                    return Ok(new ApiResponse<object>(successMessage));
                }

                var notFoundMessage = $"La game avec l'identifiant {id} n'existe pas.";
                _logger.LogInformation(notFoundMessage);
                return NotFound(new ApiResponse<object>(notFoundMessage));
            }
            catch (Exception ex)
            {
                var errorMessage = $"Une erreur est survenue lors de la suppression de la game avec l'identifiant {id} : {ex.Message}";
                _logger.LogError(errorMessage);
                return StatusCode(500, new ApiResponse<object>(errorMessage));
            }
        }
    }
}
