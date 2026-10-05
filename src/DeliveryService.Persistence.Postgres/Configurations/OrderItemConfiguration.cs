using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_items");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(oi => oi.OrderId)
                   .HasColumnName("order_id");

            builder.Property(oi => oi.MenuItemVariantId)
                   .HasColumnName("menu_item_variant_id");

            builder.Property(oi => oi.Price)
                   .HasConversion(
                        p => (decimal)p,
                        p => Currency.Create(p))
                   .HasColumnName("price")
                   .IsRequired();

            builder.Property(oi => oi.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey("order_id")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(oi => oi.Ingredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
