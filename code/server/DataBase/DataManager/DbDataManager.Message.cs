using DataBase.Context;
using DataBase.Entity;
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
        public async Task SendMessage(Message message)
        {
            using (var context = new PongDbContext())
            {
                await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
            }
        }

        public Task<Message> ReceiveMessage(int id)
        {
            using (var context = new PongDbContext())
            {
                var message = context.Messages.Where(m => m.messageId == id).ToList().FirstOrDefault();
                return Task.FromResult<Message>(message);
            }
        }

        public async Task<bool> RemoveMessage(int id)
        {
            using (var context = new PongDbContext())
            {
                var message = context.Messages.Where(m => m.messageId == id).ToList().FirstOrDefault();
                if (message != null)
                {
                    var result = context.Messages.Remove(message);
                    await context.SaveChangesAsync();
                    return result != null;
                }
                return false;
            }
        }

        public Task<List<Message>> GetMessages()
        {
            using (var context = new PongDbContext())
            {
                var messages = context.Messages.ToList();
                return Task.FromResult(messages);
            }
        }
    }
}
