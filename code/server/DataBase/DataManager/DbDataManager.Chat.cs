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
            }
        }

        public Task<bool> RemoveChat(int id)
        {
            using (var context = new PongDbContext())
            {
                var chat = context.Chats.Where(c => c.chatId == id).ToList().FirstOrDefault();
                if (chat != null)
                {
                    var result = context.Chats.Remove(chat);
                    return Task.FromResult(result != null);
                }
                return Task.FromResult(false);
            }
        }
    }
}
