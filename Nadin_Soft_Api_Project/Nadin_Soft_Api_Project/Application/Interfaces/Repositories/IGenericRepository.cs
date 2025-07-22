using Nadin_Soft_Api_Project.Domain.Entities.Common;

namespace Nadin_Soft_Api_Project.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<TEntity> Create(TEntity entity);
        public Task<bool> Delete(TEntity entity);
        public Task<int> Update(TEntity entity);
        public Task<TEntity> GetById(int id);
        public IQueryable<TEntity> GetAll();

    }
}
