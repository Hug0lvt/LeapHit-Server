using DataBase.DataManager;
using DataBase.Entity;
using DTO;
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

        public PlayerController(DbDataManager dataManager)
        {
            _dataManager = dataManager;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DTOPlayer>> GetPlayer(int id)
        {
            var player = await _dataManager.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }


            var dtoPlayer = new DTOPlayer
            {
                playerId = player.playerId,
                name = player.name,
                nbBallTouchTotal = player.nbBallTouchTotal,
                timePlayed = player.timePlayed,
                
            };
            return Ok(dtoPlayer);
        }

        [HttpPost]
        public async Task<ActionResult> AddPlayer([FromBody] DTOPlayer dtoPlayer)
        {
            
            var player = new Player
            {
                playerId = dtoPlayer.playerId,
                name = dtoPlayer.name,
                nbBallTouchTotal = dtoPlayer.nbBallTouchTotal,
                timePlayed = dtoPlayer.timePlayed,
            };

            await _dataManager.AddPlayer(player);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemovePlayer(int id)
        {
            try
            {
                var result = await _dataManager.RemovePlayer(id);
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DTOPlayer dtoPlayer)
        {   
            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.BadRequest); //"Les données du player ne sont pas correctes"

            Player playerTeste = await _dataManager.GetPlayer(id);
            if (playerTeste != null)
                return StatusCode((int)HttpStatusCode.NotFound); //"Le player n'existe pas."

            var player = new Player
            {
                playerId = dtoPlayer.playerId,
                name = dtoPlayer.name,
                nbBallTouchTotal = dtoPlayer.nbBallTouchTotal,
                timePlayed = dtoPlayer.timePlayed,
            };

            Player playerUpdate = await _dataManager.GetPlayer(id);
            await _dataManager.UpdatePlayer(id,player.name);
            return StatusCode((int)HttpStatusCode.OK); //"Le champion a été modifié."
        }

    }
}
