using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nadin_Soft_Api_Project.Application.Interfaces.Repositories;
using Nadin_Soft_Api_Project.Application.Interfaces.Services;
using Nadin_Soft_Api_Project.Application.Models.Dto.UserDto;
using Nadin_Soft_Api_Project.Domain.Entities.User;

namespace Nadin_Soft_Api_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IJwtOperation _jwtOperation;
        public UserController(IGenericRepository<User> genericRepository, IJwtOperation jwtOperation)
        {
            _genericRepository = genericRepository;
            _jwtOperation = jwtOperation;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginuserdto)
        {
            var Loginuser = await _genericRepository.GetAll().Where(x => x.FirstName == loginuserdto.Name && x.Password == loginuserdto.Password).FirstOrDefaultAsync();
            if (Loginuser != null)
            {
                var token = await _jwtOperation.GenerateTokenAsync(Loginuser.FirstName, Loginuser.Id);
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<ActionResult<User>> CreateUser(CreateUserDto UserDto)
        {
            var user= new User()
            {
                FirstName = UserDto.FirstName,
                LastName = UserDto.LastName,
                PhoneNumber = UserDto.PhoneNumber,
                Password = UserDto.Password,
            };
            var createuser = await _genericRepository.Create(user);
            var token = await _jwtOperation.GenerateTokenAsync(createuser.FirstName, createuser.Id);
            return Ok(new { createuser, token });

        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ShowUserDto>>> GetAllUser()
        {
            var getalluser = await _genericRepository.GetAll().Select(x => new ShowUserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                CreatedAt = x.CreatedAt
            }
            ).ToListAsync();
            return Ok(getalluser);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<User>> GetUser([FromQuery]int id)
        {
            var user = await _genericRepository.GetById(id);
            var result = new ShowUserDto 
            { 
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber, 
                CreatedAt = user.CreatedAt,
            };
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<User>> UpdateUser([FromQuery] int id, [FromBody] CreateUserDto userdto)
        {
            
            var user = await _genericRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.FirstName = userdto.FirstName;
                user.LastName = userdto.LastName;
                user.PhoneNumber = userdto.PhoneNumber;
                user.UpdatedAt = DateTime.Now;
                var updateuser = await _genericRepository.Update(user);
                return Ok(updateuser);
            }
            ;
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteUser([FromQuery]int id)
        {
            var user = await _genericRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var deleteuser = await _genericRepository.Delete(user);
                return Ok();
            }
            ;
        }
    }
}

