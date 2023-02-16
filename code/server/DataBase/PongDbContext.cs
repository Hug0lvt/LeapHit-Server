using DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class PongDbContext : DbContext
    {
        DbSet<Player> Players { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Chat> Chats { get; set; }

        public PongDbContext() { }
        public PongDbContext(DbContextOptions<PongDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=PongDB.db");
            }
        }
    }
}
