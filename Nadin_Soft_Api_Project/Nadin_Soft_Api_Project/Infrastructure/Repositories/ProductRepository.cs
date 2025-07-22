using Nadin_Soft_Api_Project.Application.Interfaces.Repositories;
using Nadin_Soft_Api_Project.Domain.Entities.Product;
using Nadin_Soft_Api_Project.Domain.Enums.AppAction;
using Microsoft.EntityFrameworkCore;
using Nadin_Soft_Api_Project.Infrastucture.ApplicationDb;

namespace Nadin_Soft_Api_Project.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IQueryable<Product> GetAllByUserRef(int id)
        {
            var getall = _context.Products.Where(m => m.UserRef == id && m.AppAction == AppAction.Active);
            return getall;
        }
        public async Task<Product> GetByIdProduct(int id,int userRef)
        {
            var get = await GetAllByUserRef(userRef).FirstOrDefaultAsync(m => m.Id == id && m.AppAction == AppAction.Active);
            await _context.SaveChangesAsync();
            return get;
        }

    }
}
