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

            builder.HasMany(u => u.Users)
                   .WithMany(e => e.Establishments)
                   .UsingEntity<Dictionary<string, object>>(
                        "user_establishments",
                        l => l.HasOne<User>()
                              .WithMany()
                              .HasForeignKey("user_id")
                              .OnDelete(DeleteBehavior.Restrict),
                        r => r.HasOne<Establishment>()
                              .WithMany()
                              .HasForeignKey("establishment_id")
                              .OnDelete(DeleteBehavior.Restrict),
                        j => j.ToTable("establishment_users")
                              .HasKey("user_id", "establishment_id"));

            builder.Navigation(e => e.Ingredients)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(e => e.Addresses)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(e => e.Users)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
