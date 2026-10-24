using DeliveryService.Domain.Entities;
using DeliveryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Persistence.Postgres.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("id");

            builder.Property(u => u.EmailAddress)
                   .HasConversion(
                        ea => (string)ea,
                        ea => EmailAddress.Create(ea))
                   .HasColumnName("email_address")
                   .HasMaxLength(EmailAddress.MAX_LENGTH)
                   .IsRequired();

            builder.HasIndex(u => u.EmailAddress).IsUnique();

            builder.Property(u => u.PasswordHash)
                   .HasConversion(
                        ph => (string)ph,
                        ph => StringUnbounded.Create(ph))
                   .HasColumnName("password_hash")
                   .IsRequired();

            builder.Property(u => u.Name)
                   .HasConversion(
                        n => (string)n,
                        n => StringBounded.Create(n))
                   .HasColumnName("name")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .IsRequired();

            builder.Property(u => u.ImageUrl)
                   .HasConversion(
                        iu => iu != null ? (string)iu : null,
                        iu => iu != null ? StringBounded.Create(iu) : null)
                   .HasColumnName("image_url")
                   .HasMaxLength(StringBounded.MAX_LENGTH)
                   .HasDefaultValue(null);

            builder.Property(u => u.VerifiedAt)
                   .HasColumnName("verified_at")
                   .HasDefaultValue(null);

            builder.HasMany(u => u.Roles)
                   .WithMany(r => r.Users)
                   .UsingEntity<Dictionary<string, object>>(
                        "user_roles",
                        l => l.HasOne<Role>()
                              .WithMany()
                              .HasForeignKey("role_id")
                              .OnDelete(DeleteBehavior.Restrict),
                        r => r.HasOne<User>()
                              .WithMany()
                              .HasForeignKey("user_id")
                              .OnDelete(DeleteBehavior.Restrict),
                        j => j.ToTable("user_roles")
                              .HasKey("role_id", "user_id"));

            builder.Navigation(u => u.Roles)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(u => u.Establishments)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(u => u.PaymentMethods)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
