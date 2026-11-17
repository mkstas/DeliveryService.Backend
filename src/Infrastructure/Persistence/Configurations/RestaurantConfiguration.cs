using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the <see cref="Restaurant"/> entity.
    /// </summary>
    internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("restaurants");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .HasColumnName("id");

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasColumnName("name");

            builder.Property(r => r.ImageUrl)
                   .HasDefaultValue(null)
                   .HasColumnName("image_url");

            builder.Property(r => r.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(r => r.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.Navigation(r => r.Users)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(r => r.Addresses)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

