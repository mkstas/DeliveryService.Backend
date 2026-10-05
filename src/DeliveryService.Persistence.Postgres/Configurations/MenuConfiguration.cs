using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("menus");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.EstablishmentId)
                   .HasColumnName("establishment_id");

            builder.HasOne(m => m.Establishment)
                   .WithOne(e => e.Menu)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(m => m.MenuItems)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
