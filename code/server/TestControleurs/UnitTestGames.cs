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
using Moq;
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
            int id = 9;
            var mockDataManager = new Mock<DbDataManager>();
            var mockLogger = new Mock<ILogger<PlayerController>>();
            var player = new Player { playerId = id, name = "Test Player", nbBallTouchTotal = 0, timePlayed = 3 };
            var controller = new PlayerController(mockDataManager.Object, mockLogger.Object);

            var rep = await controller.AddPlayer(player.ToDto());

            // Act
            var result = await controller.GetPlayer(id);
            var objectResult = (ObjectResult)result.Result;
            var apiResponse = objectResult.Value as ApiResponse<DTOPlayer>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.AreEqual(apiResponse.Data.playerId, id);
        }

    }
}