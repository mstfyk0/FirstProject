
using Application.Dtos.MealDtos;
using Application.Interfaces;
using Domain.Additional;
using Domain.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondProject.Controllers;
using SecondProject.Filters;
using Xunit;
using Xunit.Sdk;

namespace SecondProjectTesting.ControllersTests
{
    public class VerifiableMealsLogger : ILogger<MealsController> 
    {
        public int calledCount { get; set; }

        public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();

        public bool IsEnabled(LogLevel logLevel) => throw new NotImplementedException();
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                calledCount++;

            }
            catch (Exception)
            {

                throw new NotImplementedException();

            }
        }
    }

    public class MealsControllersTest
    {

        private readonly IMealsService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly VerifiableMealsLogger _logger;
        public MealsControllersTest()
        {
            _service = A.Fake<IMealsService>();
            _webHostEnvironment = A.Fake<IWebHostEnvironment>();
            _logger = new VerifiableMealsLogger();
        }

        [Fact]
        public void MealsController_GetAllMeals_ReturnOk()
        {
            //Arrange
            var meals = A.Fake<IQueryable<MealDto>>();
            A.CallTo(() => _service.GetAllMeals()).Returns(meals);
            var controller = new MealsController(_service, _webHostEnvironment, _logger);

            //Act
            var actionResult = controller.GetAllMeals();

            //Assert 
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            _logger.calledCount.Equals(1);

        }

        [Fact]
        public async void MealsController_GetMeal_ReturnOk()
        {
            //Arrange
            var meal = A.Fake<MealDto>();
            var id = Guid.NewGuid();
            A.CallTo(() => _service.GetMealByIdAsync(id)).Returns(meal);
            var controller = new MealsController(_service, _webHostEnvironment, _logger);

            //Act
            var actionResult = await controller.GetMeal(id);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Should().NotBeOfType(typeof(OkObjectResult));
            _logger.calledCount.Equals(1);
        }
        [Fact]
        public async void MealsController_GetPagedMeals_ReturnOK()
        {
            //Arrange
            var meals = A.Fake<IEnumerable<MealDto>>();
            var validPaginationFilter = new PaginationFilter(1, 1);
            var nutritionValuesFilter = new NutritionValuesFilter();
            var nutritionRange = new NutritionRange(nutritionValuesFilter.MinCalories, nutritionValuesFilter.MaxCalories, nutritionValuesFilter.MinProtein, nutritionValuesFilter.MaxProtein, nutritionValuesFilter.MinCarbonhydrates, nutritionValuesFilter.MaxCarbonhydrates, nutritionValuesFilter.MinFat, nutritionValuesFilter.MaxFat, nutritionValuesFilter.Name);

            bool? ascendingSort = true;
            A.CallTo(() => _service.GetMealsPagedAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize, nutritionRange, ascendingSort)).Returns(meals);

            A.CallTo(() => _service.CountMealsAsync()).Returns(10);
            var controller = new MealsController(_service, _webHostEnvironment, _logger);

            //Act
            var actionResult = await controller.GetPagedMeals(new PaginationFilter(1, 1), nutritionValuesFilter, ascendingSort);

            //Assert    
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            _logger.calledCount.Equals(1);
        }
        [Fact]
        public async void MealsController_AddMeal_ReturnCreated()
        {
            //Arrange 
            var newMeal = A.Fake<CreateMealDto>();
            var meal = A.Fake<MealDto>();
            string mealPhotoPath = "";

            A.CallTo(() => _service.AddMealAsync(newMeal, mealPhotoPath)).Returns(meal);
            var controller = new MealsController(_service, _webHostEnvironment, _logger);
            //Act
            var actionResult = await controller.AddMeal(newMeal);

            //Assert
            var result = actionResult as CreatedResult;
            result.Should().NotBeNull();
            result.Should().NotBeOfType(typeof(CreatedResult));
            _logger.calledCount.Equals(2);
        }
        [Fact]

        public async void MealsController_UpdateMeal_ReturnCreatedAction()
        {
            //Arrange
            var newMeal = A.Fake<UpdateMealDto>();
            var id = Guid.NewGuid();
            var meal = A.Fake<MealDto>();
            var mealPhotoPath = "";
            A.CallTo(() => _service.GetMealByIdAsync(id)).Returns(meal);
            A.CallTo(() => _service.UpdateMealAsync(newMeal, id, mealPhotoPath));
            var controller = new MealsController(_service, _webHostEnvironment, _logger);

            //Act
            var actionResult = await controller.UpdateMeal(newMeal, id);

            //Assert
            var result = actionResult as CreatedAtActionResult;
            result.Should().NotBeNull();
            result.Should().NotBeOfType<CreatedAtActionResult>();
            _logger.calledCount.Equals(2);
        }
        [Fact]
        public async void MealsController_DeleteMeal_ReturnNoConten()
        {
            //Arrange
            var meal = A.Fake<MealDto>();
            var id = Guid.NewGuid();
            A.CallTo(() => _service.GetMealByIdAsync(id)).Returns(meal);
            A.CallTo(() => _service.GetPathOfMealImage(id)).Returns("");
            A.CallTo(() => _service.DeleteMealAsync(id)).DoesNothing();
            var controller = new MealsController(_service, _webHostEnvironment, _logger);

            //Act
            var actionResult = await controller.DeleteMeal(id);

            //Assert
            var result = actionResult as NoContentResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            _logger.calledCount.Equals(1);

        }
    }
}
