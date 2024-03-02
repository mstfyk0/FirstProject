
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.WeightInGrams).NotEmpty();
            RuleFor(x => x.Calories).NotEmpty();
            RuleFor(x => x.Protein).NotEmpty();
            RuleFor(x => x.Carbonhydrates).NotEmpty();
            RuleFor(x => x.Fat).NotEmpty();
            RuleFor(x => x.Ingredients).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);

        }
    }
}
