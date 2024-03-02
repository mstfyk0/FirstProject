
using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Users")]
    public record User : AuditableEntity
    {
        [Key]
        public string UserName { get; init; }
        public string Password { get; init; }
        public string EmailAddress { get; init; }
        public string Role { get; init; }

        public User(){}
        public User(string userName, string password, string emailAddress, string role)
        {
            UserName = userName;
            Password = password;
            EmailAddress = emailAddress;
            Role = role;
        }

    }
}
