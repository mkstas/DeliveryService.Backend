using DeliveryService.Domain.Common.Enums;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(o => o.UserId)
                   .HasColumnName("user_id");

            builder.Property(o => o.Cost)
                   .HasConversion(
                        c => (decimal)c,
                        c => Currency.Create(c))
                   .HasColumnName("cost")
                   .IsRequired();

            builder.Property(o => o.Address)
                   .HasConversion(
                        a => (string)a,
                        a => StringBounded.Create(a))
                   .HasColumnName("address")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.Property(o => o.Comment)
                   .HasConversion(
                        c => c != null ? (string)c : null,
                        c => c != null ? StringUnbounded.Create(c) : null)
                   .HasColumnName("comment")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .HasDefaultValue(null);

            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .HasColumnName("status")
                   .HasDefaultValue(OrderStatus.Created);

            builder.Property(o => o.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired();

            builder.Property(o => o.UpdatedAt)
                   .HasColumnName("updated_at");

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(o => o.OrderItems)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
