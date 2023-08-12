using lab12a.Models.DTO;
using lab12a.Models.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace lab12a.Models.Services
{
    public class IdentityUserService :IUser
    {
        private UserManager<AppUser> _userManager;
        private JwtTokenService _jwtTokenService;
        public IdentityUserService(UserManager<AppUser>manager, JwtTokenService jwtTokenService)
        {
            _userManager = manager;
            _jwtTokenService = jwtTokenService;

        }
        //to create new user
        public async Task<UserDto> Register(RegisterUserDto registerUserDto , ModelStateDictionary modelState)
        {
            var user = new AppUser()
            {
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
                PhoneNumber = registerUserDto.Phone
            };
            var result = await _userManager.CreateAsync(user,registerUserDto.Password);
            if (result.Succeeded)
            {
              await  _userManager.AddToRolesAsync(user, registerUserDto.Roles);
                return new UserDto()
                {
                    Id = user.Id,
                    userName = user.UserName,
                    Token= await _jwtTokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                    Roles =await _userManager.GetRolesAsync(user),
                };
            }
            foreach (var error in result.Errors)
            {
                var errorMessage = error.Code.Contains("Password") ? nameof(registerUserDto.Password) :
                                   error.Code.Contains("Email") ? nameof(registerUserDto.Email) :
                                   error.Code.Contains("Username") ? nameof(registerUserDto.UserName) :
                                   error.Code.Contains("Phone") ? nameof(registerUserDto.Phone) :
                                   "";
                modelState.AddModelError(errorMessage, error.Description);

            }
            return null;
        }
        //to ensure if user really exist or not
        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            bool isValid = await _userManager.CheckPasswordAsync(user,password);
            if (isValid)
            {
                return new UserDto()
                {
                    userName = user.UserName,
                    Id = user.Id,
                    Token = await _jwtTokenService.GetToken(user,System.TimeSpan.FromMinutes(5)),
                };
            
            }

            return null;
        }

        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
           var user= await _userManager.GetUserAsync(principal);
            return new UserDto
            {
                Id = user.Id,
                userName = user.UserName,
                Token = await _jwtTokenService.GetToken(user,System.TimeSpan.FromMinutes(5))
            };
        }
    }
}
