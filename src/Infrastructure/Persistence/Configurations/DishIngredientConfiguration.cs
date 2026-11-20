using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class DishIngredientConfiguration : IEntityTypeConfiguration<DishIngredient>
    {
        public void Configure(EntityTypeBuilder<DishIngredient> builder)
        {
            builder.ToTable("dish_ingredients");

            builder.HasKey(di => di.Id);

            builder.Property(di => di.Id)
                   .HasColumnName("id");

            builder.Property(di => di.DishId)
                   .IsRequired()
                   .HasColumnName("dish_id");

            builder.Property(di => di.IngredientId)
                   .IsRequired()
                   .HasColumnName("ingredient_id");

            builder.Property(di => di.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(di => di.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasIndex(di => new { di.DishId, di.IngredientId }).IsUnique();

            builder.HasOne(di => di.Dish)
                   .WithMany(d => d.Ingredients)
                   .HasForeignKey(di => di.DishId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(di => di.Ingredient)
                   .WithMany()
                   .HasForeignKey(di => di.IngredientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
