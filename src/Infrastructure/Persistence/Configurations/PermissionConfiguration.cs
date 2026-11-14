using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the <see cref="Permission"/> entity.
    /// </summary>
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("id");

            builder.Property(p => p.SystemName)
                   .IsRequired()
                   .HasColumnName("system_name");

            builder.Property(p => p.DisplayName)
                   .IsRequired()
                   .HasColumnName("display_name");

            builder.Property(p => p.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(p => p.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasIndex(p => p.SystemName).IsUnique();

            builder.Navigation(p => p.Roles)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
