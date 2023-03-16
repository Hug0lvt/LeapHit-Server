using ApiLeapHit.Controllers;
using ApiLeapHit.Mapper;
using DataBase.DataManager;
using DataBase.Entity;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestControleurs
{
    [TestClass]
    public class UnitTestMessages
    {
        private DbDataManager _dataManager = new DbDataManager();
        private readonly ILogger<MessagesController> _logger = new NullLogger<MessagesController>();

        [TestMethod]
        public async Task ReceiveMessage_ReturnsOkResult()
        {
            // Arrange
            var controller = new MessagesController(_dataManager, _logger);
            var nb = _dataManager.GetNbMessages();
            var testMessage = new Message { messageId = nb.Result+1, message = "Test message", timestamp = new DateTime(2023, 3, 10, 14, 30, 0, DateTimeKind.Utc), player = 1 , chat =1};
            await _dataManager.SendMessage(testMessage);

            // Act
            var result = await controller.ReceiveMessage(1);
            var objectResult = (ObjectResult)result.Result;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task ReceiveMessage_ReturnsNotFound()
        {
            // Arrange
            var controller = new MessagesController(_dataManager, _logger);
            var nb = _dataManager.GetNbMessages();

            // Act
            var result = await controller.ReceiveMessage(nb.Result+1);
            var objectResult = (ObjectResult)result.Result;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task RemoveMessage_ReturnsBadRequest()
        {
            // Arrange
            var controller = new MessagesController(_dataManager, _logger);
            var nb = _dataManager.GetNbMessages();

            // Act
            var result = await controller.RemoveMessage(nb.Result + 1);
            var objectResult = (ObjectResult)result;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task RemoveMessage_ReturnsOk()
        {
            // Arrange
            var controller = new MessagesController(_dataManager, _logger);
            var nb = _dataManager.GetNbMessages();
            var testMessage = new Message { messageId = nb.Result + 1, message = "Test message", timestamp = new DateTime(2023, 3, 10, 14, 30, 0, DateTimeKind.Utc), player = 1, chat = 1 };

            // Act
            var result = await controller.RemoveMessage(nb.Result + 1);
            var objectResult = (ObjectResult)result;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task SendMessage_ReturnsCreated()
        {
            // Arrange
            var controller = new MessagesController(_dataManager, _logger);
            var nb = _dataManager.GetNbMessages();
            var testMessage = new DTOMessage { messageId = nb.Result + 1, message = "Test message", timestamp = new DateTime(2023, 3, 10, 14, 30, 0, DateTimeKind.Utc), PlayerId = 1, ChatId = 1 };

            // Act
            var result = await controller.SendMessage(testMessage);
            var objectResult = (ObjectResult)result;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
            await controller.RemoveMessage(nb.Result + 1);
        }

        [TestMethod]
        public async Task SendMessage_ReturnsNotFound()
        {
            // Arrange
            var controller = new MessagesController(_dataManager, _logger);
            var nb = _dataManager.GetNbMessages();
            var nbP = _dataManager.GetNbPlayers();
            var testMessage = new DTOMessage { messageId = nb.Result + 1, message = "Test message", timestamp = new DateTime(2023, 3, 10, 14, 30, 0, DateTimeKind.Utc), PlayerId = nb.Result+1, ChatId = 1 };

            // Act
            var result = await controller.SendMessage(testMessage);
            var objectResult = (ObjectResult)result;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
            await controller.RemoveMessage(nb.Result + 1);
        }
    }
}
