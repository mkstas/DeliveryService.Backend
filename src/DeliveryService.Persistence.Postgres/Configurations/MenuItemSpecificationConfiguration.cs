using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class MenuItemSpecificationConfiguration : IEntityTypeConfiguration<MenuItemSpecification>
    {
        public void Configure(EntityTypeBuilder<MenuItemSpecification> builder)
        {
            builder.ToTable("menu_item_specifications");

            builder.HasKey(mis => mis.Id);

            builder.Property(mis => mis.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(mis => mis.MenuItemVariantId)
                   .HasColumnName("menu_item_variant_id");

            builder.Property(mis => mis.SpecificationId)
                   .HasColumnName("specification_id");

            builder.Property(mis => mis.Value)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("value")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.HasOne(mis => mis.MenuItemVariant)
                   .WithMany(miv => miv.Specifications)
                   .HasForeignKey(mis => mis.MenuItemVariantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mis => mis.Specification)
                   .WithMany(miv => miv.MenuItemSpecifications)
                   .HasForeignKey(mis => mis.SpecificationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
