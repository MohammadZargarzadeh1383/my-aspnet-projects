using IntershipTaskManager.Domain.Entities.Common;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Domain.Entities.Task;
using Microsoft.EntityFrameworkCore;

namespace IntershipTaskManager.Application.Interfaces.ApplicationDbContext;

public interface IApplicationDbContext 
{
    public DbSet<Mission> Missions { get; }
    public DbSet<Intership> Interships { get; }
    public DbSet<IntershipTask> IntershipTasks { get; }

    public DbSet<TEntity> SetDbset<TEntity>() where TEntity : BaseEntity;
}
