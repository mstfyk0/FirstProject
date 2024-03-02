
using Domain.Additional;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMealsRepository
    {
        IQueryable<Meal>  GetAllMeals();
        Task<IEnumerable<Meal>> GetMealsPagedAsync(int pageNumber,int pageSize,NutritionRange range ,bool? ascendingSorting);
        Task<Meal> GetMealByIdAsync(Guid id);
        Task AddMealAsync(Meal meal);
        Task UpdateMealAsync(Meal meal);
        Task DeleteMealAsync(Meal meal);
        Task<int> CountMealAsync();

    }
}
