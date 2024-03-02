
using Application.Dtos.MealDtos;
using Domain.Additional;

namespace Application.Interfaces
{
    public interface IMealsService
    {
        IQueryable<MealDto> GetAllMeals();
        Task<IEnumerable<MealDto>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort);
        Task<MealDto> GetMealByIdAsync(Guid id);
        Task<MealDto> AddMealAsync(CreateMealDto newMeal,string mealPhotoPath);
        Task<MealDto> UpdateMealAsync(UpdateMealDto updateMeal, Guid id , string mealPhotoPath);
        Task DeleteMealAsync(Guid id);
        Task<int> CountMealsAsync();
        Task<string> GetPathOfMealImage(Guid id);

    }
}
