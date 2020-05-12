using JsonImporter.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonImporter.Database
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Messages;Username=postgres;Password=06090609");
        }
    }
}
