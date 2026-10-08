using DeliveryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("carts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.UserId)
                   .HasColumnName("user_id");

            builder.HasOne(c => c.User)
                   .WithOne(u => u.Cart)
                   .HasForeignKey<Cart>(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.Items)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
