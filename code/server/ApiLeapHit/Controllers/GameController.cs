using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace ApiLeapHit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly DbDataManager _dataManager;


        public GameController(DbDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOGame>> GetGame(int id)
        {
            var game = await _dataManager.GetGame(id);
            if (game == null)
            {
                return NotFound();
            }

            var winner = await _dataManager.GetPlayer(game.winner);
            var loser = await _dataManager.GetPlayer(game.loser);


            var dtoGame = new DTOGame
            {
                gameId = game.gameId,
                durationGame = game.durationGame,
                nbMaxEchanges = game.nbMaxEchanges,
                playerWinner = new DTOPlayer { playerId = winner.playerId, name = winner.name, nbBallTouchTotal = winner.nbBallTouchTotal, timePlayed = winner.timePlayed },
                playerLoser = new DTOPlayer
                {
                    playerId = loser.playerId,
                    name = loser.name,
                    nbBallTouchTotal = loser.nbBallTouchTotal,
                    timePlayed = loser.timePlayed
                }
            };
            return Ok(dtoGame);
        }

        [HttpPost]
        public async Task<ActionResult> AddGame([FromBody] DTOGame dtoGame)
        {
            var winner = await _dataManager.GetPlayer(dtoGame.playerWinner.playerId);
            var loser = await _dataManager.GetPlayer(dtoGame.playerLoser.playerId);

            var game = new Game
            {
                durationGame = dtoGame.durationGame,
                nbMaxEchanges = dtoGame.nbMaxEchanges,
                winner = winner.playerId,
                loser = loser.playerId
            };

            await _dataManager.AddGame(game);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveGame(int id)
        {
            try
            {
                var result = await _dataManager.RemoveGame(id);
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, FactoryMessage.MessageCreate("Une erreur est survenue lors de la récupération des champions"));
            }
        }
    }
}
