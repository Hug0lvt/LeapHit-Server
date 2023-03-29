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
    public class TestsChat
    {
        [Fact]
        public void Add_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
            .UseInMemoryDatabase(databaseName: "Add_Test_Database_Chats")
            .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Rami12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Chat chat = new Chat { chatId = 1, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                context.Players.Add(player);
                context.Players.Add(player2);
                context.Chats.Add(chat);
                context.SaveChanges();
            }
            using (var context = new PongDbContext(options))
            {
                Assert.Equal(1, context.Chats.Count());
                Assert.Equal(1, context.Chats.First().chatId);
            }

        }
        [Fact]
        public void Modify_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "Modify_Test_Database_Chats")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Rami12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Chat chat = new Chat { chatId = 1, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                context.Players.Add(player);
                context.Players.Add(player2);
                context.Chats.Add(chat);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int chatid = 1;
                Assert.Equal(1, context.Chats.Where(n => n.chatId.Equals(chatid)).Count());

                var elementalist = context.Chats.Where(n => n.chatId.Equals(chatid)).First();
                elementalist.player1 = "Loris1";
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int chatid = 1;
                Assert.Equal(1, context.Chats.Where(n => n.chatId.Equals(chatid)).Count());
                var elementalist = context.Chats.Where(n => n.chatId.Equals(chatid)).First();
                Assert.Equal("Loris1", elementalist.player1);
            }
        }

        [Fact]
        public void Delete_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Test_Database_Chats")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Rami12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Chat chat = new Chat { chatId = 1, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                context.Players.Add(player);
                context.Players.Add(player2);
                context.Chats.Add(chat);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int chatid = 1;
                Assert.Equal(1, context.Chats.Where(n => n.chatId.Equals(chatid)).Count());

                context.Chats.Remove(context.Chats.Where(n => n.chatId.Equals(chatid)).First());
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int chatid = 1;
                Assert.NotEqual(1, context.Chats.Where(n => n.chatId.Equals(chatid)).Count());
            }

        }


        [Fact]
        public void GetAllChat_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllSkin_Test_Database_Chats")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Rami12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Chat chat = new Chat { chatId = 1, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                Chat chat2 = new Chat { chatId = 2, player1 = "Loris12345", player2 = "Rami12345", PlayerId1 = player, PlayerId2 = player2 };
                context.Players.Add(player);
                context.Players.Add(player2);
                context.Chats.Add(chat);
                context.Chats.Add(chat2);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {

                var chats = context.Chats.ToList();

                Assert.Equal(2, chats.Count);

                var classic = chats.FirstOrDefault(c => c.chatId == 1);
                Assert.NotNull(classic);
                Assert.Equal("Loris12345", classic.player1);
                Assert.Equal("Rami12345", classic.player2);

                var elementalist = chats.FirstOrDefault(c => c.chatId == 2);
                Assert.NotNull(elementalist);
                Assert.Equal("Loris12345", elementalist.player1);
                Assert.Equal("Rami12345", elementalist.player2);
            }
        }
    }
}
