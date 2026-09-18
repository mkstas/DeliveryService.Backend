using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .HasColumnName("id");

            builder.Property(r => r.Name)
                   .HasConversion(
                        n => n,
                        n => StringBounded.Create(n))
                   .HasColumnName("name")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasMany(r => r.Permissions)
                   .WithMany(p => p.Roles)
                   .UsingEntity<Dictionary<string, object>>(
                        "role_permissions",
                        l => l.HasOne<Permission>()
                              .WithMany()
                              .HasForeignKey("permission_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        r => r.HasOne<Role>()
                              .WithMany()
                              .HasForeignKey("role_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        j => j.ToTable("role_permissions")
                              .HasKey("permission_id", "role_id"));

            builder.Navigation(r => r.Users)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(r => r.Permissions)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
