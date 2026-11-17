using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the <see cref="Ingredient"/> entity.
    /// </summary>
    internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("ingredients");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                   .ValueGeneratedOnAdd()
                   .HasColumnName("id");

            builder.Property(i => i.RestaurantId)
                   .IsRequired()
                   .HasColumnName("restaurant_id");

            builder.Property(i => i.Name)
                   .IsRequired()
                   .HasColumnName("name");

            builder.Property(i => i.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(i => i.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasOne(i => i.Restaurant)
                   .WithMany(r => r.Ingredients)
                   .HasForeignKey(i => i.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

