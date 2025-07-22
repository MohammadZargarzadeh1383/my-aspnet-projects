using Microsoft.EntityFrameworkCore;
using Moq;
using Nadin_Soft_Api_Project.Application.Interfaces.Repositories;
using Nadin_Soft_Api_Project.Domain.Entities.Product;
using Nadin_Soft_Api_Project.Domain.Enums.AppAction;
using Nadin_Soft_Api_Project.Infrastructure.ApplicationDb;
using Nadin_Soft_Api_Project.Infrastructure.Repositories;
using Xunit;

namespace Nadin_Soft_Api_Project.Tests.Repositories
{
    public class GenericRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly GenericRepository<Product> _repository;
        private readonly Mock<IProductRepository> _mockProductRepository;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGenericDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _mockProductRepository = new Mock<IProductRepository>();
            _repository = new GenericRepository<Product>(_context, _mockProductRepository.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOnlyActiveEntities()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Name = "محصول 1", Price = 1000, Stock = 10, AppAction = AppAction.Active },
                new Product { Name = "محصول 2", Price = 2000, Stock = 20, AppAction = AppAction.Active },
                new Product { Name = "محصول حذف شده", Price = 3000, Stock = 30, AppAction = AppAction.Deleted }
            };
            _context.Products.AddRange(products);
            _context.SaveChanges();

            // Act
            var result = _repository.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, item => Assert.Equal(AppAction.Active, item.AppAction));
        }

        [Fact]
        public async Task GetById_ShouldReturnActiveEntity()
        {
            // Arrange
            var product = new Product 
            { 
                Name = "محصول تست",
                Description = "توضیحات تست",
                Price = 1500,
                Stock = 15,
                AppAction = AppAction.Active
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetById(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal("محصول تست", result.Name);
            Assert.Equal(AppAction.Active, result.AppAction);
        }

        [Fact]
        public async Task Create_ShouldAddAndReturnEntity()
        {
            // Arrange
            var product = new Product 
            { 
                Name = "محصول جدید",
                Description = "توضیحات محصول جدید",
                Price = 3000,
                Stock = 30,
                AppAction = AppAction.Active
            };

            // Act
            var result = await _repository.Create(product);
            
            // Assert
            Assert.NotNull(result);
            var savedProduct = await _context.Products.FindAsync(result.Id);
            Assert.NotNull(savedProduct);
            Assert.Equal("محصول جدید", savedProduct.Name);
            Assert.Equal(AppAction.Active, savedProduct.AppAction);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntityAndReturnNumberOfChanges()
        {
            // Arrange
            var product = new Product 
            { 
                Name = "محصول اولیه",
                Description = "توضیحات اولیه",
                Price = 1000,
                Stock = 10,
                AppAction = AppAction.Active
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            product.Name = "محصول به‌روز شده";
            product.Price = 1500;
            var result = await _repository.Update(product);

            // Assert
            Assert.Equal(1, result);
            var updatedProduct = await _context.Products.FindAsync(product.Id);
            Assert.NotNull(updatedProduct);
            Assert.Equal("محصول به‌روز شده", updatedProduct.Name);
            Assert.Equal(1500, updatedProduct.Price);
            Assert.Equal(AppAction.Active, updatedProduct.AppAction);
        }

        [Fact]
        public async Task Delete_ShouldSetAppActionToDeleted()
        {
            // Arrange
            var product = new Product 
            { 
                Name = "محصول حذفی",
                Description = "این محصول باید حذف شود",
                Price = 500,
                Stock = 5,
                AppAction = AppAction.Active
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Delete(product);

            // Assert
            Assert.True(result);
            var deletedProduct = await _context.Products.FindAsync(product.Id);
            Assert.NotNull(deletedProduct);
            Assert.Equal(AppAction.Deleted, deletedProduct.AppAction);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenEntityIsDeleted()
        {
            // Arrange
            var product = new Product 
            { 
                Name = "محصول حذف شده",
                Price = 1000,
                Stock = 10,
                AppAction = AppAction.Deleted
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetById(product.Id);

            // Assert
            Assert.Null(result);
        }
    }
} 