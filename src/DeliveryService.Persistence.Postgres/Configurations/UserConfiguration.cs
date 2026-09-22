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

            builder.Property(u => u.FirstName)
                   .HasConversion(
                        fn => (string)fn,
                        fn => StringBounded.Create(fn))
                   .HasColumnName("first_name")
                   .IsRequired();

            builder.HasMany(u => u.Roles)
                   .WithMany(r => r.Users)
                   .UsingEntity<Dictionary<string, object>>(
                        "user_roles",
                        l => l.HasOne<Role>()
                              .WithMany()
                              .HasForeignKey("role_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        r => r.HasOne<User>()
                              .WithMany()
                              .HasForeignKey("user_id")
                              .OnDelete(DeleteBehavior.Cascade),
                        j => j.ToTable("user_roles")
                              .HasKey("role_id", "user_id"));

            builder.Navigation(u => u.Roles)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
