using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("dishes");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .HasColumnName("id");

            builder.Property(d => d.RestaurantId)
                   .IsRequired()
                   .HasColumnName("restaurant_id");

            builder.Property(d => d.CategoryId)
                   .IsRequired()
                   .HasColumnName("category_id");

            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasColumnName("name");

            builder.Property(d => d.Price)
                   .IsRequired()
                   .HasColumnName("price");

            builder.Property(d => d.Description)
                   .HasColumnName("description");

            builder.Property(d => d.ImageUrl)
                   .HasDefaultValue(null)
                   .HasColumnName("image_url");

            builder.Property(d => d.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(d => d.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasOne(d => d.Restaurant)
                   .WithMany()
                   .HasForeignKey(d => d.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Category)
                   .WithMany()
                   .HasForeignKey(d => d.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(d => d.Specifications)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(d => d.Ingredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(d => d.CartItems)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
