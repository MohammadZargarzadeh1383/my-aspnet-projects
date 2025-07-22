using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nadin_Soft_Api_Project.Application.Interfaces.ApplicationDbContext;
using Nadin_Soft_Api_Project.Domain.Entities;
using Nadin_Soft_Api_Project.Domain.Entities.Common;
using Nadin_Soft_Api_Project.Domain.Entities.Product;
using Nadin_Soft_Api_Project.Domain.Entities.User;
using System;
using System.Reflection;


namespace Nadin_Soft_Api_Project.Infrastucture.ApplicationDb;

public class ApplicationDbContext : DbContext , IApplicationDbContext
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) 
        : base(options) 
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>(); 

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
