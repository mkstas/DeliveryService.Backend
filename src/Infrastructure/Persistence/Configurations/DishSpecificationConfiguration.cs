using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class DishSpecificationConfiguration : IEntityTypeConfiguration<DishSpecification>
    {
        public void Configure(EntityTypeBuilder<DishSpecification> builder)
        {
            builder.ToTable("dish_specifications");

            builder.HasKey(ds => ds.Id);

            builder.Property(ds => ds.Id)
                   .HasColumnName("id");

            builder.Property(ds => ds.DishId)
                   .IsRequired()
                   .HasColumnName("dish_id");

            builder.Property(ds => ds.SpecificationId)
                   .IsRequired()
                   .HasColumnName("specification_id");

            builder.Property(ds => ds.Value)
                   .IsRequired()
                   .HasColumnName("value");

            builder.Property(ds => ds.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.Property(ds => ds.UpdatedAt)
                   .IsRequired()
                   .HasColumnName("updated_at");

            builder.HasIndex(ds => new { ds.DishId, ds.SpecificationId }).IsUnique();

            builder.HasOne(ds => ds.Dish)
                   .WithMany(d => d.Specifications)
                   .HasForeignKey(ds => ds.DishId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ds => ds.Specification)
                   .WithMany()
                   .HasForeignKey(ds => ds.SpecificationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
