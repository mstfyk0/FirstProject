
using Domain.Entities;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace Domain.Validators
{
    public class MealValidator : AbstractValidator<Meal>
    {
        public MealValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.WeightInGrams).NotEmpty();
            RuleFor(x => x.Calories).NotEmpty();
            RuleFor(x => x.Protein).NotEmpty();
            RuleFor(x => x.Carbonhydrates).NotEmpty();
            RuleFor(x => x.Fat).NotEmpty();
            RuleFor(x => x.Ingredients).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.Recipe).NotEmpty().MaximumLength(1000);
        }
    }
}
