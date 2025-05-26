using IntershipTaskManager.Application.Interfaces.ApplicationDbContext;
using IntershipTaskManager.Domain.Entities.Common;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Domain.Entities.Task;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IntershipTaskManager.Interfaces.ApplicationDB.ApplicationDbContext;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IHttpContextAccessor httpContextAccessor;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
    public DbSet<Mission> Missions => Set<Mission>();
    public DbSet<Intership> Interships => Set<Intership>();
    public DbSet<IntershipTask> IntershipTasks => Set<IntershipTask>();
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
