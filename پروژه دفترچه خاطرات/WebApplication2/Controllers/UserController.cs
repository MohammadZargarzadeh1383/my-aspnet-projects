using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebApplication2.Application.Interfaces.Repositories;
using WebApplication2.Application.Interfaces.Services;
using WebApplication2.Application.Models.Dto.User;
using WebApplication2.Domain.Entities.User;

namespace WebApplication2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtOperation _jwtOperation;
        public UserController(IUserRepository userRepository, IJwtOperation jwtOperation)
        {
            _userRepository = userRepository;
            _jwtOperation = jwtOperation;
        }
        [AllowAnonymous]
        [HttpPost("createuser")]
        public async Task<ActionResult> CreateUser(CreateUserDto userdto)
        {
                var user = new User()
                {
                    Password = userdto.Password,
                    UserName = userdto.UserName,
                    CreatedBy = "system",
                    CreatedAt = DateTime.Now,
                };
                var createuser = await _userRepository.Create(user);
                var token = await _jwtOperation.GenerateTokenAsync(userdto.UserName, createuser.Id);
                return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] CreateUserDto userdto)
        {
            var Loginuser = await _userRepository.GetAll().Where(x => x.UserName == userdto.UserName && x.Password == userdto.Password).FirstOrDefaultAsync();
            if (Loginuser != null)
            { 
                var token = await _jwtOperation.GenerateTokenAsync(Loginuser.UserName, Loginuser.Id);
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }
        }
        [AllowAnonymous]

        [HttpGet(Name = "GetAllUser")]
        public async Task<ActionResult<List<ShowUserDto>>> GetAllUser()
        {

            var createuser = await _userRepository.GetAll().Select(x => new ShowUserDto
            {
                Id = x.Id,
                Username = x.UserName,
                Firstname = x.FirstName,
                Lastname = x.LastName
            }
            ).ToListAsync();
            return Ok(createuser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetById(id);
            var result = new ShowUserDto
            {
                Id = id,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Username = user.UserName,
                CreatedBy = "system"
            };
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser([FromQuery] int id, [FromBody] CreateUserDto userdto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.UserName = userdto.UserName;
                user.UpdatedAt = DateTime.Now;
                var updateuser = await _userRepository.Update(user);
                return Ok(updateuser);
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var deleteuser = await _userRepository.Delete(user);
                return Ok();
            };
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<int>> GetId(int id)
        //{
        //    var user = await _userRepository.GetId(id);
        //    return Ok(user);
        //}


    }
}


