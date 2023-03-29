using DataBase.Context;
using DataBase.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestsDataBase
{
    public class TestGame
    {
        [Fact]
        public void Add_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
            .UseInMemoryDatabase(databaseName: "Add_Test_Database_Game")
            .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player playerRami = new Player { playerId = "Rami12345", name = "The Lady Rami", nbBallTouchTotal = 9, timePlayed = 3 };
                Game game = new Game { gameId = 1, durationGame = 2, nbMaxEchanges = 8, loser = "Loris12345", winner = "Rami12345", scoreLoser = 2, scoreWinner = 6, PlayerLoser = player, PlayerWinner = playerRami };
                context.Players.Add(player);
                context.Players.Add(playerRami);
                context.Games.Add(game);
                context.SaveChanges();
            }
            using (var context = new PongDbContext(options))
            {
                Assert.Equal(1, context.Games.Count());
                Assert.Equal(1, context.Games.First().gameId);
            }

        }
        [Fact]
        public void Modify_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "Modify_Test_Database_Games")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player playerRami = new Player { playerId = "Rami12345", name = "The Lady Rami", nbBallTouchTotal = 9, timePlayed = 3 };
                Game game = new Game { gameId = 1, durationGame = 2, nbMaxEchanges = 8, loser = "Loris12345", winner = "Rami12345", scoreLoser = 2, scoreWinner = 6, PlayerLoser = player, PlayerWinner = playerRami };
                context.Players.Add(player);
                context.Players.Add(playerRami);
                context.Games.Add(game);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int idPong = 1;
                Assert.Equal(1, context.Games.Where(n => n.gameId.Equals(idPong)).Count());

                var elementalist = context.Games.Where(n => n.gameId.Equals(idPong)).First();
                elementalist.nbMaxEchanges = 6;
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int idPong = 1;
                Assert.Equal(1, context.Games.Where(n => n.gameId.Equals(idPong)).Count());
                var elementalist = context.Games.Where(n => n.gameId.Equals(idPong)).First();
                Assert.Equal(6, elementalist.nbMaxEchanges);
            }
        }

        [Fact]
        public void Delete_Test()
        {
            var options = new DbContextOptionsBuilder<PongDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Test_Database_Games")
                .Options;

            using (var context = new PongDbContext(options))
            {
                Player player = new Player { playerId = "Loris12345", name = "The Lady", nbBallTouchTotal = 8, timePlayed = 2 };
                Player playerRami = new Player { playerId = "Rami12345", name = "The Lady Rami", nbBallTouchTotal = 9, timePlayed = 3 };
                Game game = new Game { gameId = 1, durationGame = 2, nbMaxEchanges = 8, loser = "Loris12345", winner = "Rami12345", scoreLoser = 2, scoreWinner = 6, PlayerLoser = player, PlayerWinner = playerRami };
                context.Players.Add(player);
                context.Players.Add(playerRami);
                context.Games.Add(game);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int idPong = 1;
                Assert.Equal(1, context.Games.Where(n => n.gameId.Equals(idPong)).Count());

                context.Games.Remove(context.Games.Where(n => n.gameId.Equals(idPong)).First());
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {
                int idPong = 1;
                Assert.NotEqual(1, context.Games.Where(n => n.gameId.Equals(idPong)).Count());
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
                Player playerRami = new Player { playerId = "Rami12345", name = "The Lady Rami", nbBallTouchTotal = 9, timePlayed = 3 };
                Game game = new Game { gameId = 1, durationGame = 2, nbMaxEchanges = 8, loser = "Loris12345", winner = "Rami12345", scoreLoser = 2, scoreWinner = 6, PlayerLoser = player, PlayerWinner = playerRami };
                Game game2 = new Game { gameId = 2, durationGame = 2, nbMaxEchanges = 8, loser = "Loris12345", winner = "Rami12345", scoreLoser = 2, scoreWinner = 6, PlayerLoser = player, PlayerWinner = playerRami };
                context.Players.Add(player);
                context.Players.Add(playerRami);
                context.Games.Add(game);
                context.Games.Add(game2);
                context.SaveChanges();
            }

            using (var context = new PongDbContext(options))
            {

                var games = context.Games.ToList();

                Assert.Equal(2, games.Count);

                var classic = games.FirstOrDefault(c => c.gameId == 1);
                Assert.NotNull(classic);
                Assert.Equal(2, classic.durationGame);
                Assert.Equal(2, classic.scoreLoser);

                var elementalist = games.FirstOrDefault(c => c.gameId == 2);
                Assert.NotNull(elementalist);
                Assert.Equal(2, elementalist.durationGame);
                Assert.Equal(2, elementalist.scoreLoser);
            }
        }
    }
    }
