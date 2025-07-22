using Microsoft.EntityFrameworkCore;
using Nadin_Soft_Api_Project.Domain.Entities.Product;
using Nadin_Soft_Api_Project.Infrastructure.ApplicationDb;
using Nadin_Soft_Api_Project.Infrastructure.Repositories;
using Xunit;

namespace Nadin_Soft_Api_Project.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new ProductRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                Stock = 10
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Stock, result.Stock);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _repository.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewProduct()
        {
            // Arrange
            var product = new Product
            {
                Name = "New Product",
                Description = "New Description",
                Price = 200,
                Stock = 20
            };

            // Act
            await _repository.AddAsync(product);
            await _context.SaveChangesAsync();

            // Assert
            var savedProduct = await _context.Products.FindAsync(product.Id);
            Assert.NotNull(savedProduct);
            Assert.Equal(product.Name, savedProduct.Name);
            Assert.Equal(product.Description, savedProduct.Description);
            Assert.Equal(product.Price, savedProduct.Price);
            Assert.Equal(product.Stock, savedProduct.Stock);
        }
    }
} 