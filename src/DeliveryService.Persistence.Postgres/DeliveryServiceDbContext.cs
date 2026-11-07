using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres
{
    /// <summary>
    /// Represents the Entity Framework Core database context for the delivery service.
    /// </summary>
    public class DeliveryServiceDbContext(DbContextOptions<DeliveryServiceDbContext> options) : DbContext(options)
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Modifier> Modifiers { get; set; }
        public DbSet<Specification> Specifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryServiceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
