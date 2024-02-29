

namespace Application.Dtos.UserDtos
{
    //dto used for registration of users
    public record RegisterDto
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public string EmailAddress { get; init; }
        public string Role { get; init; }
    }
}
