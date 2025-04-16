using Microsoft.EntityFrameworkCore;
using System;
using TAMRIN.Domain.Entites.Common;
using TAMRIN.Domain.Entites.Food;
using TAMRIN.Domain.Entites.User;

namespace TAMRIN.Application.Interfaces.ApplicationDb
{
    public interface IApplicationDbContext
    {
        public DbSet<User> users { get; }
        public DbSet<Food> Foods { get; }

        public DbSet<TEntity> SetDbset<TEntity>() where TEntity : BaseEntity;
    }
}
