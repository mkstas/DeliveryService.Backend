using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("menu_items");

            builder.HasKey(mi => mi.Id);

            builder.Property(mi => mi.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(mi => mi.MenuId)
                   .HasColumnName("menu_id");

            builder.Property(mi => mi.CategoryId)
                   .HasColumnName("category_id");

            builder.Property(mi => mi.Name)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("name")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.Property(mi => mi.Description)
                   .HasConversion(
                        d => d != null ? (string)d : null,
                        d => d != null ? StringUnbounded.Create(d) : null)
                   .HasColumnName("description")
                   .HasDefaultValue(null);

            builder.HasOne(mi => mi.Menu)
                   .WithMany(m => m.MenuItems)
                   .HasForeignKey(mi => mi.MenuId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mi => mi.Category)
                   .WithMany(c => c.MenuItems)
                   .HasForeignKey(mi => mi.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(mi => mi.Ingredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(mi => mi.Variants)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
