using Microsoft.EntityFrameworkCore;
using Nadin_Soft_Api_Project.Application.Interfaces.Repositories;
using Nadin_Soft_Api_Project.Domain.Entities.Common;
using Nadin_Soft_Api_Project.Domain.Entities.User;
using Nadin_Soft_Api_Project.Domain.Enums.AppAction;
using Nadin_Soft_Api_Project.Infrastructure.Repositories;
using Nadin_Soft_Api_Project.Infrastucture.ApplicationDb;

namespace Nadin_Soft_Api_Project.Infrastucture.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entities;
        private readonly IProductRepository _productRepository;
        public GenericRepository(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _entities = context.Set<TEntity>();
            _productRepository = productRepository;
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            var save =  await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return save.Entity;
        }
        public async Task<bool> Delete(TEntity entity)
        {
            entity.AppAction = AppAction.Deleted;
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
            var get = await _entities.FirstOrDefaultAsync(m => m.Id == id && m.AppAction == AppAction.Active);
            await _context.SaveChangesAsync();
            return get;
        }
        public IQueryable<TEntity> GetAll()
        {
            var getall = _entities.Where(m => m.AppAction == AppAction.Active);
            return getall;
        }

    }
}
