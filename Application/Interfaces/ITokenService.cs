
using Application.Dtos.UserDtos;
using Microsoft.Extensions.Configuration;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJWT(UserDto userCredentials, IConfiguration config);
    }
}
