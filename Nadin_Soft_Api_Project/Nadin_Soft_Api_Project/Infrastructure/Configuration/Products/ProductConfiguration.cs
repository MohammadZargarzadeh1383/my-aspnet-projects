using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nadin_Soft_Api_Project.Domain.Entities.Product;

namespace Nadin_Soft_Api_Project.Infrastructure.Configuration.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(p => p.ManufactureEmail)
                   .HasMaxLength(100);

            builder.Property(p => p.ManufacturePhone)
                   .HasMaxLength(20);

            builder.HasIndex(nameof(Product.ManufactureEmail), nameof(Product.ManufacturePhone))
                   .IsUnique();

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Products)
                   .HasForeignKey(p => p.UserRef)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}