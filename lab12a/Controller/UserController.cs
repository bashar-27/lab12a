using lab12a.Models.DTO;
using lab12a.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab12a.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userService;
        public UserController(IUser services)
        {
            _userService = services;
            
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>>Register(RegisterUserDto Data)
        {
            var user = await _userService.Register(Data, this.ModelState);
            if(ModelState.IsValid)
            {
                return user;
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }
        [HttpPost("Authenticate")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var user = await _userService.Authenticate(loginDto.UserName, loginDto.Password);
            if(user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }
        [Authorize(Roles = "District Manager") ]
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDto>>Profile()
        {
            return await _userService.GetUser(this.User);
        }
    }
}
