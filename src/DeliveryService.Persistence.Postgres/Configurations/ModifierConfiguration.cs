using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class ModifierConfiguration : IEntityTypeConfiguration<Modifier>
    {
        public void Configure(EntityTypeBuilder<Modifier> builder)
        {
            builder.ToTable("modifiers");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.EstablishmentId)
                   .HasColumnName("establishment_id");

            builder.Property(m => m.Name)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("name")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.Property(m => m.Price)
                   .HasConversion(
                        p => (decimal)p,
                        p => Currency.Create(p))
                   .HasColumnName("price")
                   .IsRequired();

            builder.Property(m => m.ImageUrl)
                   .HasConversion(
                        iu => iu != null ? (string)iu : null,
                        iu => iu != null ? StringBounded.Create(iu) : null)
                   .HasColumnName("image_url")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .HasDefaultValue(null);

            builder.HasOne(m => m.Establishment)
                   .WithMany(e => e.Modifiers)
                   .HasForeignKey(m => m.EstablishmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(m => m.Dishes)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
