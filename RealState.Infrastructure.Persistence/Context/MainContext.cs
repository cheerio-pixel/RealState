using Microsoft.EntityFrameworkCore;

using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Context
{
    public class MainContext(DbContextOptions<MainContext> options)
        : DbContext(options)
    {
        public DbSet<SalesTypes> SalesTypes { get; set; }
        public DbSet<PropertyTypes> PropertyTypes { get; set; }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<Upgrades> Upgrades { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<PropertiesUpgrades> PropertiesUpgrades { get; set; }

        public DbSet<Pictures> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
        }
    }
}