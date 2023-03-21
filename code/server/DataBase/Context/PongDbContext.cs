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
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseNpgsql(@"host=localhost;database=postgres;user id=postgres;password=1234;");
                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\..\\DataBase\\PongDB.db");
                //optionsBuilder.UseSqlite($"Data Source={path}");
                optionsBuilder.UseMySql("server=localhost;port=3306;user=user;password=pwd;database=mydb", new MySqlServerVersion(new Version(10, 11, 1)));

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
