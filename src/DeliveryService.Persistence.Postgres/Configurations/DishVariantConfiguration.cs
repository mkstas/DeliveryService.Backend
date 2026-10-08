using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class DishVariantConfiguration : IEntityTypeConfiguration<DishVariant>
    {
        public void Configure(EntityTypeBuilder<DishVariant> builder)
        {
            builder.ToTable("dish_variants");

            builder.HasKey(dv => dv.Id);

            builder.Property(dv => dv.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(dv => dv.DishId)
                   .HasColumnName("dish_id");

            builder.Property(dv => dv.Price)
                   .HasConversion(
                        p => (decimal)p,
                        p => Currency.Create(p))
                   .HasColumnName("price")
                   .IsRequired();

            builder.Property(dv => dv.ImageUrl)
                   .HasConversion(
                        iu => iu != null ? (string)iu : null,
                        iu => iu != null ? StringBounded.Create(iu) : null)
                   .HasColumnName("image_url")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .HasDefaultValue(null);

            builder.HasOne(dv => dv.Dish)
                   .WithMany(d => d.Variants)
                   .HasForeignKey(dv => dv.DishId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(dv => dv.Specifications)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
