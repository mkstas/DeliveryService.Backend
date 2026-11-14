using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the <see cref="Role"/> entity.
    /// </summary>
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .HasColumnName("id");

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasColumnName("name");

            builder.Property(r => r.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(r => r.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasMany(r => r.Permissions)
                   .WithMany(p => p.Roles)
                   .UsingEntity<Dictionary<string, object>>(
                       "role_permissions",
                       l => l.HasOne<Permission>()
                             .WithMany()
                             .HasForeignKey("permission_id")
                             .OnDelete(DeleteBehavior.Restrict),
                       r => r.HasOne<Role>()
                             .WithMany()
                             .HasForeignKey("role_id")
                             .OnDelete(DeleteBehavior.Restrict),
                       j => j.ToTable("role_permissions")
                             .HasKey("permission_id", "role_id"));

            builder.Navigation(r => r.Users)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(r => r.Permissions)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
