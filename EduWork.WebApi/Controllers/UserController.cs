using System.Threading.Tasks;
using Common.DTO.Authentication;
using Common.DTO.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduWork.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        public UserController(IUser user)
        {
            this.user = user;
        }
       
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LogUserIn(LoginDto loginDto)
        {
            var result = await user.LoginUserAsync(loginDto);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var result = await user.RegistrationUserAsync(registerUserDto);
            return Ok(result);
        }
    }
}
