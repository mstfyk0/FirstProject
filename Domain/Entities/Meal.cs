
using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Additional;

namespace Domain.Entities
{
    //table containing meals

    [Table("Meals")]
    public record Meal : AuditableEntity
    {
        [Key]
        public Guid Id { get; init;}
        public string Name { get; init;}
        public int WeightInGrams { get; init; }
        public decimal Protein { get; init; }
        public decimal Calories { get; init; }
        public decimal Carbonhydrates { get; init; }
        public decimal Fat { get; init; }
        public string Ingredients { get; set; }
        public string Recipe { get; init; }
        public string MealPhotoPath  { get; set; }

        public Meal(string name,int weightInGrams,decimal protein,decimal calories,decimal carbonhydrates,decimal fat, List<Ingredient>? ingredientsList, string recipe)
        {
            Id=Guid.NewGuid();
            Ingredients = IngredientsConverterListToString.listToString(ingredientsList);
            (Name,WeightInGrams,Protein,Calories,Carbonhydrates,Fat,Recipe) =
            (
                Name = name,
                WeightInGrams = weightInGrams,
                Protein = protein,
                Calories = calories,
                Carbonhydrates = carbonhydrates,
                Fat = fat,
                Recipe = recipe
                );
        }


    }
}
