using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_items");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                   .HasColumnName("id");

            builder.Property(ci => ci.UserId)
                   .IsRequired()
                   .HasColumnName("user_id");

            builder.Property(ci => ci.DishId)
                   .IsRequired()
                   .HasColumnName("dish_id");

            builder.Property(ci => ci.Quantity)
                   .IsRequired()
                   .HasColumnName("quantity");

            builder.Property(ci => ci.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(ci => ci.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasIndex(ci => ci.UserId)
                   .HasDatabaseName("IX_cart_items_user_id");

            builder.HasOne(ci => ci.User)
                   .WithMany()
                   .HasForeignKey(ci => ci.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Dish)
                   .WithMany(d => d.CartItems)
                   .HasForeignKey(ci => ci.DishId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
