
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetLoggingUsersAsync(string userName, string password);
        Task<User> GetUserByUsernameAsync(string userName);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);

    }
}
