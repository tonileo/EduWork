using Common.DTO.Authentication;
using EduWork.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var result = await user.RegistrationUserAsync(registerUserDto);
            return Ok(result);
        }
    }
}
