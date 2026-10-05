using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class OrderItemIngredientConfiguration : IEntityTypeConfiguration<OrderItemIngredient>
    {
        public void Configure(EntityTypeBuilder<OrderItemIngredient> builder)
        {
            builder.ToTable("order_item_ingredients");

            builder.HasKey(oii => oii.Id);

            builder.Property(oii => oii.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(oii => oii.OrderItemId)
                   .HasColumnName("order_item_id");

            builder.Property(oii => oii.IngredientId)
                   .HasColumnName("ingredient_id");

            builder.Property(oii => oii.Price)
                   .HasConversion(
                        p => (decimal)p,
                        p => Currency.Create(p))
                   .HasColumnName("price")
                   .IsRequired();

            builder.Property(oii => oii.IsAdditional)
                   .HasColumnName("is_additional");

            builder.HasOne(oii => oii.OrderItem)
                   .WithMany(oi => oi.Ingredients)
                   .HasForeignKey(oi => oi.IngredientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(oii => oii.Ingredient)
                   .WithMany(i => i.OrderItemIngredients)
                   .HasForeignKey(oii => oii.IngredientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
