
namespace Application.Dtos.UserDtos
{
    //dto used for logging of user
    public record LoginDto
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
