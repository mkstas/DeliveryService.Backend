using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class EstablishmentAddressConfiguration : IEntityTypeConfiguration<EstablishmentAddress>
    {
        public void Configure(EntityTypeBuilder<EstablishmentAddress> builder)
        {
            builder.ToTable("establishment_addresses");

            builder.HasKey(ea => ea.Id);

            builder.Property(ea => ea.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(ea => ea.EstablishmentId)
                   .HasColumnName("establishment_id");

            builder.Property(ea => ea.Address)
                   .HasConversion(
                        a => (string)a,
                        a => StringBounded.Create(a))
                   .HasColumnName("address")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.HasOne(ea => ea.Establishment)
                   .WithMany(e => e.Addresses)
                   .HasForeignKey(ea => ea.EstablishmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
