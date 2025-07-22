using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nadin_Soft_Api_Project.Domain.Entities.User;

namespace Nadin_Soft_Api_Project.Infrastructure.Configuration.Users
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.Email)
                   .HasMaxLength(150);

            

            builder.HasMany(u => u.Products)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserRef);
        }
    }
}

