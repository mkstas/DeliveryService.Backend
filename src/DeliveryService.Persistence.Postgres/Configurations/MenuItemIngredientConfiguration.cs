using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class MenuItemIngredientConfiguration : IEntityTypeConfiguration<MenuItemIngredient>
    {
        public void Configure(EntityTypeBuilder<MenuItemIngredient> builder)
        {
            builder.ToTable("menu_item_ingredients");

            builder.HasKey(mii => mii.Id);

            builder.Property(mii => mii.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(mii => mii.MenuItemId)
                   .HasColumnName("menu_item_id");

            builder.Property(mii => mii.IngredientId)
                   .HasColumnName("ingredient_id");

            builder.Property(mii => mii.Price)
                   .HasConversion(
                        p => (decimal)p,
                        p => Currency.Create(p))
                   .HasColumnName("price");

            builder.Property(mii => mii.IsAdditional)
                   .HasColumnName("is_additional")
                   .HasDefaultValue(false);

            builder.HasOne(mii => mii.MenuItem)
                   .WithMany(mi => mi.Ingredients)
                   .HasForeignKey(mii => mii.MenuItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mii => mii.Ingredient)
                   .WithMany(mi => mi.MenuItemIngredients)
                   .HasForeignKey(mii => mii.IngredientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
