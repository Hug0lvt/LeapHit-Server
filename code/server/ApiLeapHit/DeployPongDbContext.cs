using DataBase.Context;
using DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiLeapHit
{
    public class DeployPongDbContext : DbContext
    {

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public DeployPongDbContext(DbContextOptions<DeployPongDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString("AppDb");
                optionsBuilder.UseSqlite(connectionString);
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
