using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nadin_Soft_Api_Project.Application.Interfaces.Repositories;
using Nadin_Soft_Api_Project.Application.Interfaces.Services;
using Nadin_Soft_Api_Project.Application.Models.Dto.CommentDto;
using Nadin_Soft_Api_Project.Application.Models.Dto.UserDto;
using Nadin_Soft_Api_Project.Domain.Entities.Product;
using Nadin_Soft_Api_Project.Domain.Entities.User;

namespace Nadin_Soft_Api_Project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(IGenericRepository<Product> genericRepository, IProductRepository productRepository)
        {
            _genericRepository = genericRepository;
            _productRepository = productRepository;
        }



        [HttpPost("Create")]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto ProductDto)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var product = new Product()
            {
                ManufactureEmail = ProductDto.ManufactureEmail,
                ManufacturePhone = ProductDto.ManufacturePhone,
                Name = ProductDto.Name,
                UserRef = int.Parse(userid)
            };
            var createproduct = await _genericRepository.Create(product);
            return Ok(createproduct);
        }
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ShowProductDto>>> GetAllProduct()
        {
            var getallproduct = await _genericRepository.GetAll().Select(x => new ShowProductDto
            {
                Id = x.Id,
                ManufactureEmail = x.ManufactureEmail,
                ManufacturePhone = x.ManufacturePhone,
                UserRef = x.UserRef,
                Name = x.Name,
                ProduceDate = x.ProduceDate,
            }
            ).ToListAsync();
            return Ok(getallproduct);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Product>> GetProduct([FromQuery] int id)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var product = await _productRepository.GetByIdProduct(id, int.Parse(userid));
            var result = new ShowProductDto
            {
                Id = id,
                ManufactureEmail = product.ManufactureEmail,
                ManufacturePhone = product.ManufacturePhone,
                Name = product.Name,
                ProduceDate = product.ProduceDate,
                UserRef = product.UserRef
            };
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("GetByUserRef")]
        public async Task<ActionResult<Product>> GetUserProduct([FromQuery] int id)
        {
            var product = await _productRepository.GetAllByUserRef(id).Select(x => new ShowProductDto
            {
                Id = x.Id,
                ManufactureEmail = x.ManufactureEmail,
                ManufacturePhone = x.ManufacturePhone,
                UserRef = x.UserRef,
                Name = x.Name,
                ProduceDate = x.ProduceDate,
            }
            ).ToListAsync();
            return Ok(product);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Product>> UpdateProduct([FromQuery] int id, [FromBody] CreateProductDto Productdto)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var product = await _productRepository.GetByIdProduct(id, int.Parse(userid));
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                product.ManufacturePhone = Productdto.ManufacturePhone;
                product.ManufactureEmail= Productdto.ManufactureEmail;
                product.Name = Productdto.Name;
                product.ProduceDate = DateTime.Now;
                var updateproduct = await _genericRepository.Update(product);
                return Ok(updateproduct);
            }
            ;
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteProduct([FromQuery] int id)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var product = await _productRepository.GetByIdProduct(id, int.Parse(userid));
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var deleteproduct = await _genericRepository.Delete(product);
                return Ok();
            }
            ;
        }
    }
}

