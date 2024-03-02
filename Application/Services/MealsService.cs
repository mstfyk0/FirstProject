
using Application.Dtos.MealDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Additional;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services
{
    //service operating on a meals' repository, meals controller is using its functions
    internal class MealsService : IMealsService
    {
        private readonly IMapper _mapper;
        private readonly IMealsRepository _repository;
        private readonly IValidator<Meal> _validator;

        public MealsService(IMapper mapper, IMealsRepository repository, IValidator<Meal> validator)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        public async Task<MealDto> AddMealAsync(CreateMealDto newMeal, string mealPhotoPath)
        {
            try
            {
                var meal = _mapper.Map<Meal>(newMeal);
                meal.MealPhotoPath = mealPhotoPath;

                ValidationResult resutl = await _validator.ValidateAsync(meal);
                if (!resutl.IsValid) { return null; }

                await _repository.AddMealAsync(meal);
                return _mapper.Map<MealDto>(meal);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

           
        }

        public async Task<int> CountMealsAsync()
        {
            try
            {
                return await _repository.CountMealAsync();
            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }

        public async Task DeleteMealAsync(Guid id)
        {
            try
            {
                var meal = await _repository.GetMealByIdAsync(id);
                await _repository.DeleteMealAsync(meal);    
            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }

        public IQueryable<MealDto> GetAllMeals()
        {
            try
            {
                var meals = _repository.GetAllMeals();
                return _mapper.ProjectTo<MealDto>(meals);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<MealDto> GetMealByIdAsync(Guid id)
        {
            try
            {
                var meal = await _repository.GetMealByIdAsync(id);
                return _mapper.Map<MealDto>(meal);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
          
        }

        public async Task<IEnumerable<MealDto>> GetMealsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSort)
        {
            try
            {
                var meals = await _repository.GetMealsPagedAsync(pageNumber, pageSize, range, ascendingSort);
                return _mapper.Map<IEnumerable<MealDto>>(meals);
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }

        }

        public async Task<string> GetPathOfMealImage(Guid id)
        {
            try
            {
                var meal = await _repository.GetMealByIdAsync(id);
                return meal.MealPhotoPath;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task<MealDto> UpdateMealAsync(UpdateMealDto updateMeal, Guid id, string mealPhotoPath)
        {
            try
            {
                var existingMeal= await _repository.GetMealByIdAsync(id);
                var meal = _mapper.Map(updateMeal, existingMeal);
                meal.MealPhotoPath= mealPhotoPath;
                ValidationResult result = await _validator.ValidateAsync(meal);
                if (result.IsValid) { return null; }

                await _repository.UpdateMealAsync(meal);
                return _mapper.Map<MealDto>(meal);

            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }

           
        }
    }
}
