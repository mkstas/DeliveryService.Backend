using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("ingredients");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(i => i.EstablishmentId)
                   .HasColumnName("establishment_id");

            builder.Property(i => i.Name)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("name")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.Property(i => i.ImageUrl)
                   .HasConversion(
                        iu => iu != null ? (string)iu : null,
                        iu => iu != null ? StringBounded.Create(iu) : null)
                   .HasColumnName("image_url")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .HasDefaultValue(null);

            builder.Property(i => i.IsActive)
                   .HasColumnName("is_active")
                   .HasDefaultValue(true);

            builder.Navigation(i => i.MenuItemIngredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(i => i.CartItems)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
