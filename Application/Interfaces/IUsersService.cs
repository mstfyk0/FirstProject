
using Application.Dtos.UserDtos;

namespace Application.Interfaces
{
    //interface for service working on users repository
    public interface IUsersService
    {
        Task<UserDto> GetUserAsync(LoginDto userCredentials);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<UserDto> AddUserAsync(RegisterDto userDto);
        Task<UserDto> ChangePasswordAsync(string username, string password);
        Task<UserDto> ChangeEmailAsync(string username, string email);
        Task DeleteUserAsync(string username);
    }
}
