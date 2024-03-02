

using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{   
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(20);
            RuleFor(x => x.EmailAddress).EmailAddress();
            RuleFor(x => x.Role).NotEmpty().MaximumLength(20);
        }
    }
}
