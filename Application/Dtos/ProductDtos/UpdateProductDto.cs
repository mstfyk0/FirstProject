﻿using Microsoft.AspNetCore.Http;

namespace Application.Dtos.ProductDtos
{
    // dto for updated product
    public record UpdateProductDto
    {
        public string Name { get; init; }
        public int WeightInGrams { get; init; }
        public decimal Calories { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbohydrates { get; init; }
        public decimal Fat { get; init; }
        public string Ingredients { get; init; }
        public string Description { get; init; }
        public IFormFile? Image { get; init; }
    }
}
