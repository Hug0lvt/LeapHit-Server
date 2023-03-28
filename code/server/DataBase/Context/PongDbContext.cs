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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<Chat>().ToTable("Chats");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseNpgsql(@"host=localhost;database=postgres;user id=postgres;password=1234;");
                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\..\\DataBase\\PongDB.db");
                //optionsBuilder.UseSqlite($"Data Source={path}");
                var dbDatabase = Environment.GetEnvironmentVariable("MARIADB_DATABASE", EnvironmentVariableTarget.Process);
                var dbUser = Environment.GetEnvironmentVariable("MARIADB_USER", EnvironmentVariableTarget.Process);
                var dbPassword = Environment.GetEnvironmentVariable("MARIADB_PASSWORD", EnvironmentVariableTarget.Process);
                var dbServer = Environment.GetEnvironmentVariable("DB_SERVER", EnvironmentVariableTarget.Process);
                Debug.WriteLine(dbPassword);
                optionsBuilder.UseMySql($"server=leap-hit-team-mysql;port=3306;user=leaphit;password=leaphit;database=leaphit", new MySqlServerVersion(new Version(10, 11, 1)));
                

            }
        }
    }
}
