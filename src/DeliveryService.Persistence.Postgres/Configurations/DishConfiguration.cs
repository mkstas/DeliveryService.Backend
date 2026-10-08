using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("dishes");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(d => d.EstablishmentId)
                   .HasColumnName("establishment_id");

            builder.Property(d => d.CategoryId)
                   .HasColumnName("category_id");

            builder.Property(d => d.Name)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("name")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.Property(d => d.Description)
                   .HasConversion(
                        desc => desc != null ? (string)desc : null,
                        desc => desc != null ? StringUnbounded.Create(desc) : null)
                   .HasColumnName("description")
                   .HasDefaultValue(null);

            builder.HasOne(d => d.Establishment)
                   .WithMany()
                   .HasForeignKey(d => d.EstablishmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Category)
                   .WithMany(c => c.Dishes)
                   .HasForeignKey(d => d.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Ingredients)
                   .WithMany(i => i.Dishes)
                   .UsingEntity<Dictionary<string, object>>(
                        "dish_ingredients",
                        r => r.HasOne<Ingredient>()
                              .WithMany()
                              .HasForeignKey("ingredient_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        l => l.HasOne<Dish>()
                              .WithMany()
                              .HasForeignKey("dish_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        j => j.ToTable("dish_ingredients")
                              .HasKey("dish_id", "ingredient_id"));

            builder.HasMany(d => d.Modifiers)
                   .WithMany(m => m.Dishes)
                   .UsingEntity<Dictionary<string, object>>(
                        "dish_modifiers",
                        r => r.HasOne<Modifier>()
                              .WithMany()
                              .HasForeignKey("modifier_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        l => l.HasOne<Dish>()
                              .WithMany()
                              .HasForeignKey("dish_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        j => j.ToTable("dish_modifiers")
                              .HasKey("dish_id", "modifier_id"));

            builder.Navigation(d => d.Ingredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(d => d.Variants)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(d => d.Modifiers)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
