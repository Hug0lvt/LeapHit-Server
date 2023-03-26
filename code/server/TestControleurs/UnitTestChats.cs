using ApiLeapHit.Controllers;
using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestControleurs
{
    [TestClass]
    public class UnitTestChats
    {
        //private  DbDataManager _dataManager = new DbDataManager(); 
        //private readonly ILogger<ChatsController> _logger = new NullLogger<ChatsController>();

        //[TestMethod]
        //public async Task AddChat_ReturnsOkResult_WhenChatIsAdded()
        //{
        //    // Arrange
        //    var player1 = new Player { playerId = 1, name = "Player1" };
        //    var player2 = new Player { playerId = 2, name = "Player2" };
        //    var dtoChat = new DTOChat { PlayerId1 = player1.playerId, PlayerId2 = player2.playerId };
        //    var controller = new ChatsController(_dataManager, _logger);

        //    // Act
        //    var result = await controller.AddChat(dtoChat);
        //    var objectResult = result as ObjectResult;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task AddChat_ReturnsBadRequestResult()
        //{
        //    // Arrange
        //    var nb = await _dataManager.GetNbChats();
        //    var dtoChat = new DTOChat { PlayerId1 = 1, PlayerId2 = "test" };
        //    var controller = new ChatsController(_dataManager, _logger);

        //    // Act
        //    var result = await controller.AddChat(dtoChat);
        //    var objectResult = result as ObjectResult;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task GetChats_ReturnsOkResult()
        //{
        //    // Arrange
        //    var controller = new ChatsController(_dataManager, _logger);

        //    // Act
        //    var result = await controller.GetChats();
        //    var objectResult = (ObjectResult)result.Result;
        //    var dtoChats = objectResult.Value as IEnumerable<DTOChat>;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        //}
    

        ////[TestMethod]
        ////public async Task GetChats_ReturnsNotFoundResult()
        ////{
        ////    // Arrange
        ////    var mockDataManager = new Mock<DbDataManager>();
        ////    mockDataManager.Setup(dm => dm.GetChats()).ReturnsAsync(new List<Chat>());
        ////    var controller = new ChatsController(mockDataManager.Object, _logger);

        ////    // Act
        ////    var result = await controller.GetChats();
        ////    var objectResult = (ObjectResult)result.Result;

        ////    // Assert
        ////    Assert.IsNotNull(objectResult);
        ////    Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        ////}

        //[TestMethod]
        //public async Task GetChatById_ReturnsOkResult()
        //{
        //    // Arrange
        //    var chat = new Chat { chatId = 1, player1 = 1, player2 = 2 };
        //    var controller = new ChatsController(_dataManager, _logger);

        //    // Act
        //    var result = await controller.GetChatById(chat.chatId);
        //    var objectResult = result.Result as ObjectResult;
        //    var dtoChat = objectResult.Value as DTOChat;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        //    Assert.IsNotNull(dtoChat);
        //    Assert.AreEqual(chat.chatId, dtoChat.chatId);
        //}

        //[TestMethod]
        //public async Task GetChatById_ReturnsNotFoundResult()
        //{
        //    // Arrange
        //    var chatId = 1000;
        //    var controller = new ChatsController(_dataManager, _logger);

        //    // Act
        //    var result = await controller.GetChatById(chatId);
        //    var objectResult = result.Result as ObjectResult;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task RemoveChat_ReturnsOkResult()
        //{
        //    // Arrange
        //    var nb = await _dataManager.GetNbChats();
        //    var chat = new Chat { chatId = nb+1, player1 = 1, player2 = 2 };
        //    await _dataManager.AddChat(chat);
        //    var controller = new ChatsController(_dataManager, _logger);

        //    // Act
        //    var result = await controller.RemoveChat(chat.chatId);
        //    var objectResult = result as ObjectResult;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        //}

        //[TestMethod]
        //public async Task RemoveChat_ReturnsNotFoundResult()
        //{
        //    // Arrange
        //    var chatId = 1000;
        //    var controller = new ChatsController(_dataManager, _logger);

        //    // Act
        //    var result = await controller.RemoveChat(chatId);
        //    var objectResult = result as ObjectResult;

        //    // Assert
        //    Assert.IsNotNull(objectResult);
        //    Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        //}

    }
}
