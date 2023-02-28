using ApiLeapHit.Controllers;
using ApiLeapHit.Mapper;
using DataBase.Context;
using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using DTO.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestControleurs
{
    [TestClass]
    public class UnitTestGames
    {
        [TestMethod]
        public async Task TestGetPlayer_ValidId()
        {
            // Arrange
            int id = 8;
            DbDataManager dataManager = new DbDataManager();
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var player = new Player { playerId = id, name = "Test Player", nbBallTouchTotal = 0, timePlayed = 3 };
            var controller = new PlayerController(dataManager, loggerFactory.CreateLogger<PlayerController>());


            var rep = await controller.AddPlayer(player.ToDto());

            // Act
            var result = await controller.GetPlayer(id);
            var objectResult = (ObjectResult)result.Result;
            var apiResponse = (ApiResponse<DTOPlayer>)objectResult.Value;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.AreEqual(apiResponse.Data.playerId, id);
         
        }

    }
}