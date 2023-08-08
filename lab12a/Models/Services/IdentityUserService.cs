using lab12a.Models.DTO;
using lab12a.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace lab12a.Models.Services
{
    public class IdentityUserService :IUser
    {
        private UserManager<AppUser> _userManager;
        public IdentityUserService(UserManager<AppUser>manager)
        {
            _userManager = manager;
        }
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
                return new UserDto()
                {
                    Id = user.Id,
                    userName = user.UserName
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
        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            bool isValid = await _userManager.CheckPasswordAsync(user,password);
            if (isValid)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    userName = user.UserName
                };
            
            }
            return null;
        }
    }
}
