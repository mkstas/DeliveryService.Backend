using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_items");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(ci => ci.CartId)
                   .HasColumnName("cart_id");

            builder.Property(ci => ci.MenuItemVariantId)
                   .HasColumnName("menu_item_variant_id");

            builder.Property(ci => ci.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();

            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.CartItems)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.MenuItemVariant)
                   .WithMany(miv => miv.CartItems)
                   .HasForeignKey(ci => ci.MenuItemVariantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ci => ci.Ingredients)
                   .WithMany(i => i.CartItems)
                   .UsingEntity<Dictionary<string, object>>(
                        "cart_item_ingredients",
                        l => l.HasOne<Ingredient>()
                              .WithMany()
                              .HasForeignKey("ingredient_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        r => r.HasOne<CartItem>()
                              .WithMany()
                              .HasForeignKey("cart_item_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        j => j.ToTable("cart_item_ingredients")
                              .HasKey("ingredient_id", "cart_item_ingredients"));
        }
    }
}
