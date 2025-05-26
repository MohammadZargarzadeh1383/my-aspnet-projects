using IntershipTaskManager.Domain.Entities.Common;

namespace IntershipTaskManager.Application.Interfaces.Repositoreis
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public  Task<int> Creat(TEntity entity);
        public  Task<bool> Delete(TEntity entity);
        public  Task<int> Update(TEntity entity);
        public  Task<TEntity> GetById(int id);
        public IQueryable<TEntity> GetAllUser();

    }
}
