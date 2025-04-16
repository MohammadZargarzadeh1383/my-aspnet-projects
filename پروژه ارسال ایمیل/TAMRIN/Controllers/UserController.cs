using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAMRIN.Application.Interfaces.Repositories;
using TAMRIN.Application.Models.User;
using TAMRIN.Domain.Entites.User;
using TAMRIN.Infrastucture.Repositoreis;

namespace TAMRIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> EmailLogin([FromQuery]string email) 
        {
            var user = new User { Email = email };
            _userRepository.Emaillogin(user);
            return Ok();
        }
    }
}
