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

            builder.HasOne(i => i.Establishment)
                   .WithMany(e => e.Ingredients)
                   .HasForeignKey(i => i.EstablishmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(i => i.Dishes)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
