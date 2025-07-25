﻿using Microsoft.EntityFrameworkCore;
using WebApplication2.Domain.common;
using WebApplication2.Domain.Entities.Notion;
using WebApplication2.Domain.Entities.User;

namespace WebApplication2.Application.Interfaces.ApplicationDd
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Notion> Notions { get; }

        public DbSet<TEntity> SetDbset<TEntity>() where TEntity : BaseEntity;
    }
}
