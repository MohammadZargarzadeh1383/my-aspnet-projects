using IntershipTaskManager.Application.Interfaces.Repositoreis;
using IntershipTaskManager.Domain.Entities.Common;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Interfaces.ApplicationDB.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace IntershipTaskManager.Infrastucture.Repositoreis.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private ApplicationDbContext _context;
        private DbSet<TEntity> _entities;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public async Task<int> Creat(TEntity entity)
        {
            await _entities.AddAsync(entity);
            var save = await _context.SaveChangesAsync();
            return save;
        }
        public async Task<bool> Delete(TEntity entity)
        {
            entity.AppAction = Domain.Enums.AppActionType.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> Update(TEntity entity)
        {
            _entities.Update(entity);
            var save = await _context.SaveChangesAsync();
            return save;
        }
        public async Task<TEntity> GetById(int id)
        {
            var get = await _entities.FirstOrDefaultAsync(m => m.Id == id && m.AppAction == Domain.Enums.AppActionType.Active);
            await _context.SaveChangesAsync();
            return get;
        }
        public  IQueryable<TEntity> GetAllUser()
        {
            var getall =  _entities.Where(m => m.AppAction == Domain.Enums.AppActionType.Active);
            return getall;
        }
    }
}
