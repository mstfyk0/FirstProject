
namespace Application.Dtos.UserDtos
{
    //dto used for showing info of user
    public record UserDto
    {
        public string Username { get; init; }
        public string EmailAddress { get; init; }
        public string Role { get; init; }
    }
}
