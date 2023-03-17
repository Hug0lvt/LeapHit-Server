using DataBase.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Context
{
    public class PongDbContextWithStub : PongDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Player player1 = new() { playerId = "test", name = "Rami", timePlayed = 120, nbBallTouchTotal = 20 };
            Player player2 = new() { playerId = "test2", name = "Hugo", timePlayed = 250, nbBallTouchTotal = 90 };
            modelBuilder.Entity<Player>().HasData(player1, player2);

            Game game = new() { gameId = 1, durationGame = 65, nbMaxEchanges = 5, winner = "test", loser = "test2", scoreLoser = 2, scoreWinner = 6};
            modelBuilder.Entity<Game>().HasData(game);

            Chat chat = new() { chatId = 1, player1 = "test", player2 = "test2" };
            modelBuilder.Entity<Chat>().HasData(chat);

            Message message1 = new() { messageId = 1, message = "Salut mon gars !", player = "test", timestamp = new DateTime(2023, 02, 16, 17, 05, 12), chat = 1 };
            Message message2 = new() { messageId = 2, message = "Comment tu vas ?", player = "test2", timestamp = new DateTime(2023, 02, 16, 17, 12, 35), chat = 1 };
            modelBuilder.Entity<Message>().HasData(message1, message2);
        }
    }
}
