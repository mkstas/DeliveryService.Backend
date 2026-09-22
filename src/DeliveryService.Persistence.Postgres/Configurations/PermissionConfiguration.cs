using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("id");

            builder.Property(p => p.SystemName)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("system_name")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.HasIndex(p => p.SystemName).IsUnique();

            builder.Property(p => p.DisplayName)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("display_name")
                   .HasMaxLength(StringBounded.MAX_LENGTH);

            builder.HasIndex(p => p.DisplayName).IsUnique();

            builder.Navigation(p => p.Roles)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
