using DataBase.Context;
using DataBase.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsDatabase
{
    public class TestsMessage
    {
        [Fact]
        public void Add_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
            .UseInMemoryDatabase(databaseName: "Add_Test_Database_Messages")
            .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Rami12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Chat chat = new Chat { chatId = 1, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                Message message = new Message { messageId = 1, ChatId = chat, message = "message", chat = 1, player = "Loris12345", PlayerId = player, timestamp = new DateTime(2023, 3, 10, 14, 30, 0, DateTimeKind.Utc) };

                context.Messages.Add(message);
                context.SaveChanges();
            }
            using (var context = new PongDbContext(options))
            {
                Assert.Equal(1, context.Messages.Count());
                Assert.Equal(1, context.Messages.First().messageId);
            }

        }
        [Fact]
        public void Modify_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "Modify_Test_Database_Messages")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Rami12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Chat chat = new Chat { chatId = 1, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                Message message = new Message { messageId = 1, ChatId = chat, message = "message", chat = 1, player = "Loris12345", PlayerId = player, timestamp = new DateTime(2023, 3, 10, 14, 30, 0, DateTimeKind.Utc) };

                context.Messages.Add(message);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int messageId = 1;
                Assert.Equal(1, context.Messages.Where(n => n.messageId.Equals(messageId)).Count());

                var elementalist = context.Messages.Where(n => n.messageId.Equals(messageId)).First();
                elementalist.message = "Loris1";
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int messageId = 1;
                Assert.Equal(1, context.Messages.Where(n => n.messageId.Equals(messageId)).Count());
                var elementalist = context.Messages.Where(n => n.messageId.Equals(messageId)).First();
                Assert.Equal("Loris1", elementalist.message);
            }
        }

        [Fact]
        public void Delete_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Test_Database_Messages")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Rami12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Chat chat = new Chat { chatId = 1, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                Message message = new Message { messageId = 1, ChatId = chat, message = "message", chat = 1, player = "Loris12345", PlayerId = player, timestamp = new DateTime(2023, 3, 10, 14, 30, 0, DateTimeKind.Utc) };

                context.Messages.Add(message);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int messageId = 1;
                Assert.Equal(1, context.Messages.Where(n => n.messageId.Equals(messageId)).Count());

                context.Messages.Remove(context.Messages.Where(n => n.messageId.Equals(messageId)).First());
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int messageId = 1;
                Assert.Equal(0, context.Messages.Where(n => n.messageId.Equals(messageId)).Count());
            }

        }


        [Fact]
        public void GetAllMessage_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllSkin_Test_Database_Messages")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Rami12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Chat chat = new Chat { chatId = 1, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                Message message = new Message { messageId = 1, ChatId = chat, message = "message", chat = 1, player = "Loris12345", PlayerId = player, timestamp = new DateTime(2023, 3, 10, 14, 30, 0, DateTimeKind.Utc) };

                context.Messages.Add(message);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {

                var messages = context.Messages.ToList();

                Assert.Equal(1, messages.Count);

                var classic = messages.FirstOrDefault(c => c.messageId == 1);
                Assert.NotNull(classic);
                Assert.Equal("Loris12345", classic.player);
                Assert.Equal("message", classic.message);
            }
        }
    }
}
