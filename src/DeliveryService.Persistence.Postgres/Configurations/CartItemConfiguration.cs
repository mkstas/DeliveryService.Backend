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

            builder.Property(ci => ci.DishVariantId)
                   .HasColumnName("dish_variant_id");

            builder.Property(ci => ci.Quantity)
                   .HasConversion(q => (int)q, q => (uint)q)
                   .HasColumnName("quantity")
                   .IsRequired();

            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.Items)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.DishVariant)
                   .WithMany()
                   .HasForeignKey(ci => ci.DishVariantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ci => ci.Modifiers)
                   .WithMany()
                   .UsingEntity<Dictionary<string, object>>(
                        "cart_item_modifiers",
                        r => r.HasOne<Modifier>()
                              .WithMany()
                              .HasForeignKey("modifier_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        l => l.HasOne<CartItem>()
                              .WithMany()
                              .HasForeignKey("cart_item_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        j => j.ToTable("cart_item_modifiers")
                              .HasKey("cart_item_id", "modifier_id"));

            builder.Navigation(ci => ci.Modifiers)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
