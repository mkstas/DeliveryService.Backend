using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class DishSpecificationConfiguration : IEntityTypeConfiguration<DishSpecification>
    {
        public void Configure(EntityTypeBuilder<DishSpecification> builder)
        {
            builder.ToTable("dish_specifications");

            builder.HasKey(ds => ds.Id);

            builder.Property(ds => ds.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(ds => ds.DishVariantId)
                   .HasColumnName("dish_variant_id");

            builder.Property(ds => ds.SpecificationId)
                   .HasColumnName("specification_id");

            builder.Property(ds => ds.Value)
                   .HasConversion(
                        v => (string)v,
                        v => StringBounded.Create(v))
                   .HasColumnName("value")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.HasOne(ds => ds.DishVariant)
                   .WithMany(dv => dv.Specifications)
                   .HasForeignKey(ds => ds.DishVariantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ds => ds.Specification)
                   .WithMany(s => s.DishSpecifications)
                   .HasForeignKey(ds => ds.SpecificationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
