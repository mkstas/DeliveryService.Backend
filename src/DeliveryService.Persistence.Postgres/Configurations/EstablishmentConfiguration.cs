using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class EstablishmentConfiguration : IEntityTypeConfiguration<Establishment>
    {
        public void Configure(EntityTypeBuilder<Establishment> builder)
        {
            builder.ToTable("establishments");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("id");

            builder.Property(e => e.Name)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("name")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.Property(e => e.BusinessAccount)
                   .HasConversion(
                        ba => (string)ba,
                        ba => BankAccount.Create(ba))
                   .HasColumnName("business_account")
                   .HasMaxLength(BankAccount.LENGTH)
                   .IsRequired();

            builder.HasIndex(e => e.BusinessAccount).IsUnique();

            builder.Navigation(e => e.Ingredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(e => e.Addresses)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(e => e.Users)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
