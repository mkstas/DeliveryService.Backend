using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Represents the database context.
    /// </summary>
    /// <param name="options">The options used to configure the database context.</param>
    public class PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// The set of permissions.
        /// </summary>
        public DbSet<Permission> Permissions { get; init; }

        /// <summary>
        /// The set of roles.
        /// </summary>
        public DbSet<Role> Roles { get; init; }

        /// <summary>
        /// The set of users.
        /// </summary>
        public DbSet<User> Users { get; init; }

        /// <summary>
        /// The set of restaurants.
        /// </summary>
        public DbSet<Restaurant> Restaurants { get; init; }

        /// <summary>
        /// The set of categories.
        /// </summary>
        public DbSet<Category> Categories { get; init; }

        /// <summary>
        /// The set of specifications.
        /// </summary>
        public DbSet<Specification> Specifications { get; init; }

        /// <summary>
        /// The set of ingredients.
        /// </summary>
        public DbSet<Ingredient> Ingredients { get;  init; }

        /// <summary>
        /// The set of dishes.
        /// </summary>
        public DbSet<Dish> Dishes { get; init; }

        /// <summary>
        /// The set of dish specifications.
        /// </summary>
        public DbSet<DishSpecification> DishSpecifications { get; init; }

        /// <summary>
        /// The set of dish ingredients.
        /// </summary>
        public DbSet<DishIngredient> DishIngredients { get; init; }

        /// <summary>
        /// The set of cart items.
        /// </summary>
        public DbSet<CartItem> CartItems { get; init; }

        /// <summary>
        /// The set of orders.
        /// </summary>
        public DbSet<Order> Orders { get; init; }

        /// <summary>
        /// Applies the entity configurations from the current assembly.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
