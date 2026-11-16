using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the <see cref="Payment"/> entity.
    /// </summary>
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.UserId)
                   .IsRequired()
                   .HasColumnName("user_id");

            builder.Property(p => p.CardNumber)
                    .IsRequired()
                    .HasMaxLength(Payment.CARD_NUMBER_LENGTH)
                    .HasColumnName("card_number");

            builder.HasIndex(p => p.CardNumber).IsUnique();

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Payments)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

