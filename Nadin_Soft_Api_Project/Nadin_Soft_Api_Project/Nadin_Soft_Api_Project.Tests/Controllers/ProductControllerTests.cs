using Microsoft.AspNetCore.Mvc;
using Moq;
using Nadin_Soft_Api_Project.Application.Interfaces.Repositories;
using Nadin_Soft_Api_Project.Application.Models.Dto.ProductDto;
using Nadin_Soft_Api_Project.Controllers;
using Nadin_Soft_Api_Project.Domain.Entities.Product;
using Xunit;

namespace Nadin_Soft_Api_Project.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _controller = new ProductController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                Stock = 10
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ShowProductDto>(okResult.Value);
            Assert.Equal(product.Id, returnValue.Id);
            Assert.Equal(product.Name, returnValue.Name);
            Assert.Equal(product.Description, returnValue.Description);
            Assert.Equal(product.Price, returnValue.Price);
            Assert.Equal(product.Stock, returnValue.Stock);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.GetByIdAsync(nonExistentId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetById(nonExistentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedProduct()
        {
            // Arrange
            var createDto = new CreateProductDto
            {
                Name = "New Product",
                Description = "New Description",
                Price = 200,
                Stock = 20
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                Stock = createDto.Stock
            };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<ShowProductDto>(createdAtActionResult.Value);
            Assert.Equal(product.Id, returnValue.Id);
            Assert.Equal(createDto.Name, returnValue.Name);
            Assert.Equal(createDto.Description, returnValue.Description);
            Assert.Equal(createDto.Price, returnValue.Price);
            Assert.Equal(createDto.Stock, returnValue.Stock);
        }
    }
} 