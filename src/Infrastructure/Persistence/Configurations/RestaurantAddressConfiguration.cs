using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the <see cref="RestaurantAddress"/> entity.
    /// </summary>
    internal class RestaurantAddressConfiguration : IEntityTypeConfiguration<RestaurantAddress>
    {
        public void Configure(EntityTypeBuilder<RestaurantAddress> builder)
        {
            builder.ToTable("restaurant_addresses");

            builder.HasKey(ra => ra.Id);

            builder.Property(ra => ra.Id)
                   .HasColumnName("id");

            builder.Property(ra => ra.RestaurantId)
                   .IsRequired()
                   .HasColumnName("restaurant_id");

            builder.Property(ra => ra.Address)
                   .IsRequired()
                   .HasColumnName("address");

            builder.Property(ra => ra.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(ra => ra.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasOne(ra => ra.Restaurant)
                   .WithMany(r => r.Addresses)
                   .HasForeignKey(ra => ra.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

