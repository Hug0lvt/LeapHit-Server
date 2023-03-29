using DataBase.Context;
using DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace TestsDataBase
{
    public class TestPlayer
    {
        [Fact]
        public void Add_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
            .UseInMemoryDatabase(databaseName: "Add_Test_Database_Player")
            .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                context.Players.Add(player);
                context.SaveChanges();
            }
            using (var context = new PongDbContext(options))
            {
                Assert.Equal(1, context.Players.Count());
                Assert.Equal("The Lady", context.Players.First().name);
            }

        }
        [Fact]
        public void Modify_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "Modify_Test_Database_Player")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                context.Players.Add(player);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                string nameToFind = "the lady";
                Assert.Equal(1, context.Players.Where(n => n.name.ToLower().Contains(nameToFind)).Count());

                var elementalist = context.Players.Where(n => n.name.ToLower().Contains(nameToFind)).First();
                elementalist.nbBallTouchTotal = 8;
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                string nameToFind = "the lady";
                Assert.Equal(1, context.Players.Where(n => n.name.ToLower().Contains(nameToFind)).Count());
                var elementalist = context.Players.Where(n => n.name.ToLower().Contains(nameToFind)).First();
                Assert.Equal(8, elementalist.nbBallTouchTotal);
            }
        }

        [Fact]
        public void Delete_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Test_Database_Players")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                context.Players.Add(player);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                string nameToFind = "the lady";
                Assert.Equal(1, context.Players.Where(n => n.name.ToLower().Contains(nameToFind)).Count());

                context.Players.Remove(context.Players.Where(n => n.name.ToLower().Contains(nameToFind)).First());
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                string nameToFind = "the lady";
                Assert.NotEqual(1, context.Players.Where(n => n.name.ToLower().Contains(nameToFind)).Count());
            }

        }


        [Fact]
        public void GetAllSkin_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllSkin_Test_Database_Skins")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player player2 = new Player { playerId = "Noan12345", name = "The Lady Noan", nbBallTouchTotal = 9, timePlayed = 5 };
                context.Players.Add(player);
                context.Players.Add(player2);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {

                var players = context.Players.ToList();

                Assert.Equal(2, players.Count);

                var classic = players.FirstOrDefault(c => c.name == "The Lady Noan");
                Assert.NotNull(classic);
                Assert.Equal(9, classic.nbBallTouchTotal);
                Assert.Equal(5, classic.timePlayed);

                var elementalist = players.FirstOrDefault(c => c.name == "The Lady");
                Assert.NotNull(elementalist);
                Assert.Equal(8, elementalist.nbBallTouchTotal);
                Assert.Equal(2, elementalist.timePlayed);
            }
        }
    }
}