using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using TAMRIN.Domain.Entites.Common;
using TAMRIN.Domain.Entites.User;
using TAMRIN.Domain.Entites.Food;
using TAMRIN.Application.Interfaces.ApplicationDb;

namespace TAMRIN.Infrastucture.ApplicationDb.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext , IApplicationDbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public DbSet<User> users => Set<User>(); 
        public DbSet<Food> Foods => Set<Food>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TEntity> SetDbset<TEntity>() where TEntity : BaseEntity => Set<TEntity>();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }
    }
}
