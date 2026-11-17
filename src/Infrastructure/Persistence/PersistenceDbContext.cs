using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Represents the database context.
    /// </summary>
    public class PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : DbContext(options)
    {
        public DbSet<Permission> Permissions { get; init; }
        public DbSet<Role> Roles { get; init; }
        public DbSet<User> Users { get; init; }
        public DbSet<Restaurant> Restaurants { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Specification> Specifications { get; init; }
        public DbSet<Dish> Dishes { get; init; }
        public DbSet<DishSpecification> DishSpecifications { get; init; }
        public DbSet<CartItem> CartItems { get; init; }
        public DbSet<Order> Orders { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
