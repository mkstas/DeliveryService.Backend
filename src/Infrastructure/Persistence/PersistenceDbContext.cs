using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Represents the database context.
    /// </summary>
    public class PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// The permissions table.
        /// </summary>
        public DbSet<Permission> Permissions { get; init; }

        /// <summary>
        /// The roles table.
        /// </summary>
        public DbSet<Role> Roles { get; init; }

        /// <summary>
        /// The users table.
        /// </summary>
        public DbSet<User> Users { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
