using DataBase.Context;
using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DataManager
{
    public partial class DbDataManager
    {
        public async Task AddGame(Game game)
        {
            using (var context = new PongDbContext())
            {
                await context.Games.AddAsync(game);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> RemoveGame(int id)
        {
            using (var context = new PongDbContext())
            {
                var game = context.Games.Where(g => g.gameId == id).ToList().FirstOrDefault();
                if (game != null)
                {
                    var result = context.Games.Remove(game);
                    await context.SaveChangesAsync();
                    return result != null;
                }
                return false;
            }
        }

        public Task<Game> GetGame(int id)
        {
            using (var context = new PongDbContext())
            {
                var game = context.Games.Where(g => g.gameId == id).ToList().FirstOrDefault();
                return Task.FromResult<Game>(game);
            }
        }
    }
}
