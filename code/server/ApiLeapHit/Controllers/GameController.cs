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
    public class GameController : ControllerBase
    {
        private readonly DbDataManager _dataManager;

        private readonly ILogger<GameController> _logger;

        public GameController(DbDataManager dataManager, ILogger<GameController> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOGame>> GetGame(int id)
        {
            try
            {
                var game = await _dataManager.GetGame(id);
                if (game == null)
                {
                    return NotFound(new ApiResponse<DTOGame>("La game avec l'identifiant " + id + " n'existe pas."));
                }
                var winner = await _dataManager.GetPlayer(game.winner);
                //if (winner == null)
                //{
                //    return NotFound("Le joueur avec l'identifiant " + game.winner + " n'existe pas.");
                //}

                var loser = await _dataManager.GetPlayer(game.loser);
                //if (loser == null)
                //{
                //    return NotFound("Le joueur avec l'identifiant " + game.loser + " n'existe pas.");
                //}

                return Ok(new ApiResponse<DTOGame>("Récupération de la game réussie.", game.ToDto(winner, loser)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<Game>("Une erreur est survenue lors de la récupération des données : " + ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOGame>>> GetGames()
        {
            try
            {
                var games = await _dataManager.GetGames();
                if (games == null)
                {
                    return NotFound(new ApiResponse<IEnumerable<DTOGame>>("Aucune game n'ont été trouvées."));
                }

                //StringBuilder errorMessage = new StringBuilder();
                var dtoGames = new List<DTOGame>();
                foreach (var game in games)
                {
                    var winner = await _dataManager.GetPlayer(game.winner);
                    var loser = await _dataManager.GetPlayer(game.loser);

                    //if (winner == null || loser == null)
                    //{
                    //    errorMessage.Append("Le joueur gagnant ou le joueur perdant n'existe pas pour le jeu avec l'identifiant ");
                    //    errorMessage.Append(game.gameId);
                    //    errorMessage.Append(".");
                    //    break;
                    //}
                    dtoGames.Add(game.ToDto(winner, loser));
                }

                //if (errorMessage.Length > 0)
                //{
                //    return NotFound(errorMessage.ToString());
                //}
                return Ok(new ApiResponse<IEnumerable<DTOGame>>("La récupération des games à réussit." , dtoGames));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<Game>("Une erreur est survenue lors de la récupération des données : " + ex.Message));
            }
        }

        [HttpGet("byPlayer/{id}")]
        public async Task<ActionResult<IEnumerable<DTOGame>>> GetGameByIdPlayer(int id)
        {
            try { 
                var games = await _dataManager.GetGameById(id);
                if (games == null || games.Count == 0)
                {
                    return NotFound(new ApiResponse<IEnumerable<DTOGame>>("Aucune game trouvé pour le joueur avec l'id  : " + id));
                }
                //StringBuilder errorMessage = new StringBuilder();
                var dtoGames = new List<DTOGame>();
                foreach (var game in games)
                {
                    var winner = await _dataManager.GetPlayer(game.winner);
                    var loser = await _dataManager.GetPlayer(game.loser);


                    //if (winner == null)
                    //{
                    //    errorMessage.Append("Le joueur gagnant n'existe pas pour le jeu avec l'identifiant ");
                    //    errorMessage.Append(game.gameId);
                    //    errorMessage.Append(".");
                    //    break;
                    //}

                    //if (loser == null)
                    //{
                    //    errorMessage.Append("Le joueur perdant n'existe pas pour le jeu avec l'identifiant ");
                    //    errorMessage.Append(game.gameId);
                    //    errorMessage.Append(".");
                    //    break;
                    //}

                    dtoGames.Add(game.ToDto(winner, loser));
                }

                //if (errorMessage.Length > 0)
                //{
                //    return NotFound(errorMessage.ToString());
                //}
                return Ok(new ApiResponse<IEnumerable<DTOGame>>("Récupérations réussis des games pour le joueur " + id, dtoGames));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<Game>("Une erreur est survenue lors de la récupération des données : " + ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddGame([FromBody] DTOGameWithIdPlayer dtoGame)
        {
            try { 
                var winner = await _dataManager.GetPlayer(dtoGame.playerWinner);
                var loser = await _dataManager.GetPlayer(dtoGame.playerLoser);

                //StringBuilder errorMessage = new StringBuilder();

                //if (winner == null)
                //{
                //    errorMessage.Append("Le joueur gagnant avec l'identifiant ");
                //    errorMessage.Append(dtoGame.playerWinner.playerId);
                //    errorMessage.Append(" n'existe pas.");
                //    return NotFound(errorMessage.ToString());
                //}

                //if (loser == null)
                //{
                //    errorMessage.Append("Le joueur perdant avec l'identifiant ");
                //    errorMessage.Append(dtoGame.playerLoser.playerId);
                //    errorMessage.Append(" n'existe pas.");
                //    return NotFound(errorMessage.ToString());
                //}

                var game = dtoGame.ToGame(winner, loser);
                return Ok(new ApiResponse<Game>("La game a été ajoutée avec succès.", game));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<Game>("Une erreur est survenue lors de la récupération des données : " + ex.Message));
            }
        }

    [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveGame(int id)
        {
            try { 
                var result = await _dataManager.RemoveGame(id);
                if (result)
                {
                    return Ok(new ApiResponse<object>( "La game avec l'identifiant " + id + " a été supprimée avec succès."));
                }
                return NotFound(new ApiResponse<object>("La game avec l'identifiant " + id + " n'existe pas."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>("Une erreur est survenue lors de la récupération des données : " + ex.Message));
            }
        }
    }
}
