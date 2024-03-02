
using Domain.Additional;
using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    [Table("Products")]
    public record  Product : AuditableEntity
    {
        [Key]
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int WeightInGrams { get; init; }
        public decimal Calories { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbonhydrates { get; init; }
        public decimal Fat { get; init;}
        public string  Ingredients { get; set; }
        public string Description { get; init; }
        public string ProductPhotoPath { get; set; }

        public Product(){}
        public Product(string name,int weightInGrams, decimal calories, decimal protein, decimal carbonhydrates,decimal fat, string ingredientsList , string description)
        {
            Id= Guid.NewGuid();
            (Name, WeightInGrams, Calories, Protein, Carbonhydrates, Fat, Ingredients, Description) = (name, weightInGrams, calories, protein, carbonhydrates, fat, ingredientsList, description);

        }
    }
}
