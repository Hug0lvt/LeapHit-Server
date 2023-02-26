using DataBase.Context;
using DataBase.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DataManager
{
    public partial class DbDataManager
    {
        public async Task AddPlayer(Player player)
        {
            using (var context = new PongDbContext())
            {
                await context.Players.AddAsync(player);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> RemovePlayer(int id)
        {
            using (var context = new PongDbContext())
            {
                var player = context.Players.Where(p => p.playerId == id).ToList().FirstOrDefault();
                if (player != null) 
                { 
                    var result = context.Players.Remove(player);
                    await context.SaveChangesAsync();
                    return result != null;
                }
                return false;
            }
        }

        public async Task<Player> UpdatePlayer(int id, string newName)
        {
            using (var context = new PongDbContext())
            {
                var player = context.Players.Where(p => p.playerId == id).ToList().FirstOrDefault();
                if (player != null)
                {
                    player.name = newName;
                }
                await   context.SaveChangesAsync();
                return player;
            }
        }

        public Task<Player> GetPlayer(int id) 
        {
            using (var context = new PongDbContext())
            {
                var player = context.Players.Where(p => p.playerId == id).ToList().FirstOrDefault();
                return Task.FromResult<Player>(player);
            }
        }

        public Task<List<Player>> GetPlayers()
        {
            using (var context = new PongDbContext())
            {
                var players = context.Players.ToList();
                return Task.FromResult(players);
            }
        }
    }
}
