using lab12a.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace lab12a.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDto> Register(RegisterUserDto registerUser, ModelStateDictionary modelState);
        public Task<UserDto> Authenticate(string username, string password);
    }
}
