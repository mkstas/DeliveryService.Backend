using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the <see cref="Category"/> entity.
    /// </summary>
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("id");

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasColumnName("name");

            builder.Property(c => c.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(c => c.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}

