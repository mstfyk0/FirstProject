using Application.Dtos.MealDtos;
using Application.Interfaces;
using Domain.Additional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondProject.Exceptions;
using SecondProject.Filters;
using SecondProject.Helpers;
using SecondProject.Wrapper;
using System.Web.Http.OData;

namespace SecondProject.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("meals")]
    public class MealsController : ControllerBase
    {
        private readonly IMealsService _mealsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MealsController> _logger;

        public MealsController (IMealsService mealsService, IWebHostEnvironment webHostEnvironment, ILogger<MealsController> logger)
        {
            _mealsService = mealsService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [HttpGet("all")]
        [EnableQuery]
        public ActionResult<IQueryable<MealDto>>  GetAllMeals() {
            _logger.LogInformation("Getting all meals from the database as queryable.");
            return Ok(_mealsService.GetAllMeals());

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDto>>> GetPagedMeals([FromQuery] PaginationFilter paginationFilter, [FromQuery] NutritionValuesFilter nUtritionValuesFilter , [FromQuery] bool? ascendingSort )
        {
            _logger.LogInformation("Getting paged meals from the database.");
            
            if (!nUtritionValuesFilter.ValidFilterValues()) {

                _logger.LogInformation("Invalid ranges for nutrition values filter were passed.");
                throw new InvalidFilterRangesException("Invalid ranges of nutrition values.");
            }
            var nutritionRange = new NutritionRange(nUtritionValuesFilter.MinCalories, nUtritionValuesFilter.MaxCalories, nUtritionValuesFilter.MinProtein, nUtritionValuesFilter.MaxProtein, nUtritionValuesFilter.MinCarbonhydrates, nUtritionValuesFilter.MaxCarbonhydrates, nUtritionValuesFilter.MinFat, nUtritionValuesFilter.MaxFat,nUtritionValuesFilter.Name);

            var validPagingFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var meals = await _mealsService.GetMealsPagedAsync(validPagingFilter.PageNumber, validPagingFilter.PageSize, nutritionRange, ascendingSort);
            var totalRecords = await _mealsService.CountMealsAsync();

            return Ok(PaginationHelper.CreatePagedResponse(meals, validPagingFilter, totalRecords));
        }

        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<ActionResult<MealDto>> GetMeal(Guid id)
        {
            _logger.LogInformation("Getting the meal from the database by id.");
            var meal=  await _mealsService.GetMealByIdAsync(id);
            
            if (meal == null)
            {
                _logger.LogInformation("The meal was not found in the database.");
                throw new EntityNotFoundException("The passed id is wrong - the meal doesn't exist.");

            }
            return Ok(new Response<MealDto>(meal));
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddMeal([FromForm]CreateMealDto newMeal)
        {
            _logger.LogInformation("Adding the meal to the database.");
                
            string mealPhotoPath;
            if (newMeal.Image == null || newMeal.Image.Length ==0 ) {

                mealPhotoPath = "";
                _logger.LogInformation("The meal is being added without a photo.");
            }
            else
            {
                string unigueId= Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath,"UploadImages", unigueId+newMeal.Image.FileName);
                using (FileStream stream = new FileStream(path,FileMode.Create))
                {
                    await newMeal.Image.CopyToAsync(stream);    
                    stream.Close();
                }
                mealPhotoPath = path;
            }
            var meal = await _mealsService.AddMealAsync(newMeal,mealPhotoPath);
            if (meal==null)
            {
                _logger.LogInformation("Adding meal has not succeedeed.");
                throw new EntityValidationException("The meal you are trying to add has invalid properties.");
            }
            return Created($"/meals.{meal.Id}",new Response<MealDto>(meal));

        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMeal([FromForm]UpdateMealDto updateMeal, Guid id  )
        {
            _logger.LogInformation("Updating the meal from the database.");
            var meal = _mealsService.GetMealByIdAsync(id);

            if (meal==null)
            {
                _logger.LogInformation("The meal to update was not found in the database.");
                throw new EntityNotFoundException("The passed id is wrong.");
            }
            var mealPhotoPath = await _mealsService.GetPathOfMealImage(id);
            if (System.IO.File.Exists(mealPhotoPath))
            {
                System.IO.File.Delete(mealPhotoPath);
            }
            if (updateMeal.Image==null ||  updateMeal.Image.Length==0)
            {
                mealPhotoPath = "";
                _logger.LogInformation("The meal is being updated without a photo.");

            }else
            {
                string uniqueId = Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath,"UpdateImages", uniqueId+updateMeal.Image.FileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    await updateMeal.Image.CopyToAsync(fileStream); 
                    fileStream.Close();
                }
                mealPhotoPath = path;
            }
            var newMeal = await _mealsService.UpdateMealAsync(updateMeal,id , mealPhotoPath );

            if (newMeal == null)
            {
                _logger.LogInformation("Updating meal has not succeeded.");
                throw new EntityValidationException("The updated properties are not valid.");
            }
            return CreatedAtAction("Update", new Response<MealDto>(newMeal));

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(Guid id)
        {
            _logger.LogInformation("Deleting the meal from the database.");
            var meal = _mealsService.GetMealByIdAsync(id);
            if (meal == null)
            {
                _logger.LogInformation("The meal to delete was not found in the database.");
                throw new EntityNotFoundException("The passed id is wrong.");
            }
            var mealPhotoPath = await _mealsService.GetPathOfMealImage(id);
            if (System.IO.File.Exists(mealPhotoPath))
            {
                System.IO.File.Delete(mealPhotoPath);   
            }
            await _mealsService.DeleteMealAsync(id);
            return NoContent();

        }


    }
}
