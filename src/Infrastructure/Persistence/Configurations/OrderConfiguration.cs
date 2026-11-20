using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                   .HasColumnName("id");

            builder.Property(o => o.UserId)
                   .IsRequired()
                   .HasColumnName("user_id");

            builder.Property(o => o.Status)
                   .IsRequired()
                   .HasColumnName("status");

            builder.Property(o => o.Address)
                   .IsRequired()
                   .HasColumnName("address");

            builder.Property(o => o.Cost)
                   .IsRequired()
                   .HasColumnName("cost");

            builder.Property(o => o.Details)
                   .HasColumnName("details");

            builder.Property(o => o.Comment)
                   .HasColumnName("comment");

            builder.Property(o => o.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(o => o.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasOne(o => o.User)
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
