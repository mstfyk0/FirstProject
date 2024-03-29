﻿
using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.MealDtos
{
    public record CreateMealDto
    {
        public string Name { get; init; }
        public int WeightInGrams { get; init; }
        public decimal Calories  { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbonhydrates { get; init; }
        public decimal Fat { get; init; }
        public List<Ingredient>? IngredientsList { get; init; }
        public string Recipe { get; init; }
        public IFormFile? Image { get; init; }

    }
}
