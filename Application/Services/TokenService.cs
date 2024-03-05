
using Application.Dtos.UserDtos;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    internal class TokenService : ITokenService
    {
        public string GenerateJWT(UserDto userCredentials, IConfiguration config)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userCredentials.Username),
                    new Claim(ClaimTypes.Email, userCredentials.EmailAddress),
                    new Claim(ClaimTypes.Role, userCredentials.Role),
                    new Claim("Date", DateTime.Now.ToString())

                };
                var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials );
                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception)
            {
                throw new NotImplementedException();

            }
        }
    }
}
