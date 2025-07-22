using Nadin_Soft_Api_Project.Domain.Entities.Product;

namespace Nadin_Soft_Api_Project.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public IQueryable<Product> GetAllByUserRef(int id);
        public Task<Product> GetByIdProduct(int id, int userRef);

    }
}
