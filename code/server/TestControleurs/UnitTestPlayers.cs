using ApiLeapHit.Controllers;
using DataBase.DataManager;
using DataBase.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestControleurs
{
    [TestClass]
    public class UnitTestPlayers
    {
        private DbDataManager _dataManager = new DbDataManager();
        private readonly ILogger<PlayersController> _logger = new NullLogger<PlayersController>();

        //[TestMethod]
        //public async Task GetPlayer_ReturnsOkResult()
        //{
        //    // Arrange
        //    var playerId = 1;

        //    var player = new Player() { Id = playerId, Name = "John Doe" };
        //    _mockDataManager.Setup(dm => dm.GetPlayer(playerId)).ReturnsAsync(player);

        //    // Act
        //    var result = await _controller.GetPlayer(playerId);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        //    var apiResponse = ((OkObjectResult)result).Value as ApiResponse<DTOPlayer>;
        //    Assert.IsNotNull(apiResponse);
        //}
    }
}
