using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration for the <see cref="User"/> entity.
    /// </summary>
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("id");

            builder.Property(u => u.EmailAddress)
                   .IsRequired()
                   .HasColumnName("email_address");

            builder.HasIndex(u => u.EmailAddress).IsUnique();

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasColumnName("password_hash");

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasColumnName("name");

            builder.Property(u => u.ImageUrl)
                   .HasDefaultValue(null)
                   .HasColumnName("image_url");

            builder.Property(u => u.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(u => u.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

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
        }
    }
}
