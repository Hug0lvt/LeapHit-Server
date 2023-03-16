using DataBase.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

namespace DataBase.Context
{
    public class PongDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public PongDbContext() { }
        public PongDbContext(DbContextOptions<PongDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\..\\DataBase\\PongDB.db");
                optionsBuilder.UseSqlite($"Data Source={path}");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<Chat>().ToTable("Chats");

        }
    }
}
