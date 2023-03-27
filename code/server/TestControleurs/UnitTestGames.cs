using ApiLeapHit.Controllers;
using ApiLeapHit.Mapper;
using DataBase.Context;
using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using DTO.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestControleurs
{
    [TestClass]
    public class UnitTestGames
    {
        //private DbDataManager _dataManager = new DbDataManager();
        //private readonly ILogger<GamesController> _logger = new NullLogger<GamesController>();

        //[TestMethod]
        //public async Task GetGame_ReturnsOkResult()
        //{
        //    // Arrange
        //    var controller = new GamesController(_dataManager, _logger);
        //    var nb = _dataManager.GetNbGames();
        //    var testGame = new Game { gameId = nb.Result + 1, durationGame = 3, loser = 1, winner = 2, nbMaxEchanges = 33, scoreLoser = 5, scoreWinner = 6 };
        //    await _dataManager.AddGame(testGame);

        //    // Act
        //    var result = await controller.GetGame(nb.Result + 1);
        //    var objectResult = (ObjectResult)result.Result;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task GetGame_ReturnsNotFoundResult()
        //{
        //    // Arrange
        //    var controller = new GamesController(_dataManager, _logger);
        //    var nb = _dataManager.GetNbGames();

        //    // Act
        //    var result = await controller.GetGame(nb.Result + 1);
        //    var objectResult = (ObjectResult)result.Result;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task RemoveGame_ReturnsNotFoundResult()
        //{
        //    // Arrange
        //    var controller = new GamesController(_dataManager, _logger);
        //    var nb = _dataManager.GetNbGames();

        //    // Act
        //    var result = await controller.RemoveGame(nb.Result + 1);
        //    var objectResult = (ObjectResult)result;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task RemoveGame_ReturnsOKResult()
        //{
        //    // Arrange
        //    var controller = new GamesController(_dataManager, _logger);
        //    var nb = _dataManager.GetNbGames();
        //    var testGame = new Game { gameId = nb.Result + 1, durationGame = 3, loser = 1, winner = 2, nbMaxEchanges = 33, scoreLoser = 5, scoreWinner = 6 }; 
        //    await _dataManager.AddGame(testGame);

        //    // Act
        //    var result = await controller.RemoveGame(nb.Result + 1);
        //    var objectResult = (ObjectResult)result;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task GetGameByIdPlayer_ReturnsOKResult()
        //{
        //    // Arrange
        //    var controller = new GamesController(_dataManager, _logger);
        //    var nb = _dataManager.GetNbGames();
        //    var nbP = _dataManager.GetNbPlayers();
        //    var testGame = new Game { gameId = nb.Result + 1, durationGame = 3, loser = nbP.Result, winner = 2, nbMaxEchanges = 33, scoreLoser = 5, scoreWinner = 6 };
        //    await _dataManager.AddGame(testGame);

        //    // Act
        //    var result = await controller.GetGameByIdPlayer(nbP.Result);
        //    var objectResult = (ObjectResult)result.Result;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task GetGameByIdPlayer_ReturnsNotFoundResult()
        //{
        //    // Arrange
        //    var controller = new GamesController(_dataManager, _logger);
        //    var nb = _dataManager.GetNbPlayers();

        //    // Act
        //    var result = await controller.GetGameByIdPlayer(nb.Result + 1);
        //    var objectResult = (ObjectResult)result.Result;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        //}

        //[HttpPost]
        //public async Task<ActionResult> AddGame([FromBody] DTOGame dtoGame)
        //{
        //    try
        //    {
        //        var winner = await _dataManager.GetPlayer(dtoGame.playerWinner);
        //        var loser = await _dataManager.GetPlayer(dtoGame.playerLoser);

        //        if (winner == null || loser == null)
        //        {
        //            var errorMessage = "Le joueur gagnant ou le joueur perdant n'existe pas pour la partie avec l'identifiant " + dtoGame.gameId + ".";
        //            _logger.LogError(errorMessage);
        //            return NotFound(new ApiResponse<Game>(errorMessage));
        //        }

        //        var game = dtoGame.ToGame();
        //        await _dataManager.AddGame(game);

        //        var successMessage = "La partie avec l'identifiant " + game.gameId + " a été ajoutée avec succès.";
        //        _logger.LogInformation(successMessage);
        //        return Ok(new ApiResponse<Game>(successMessage, game));
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorMessage = "Une erreur est survenue lors de l'ajout de la partie : " + ex.Message;
        //        _logger.LogError(errorMessage);
        //        return StatusCode(500, new ApiResponse<object>(errorMessage));
        //    }
        //}
    }
}