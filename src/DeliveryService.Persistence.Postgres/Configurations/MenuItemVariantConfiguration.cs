using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class MenuItemVariantConfiguration : IEntityTypeConfiguration<MenuItemVariant>
    {
        public void Configure(EntityTypeBuilder<MenuItemVariant> builder)
        {
            builder.ToTable("menu_item_variants");

            builder.HasKey(miv => miv.Id);

            builder.Property(miv => miv.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(miv => miv.MenuItemId)
                   .HasColumnName("menu_item_id");

            builder.Property(miv => miv.ImageUrl)
                   .HasConversion(
                        iu => iu != null ? (string)iu : null,
                        iu => iu != null ? StringBounded.Create(iu) : null)
                   .HasColumnName("image_url")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .HasDefaultValue(null);

            builder.Property(miv => miv.Price)
                   .HasConversion(
                        p => (decimal)p,
                        p => Currency.Create(p))
                   .HasColumnName("price");

            builder.HasOne(miv => miv.MenuItem)
                   .WithMany(mi => mi.Variants)
                   .HasForeignKey(mii => mii.MenuItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(miv => miv.Specifications)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
