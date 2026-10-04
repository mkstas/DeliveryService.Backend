using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("payment_methods");

            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(pm => pm.UserId)
                   .HasColumnName("user_id");

            builder.Property(pm => pm.CardNumber)
                   .HasConversion(
                        cn => (string)cn,
                        cn => CardNumber.Create(cn))
                   .HasColumnName("card_number")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.HasIndex(pm => pm.CardNumber).IsUnique();

            builder.HasOne(pm => pm.User)
                   .WithMany(u => u.PaymentMethods)
                   .HasForeignKey(pm => pm.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
