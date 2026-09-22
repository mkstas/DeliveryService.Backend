using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Persistence.Postgres
{
    public class DeliveryServiceDbContext(DbContextOptions<DeliveryServiceDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryServiceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
