using Domain.Additional;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    // class working on sql database, more specifically on products table
    public class SQLMealsRepository : IMealsRepository
    {
        private readonly FitnessPlannerContext _context;

        public SQLMealsRepository(FitnessPlannerContext context)
        {
            _context = context;
        }

        public IQueryable<Meal> GetMeals()
        {
            return _context.Meals.AsQueryable();
        }

        public async Task<IEnumerable<Meal>> GetMealsAsync(int pageNumber, int pagesize, NutritionRange range, bool? ascendingSort)
        {
            var meals = _context.Meals
                .Where(m => m.Calories <= range.MaxCalories && m.Calories >= range.MinCalories && m.Protein <= range.MaxProtein && m.Protein >= range.MinProtein && m.Carbonhydrates <= range.MaxCarbonHydrates && m.Carbonhydrates >= range.MinCarbonHydrates && m.Fat >= range.MinFat && m.Fat <= range.MaxFat)
                .Skip((pageNumber - 1) * pagesize)
                .Take(pagesize);

            SearchByName(ref meals, range.Name);

            if (ascendingSort == null)
                return await meals.ToListAsync();
            else if ((bool)ascendingSort)
                return await meals.OrderBy(m => m.Name).ToListAsync();
            else
                return await meals.OrderByDescending(m => m.Name).ToListAsync();

        }
        private void SearchByName(ref IQueryable<Meal> meals, string nameOfMeal)
        {
            if (!meals.Any() || string.IsNullOrWhiteSpace(nameOfMeal))
            {
                return;
            }

            meals = meals.Where(m => m.Name.ToLower().Contains(nameOfMeal.Trim().ToLower()));

        }


        public async Task AddMealAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;

            throw new NotImplementedException();
        }

        public async Task<int> CountMealAsync()
        {
            return await _context.Meals.CountAsync();

            throw new NotImplementedException();
        }

        public async Task DeleteMealAsync(Meal meal)
        {
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;

            throw new NotImplementedException();
        }

        public IQueryable<Meal> GetAllMeals()
        {
            throw new NotImplementedException();
        }

        public async Task<Meal> GetMealByIdAsync(Guid id)
        {
            return await _context.Meals.SingleOrDefaultAsync(x => x.Id == id);

            throw new NotImplementedException();
        }

        public Task<IEnumerable<Meal>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMealAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
