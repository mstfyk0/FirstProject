
namespace Application.Dtos.ProductDtos
{
    // Dto for product requested by users
    public record ProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int WeightInGrams { get; init; }
        public decimal Calories { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbohydrates { get; init; }
        public decimal Fat { get; init; }
        public string Ingredients { get; init; }
        public string Description { get; init; }
        public string ProductPhotoPath { get; init; }
    }
}
