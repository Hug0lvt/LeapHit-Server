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
        public async Task AddChat(Chat chat)
        {
            using (var context = new PongDbContext())
            {
                await context.Chats.AddAsync(chat);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> RemoveChat(int id)
        {
            try
            {
                using (var context = new PongDbContext())
                {
                    var chat = context.Chats.Where(c => c.chatId == id).ToList().FirstOrDefault();
                    if (chat != null)
                    {
                        var result = context.Chats.Remove(chat);
                        await context.SaveChangesAsync();
                        return result != null;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<List<Chat>> GetChats()
        {
            using (var context = new PongDbContext())
            {
                var chats = context.Chats.ToList();
                return Task.FromResult(chats);
            }
        }
        public Task<Chat> GetChat(int id)
        {
            using (var context = new PongDbContext())
            {
                var chat = context.Chats.Where(g => g.chatId == id).ToList().FirstOrDefault();
                return Task.FromResult<Chat>(chat);
            }
        }

        public Task<List<Chat>> GetChatsByIdPlayer(string id)
        {
            using (var context = new PongDbContext())
            {
                var chats = context.Chats.Where(g => g.player1 == id || g.player2 == id).ToList();
                return Task.FromResult(chats);
            }
        }

        public Task<List<Chat>> GetChatsByIdPlayers(string idPlayer1, string idPlayer2)
        {
            using (var context = new PongDbContext())
            {
                var chats = context.Chats.Where(g => (g.player1 == idPlayer1 && g.player2 == idPlayer2) || (g.player1 == idPlayer2 && g.player2 == idPlayer1)).ToList();
                return Task.FromResult(chats);
            }
        }

        public Task<int> GetNbChats()
        {
            using (var context = new PongDbContext())
            {
                var nbchats = context.Chats.ToList().Count();
                return Task.FromResult(nbchats);
            }
        }
    }
}
