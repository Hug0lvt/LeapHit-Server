using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class PongDbContext : DbContext
    {
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
