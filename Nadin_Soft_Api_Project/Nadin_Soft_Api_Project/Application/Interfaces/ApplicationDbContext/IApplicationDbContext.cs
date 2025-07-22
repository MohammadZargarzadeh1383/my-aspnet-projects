using Microsoft.EntityFrameworkCore;
using Nadin_Soft_Api_Project.Domain.Entities.Common;
using Nadin_Soft_Api_Project.Domain.Entities.Product;
using Nadin_Soft_Api_Project.Domain.Entities.User;
using System.Reflection;

namespace Nadin_Soft_Api_Project.Application.Interfaces.ApplicationDbContext
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Product> Products { get; }
        public DbSet<TEntity> SetDbset<TEntity>() where TEntity : BaseEntity;
    }
}
