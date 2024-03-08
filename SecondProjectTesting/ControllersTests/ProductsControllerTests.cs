using Application.Dtos.ProductDtos;
using Application.Interfaces;
using Castle.Core.Logging;
using Domain.Additional;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondProject.Controllers;
using SecondProject.Filters;
using Xunit;

namespace SecondProjectTesting.ControllersTests
{
    public class VerifiableProductsLogger : ILogger<ProductsController>
    {
        public int calledCount { get; set; }    

        public IDisposable? BeginScope<TState>(TState state) => throw new NotImplementedException();

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
    public class ProductsControllerTests
    {
        private readonly IProductsService _productsService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly VerifiableMealsLogger _logger;

        public ProductsControllerTests()
        {
            _productsService = A.Fake<IProductsService>();
            _webHostEnvironment = A.Fake<IWebHostEnvironment>();
            _logger = new VerifiableMealsLogger();

        }
        [Fact]
        public async void ProductsController_GetAllProducts_ReturnOK()
        {
            //Arrange
            var products = A.Fake<IQueryable<ProductDto>>();

            A.CallTo(()=> _productsService.GetAllProducts()).Returns(products);
            var controller = new ProductsController(_productsService, _webHostEnvironment, _logger);

            //Act
            var actionResult = controller.GetAllProducts();

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            _logger.calledCount.Equals(1);
        }

        [Fact]
        public async void ProductsController_GetProduct_ReturnOK()
        {
            var product = A.Fake<ProductDto>();
            var id = Guid.NewGuid();
            A.CallTo(() => _productsService.GetProductByIdAsync(id)).Returns(product);
            var controller = new ProductsController(_productsService,_webHostEnvironment, _logger);

            //Act 
            var actionResult = await controller.GetProduct(id);

            //Assert 
            var result= actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>(); // (typeof(OkObjectResult)) şeklinde yazılmadı. Eğer hata alınırsa buradan kaynaklı olabilir
            _logger.calledCount.Equals(1);   
        }
        [Fact]

        public async void ProductsController_GetPagedProducts_ReturnOK()
        {
            var products = A.Fake<IEnumerable<ProductDto>>();
            var validPaginationFilter = new PaginationFilter(1, 1);
            var nutritionValuesFilter = new NutritionValuesFilter();
            var nutritionRange = new NutritionRange(nutritionValuesFilter.MinCalories, nutritionValuesFilter.MaxCalories, nutritionValuesFilter.MinProtein, nutritionValuesFilter.MaxProtein, nutritionValuesFilter.MinCarbonhydrates, nutritionValuesFilter.MaxCarbonhydrates, nutritionValuesFilter.MinFat, nutritionValuesFilter.MaxFat, nutritionValuesFilter.Name);
            bool? ascendingSort = true;
            A.CallTo(() => _productsService.GetAllProductsPagedAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize, nutritionRange, ascendingSort));
            A.CallTo(() =>  _productsService.CountProductAsync()).Returns(10);
            var controller = new ProductsController(_productsService, _webHostEnvironment, _logger);

            //Act 
            var actionResult = await controller.GetPagedProducts(new  PaginationFilter(1,1),nutritionValuesFilter,ascendingSort);
            
            //Assert
            var result= actionResult.Result as OkObjectResult ;
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            _logger.calledCount.Equals(1);
        }
        [Fact]
        public async void ProductsController_AddProduct_ReturnCreated()
        {
            //Arrange 
            var newProduct = A.Fake<CreateProductDto>();
            var product = A.Fake<ProductDto>();
            string productPhotoPath = "";
            A.CallTo(() => _productsService.AddProductAsync(newProduct, productPhotoPath)).Returns(product);
            var controller = new ProductsController(_productsService,_webHostEnvironment,_logger);

            //Act
            var actionResult = await controller.AddProduct(newProduct);

            //Arrange
            var result = actionResult as CreatedResult; 
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(CreatedResult));
            _logger.calledCount.Equals(2);
        }

        [Fact]

        public async void ProductsController_UpdateProduct_ReturnCreatedAtAction ()
        {
            //Arrange 
            var newProduct = A.Fake<UpdateProductDto>();
            var id = Guid.NewGuid();
            var product= A.Fake<ProductDto>();
            var productPhotoPath = "";
            A.CallTo(() => _productsService.GetProductByIdAsync(id));

            A.CallTo(() => _productsService.UpdateProductAsync(newProduct, id, productPhotoPath));
            var controller = new ProductsController (_productsService,_webHostEnvironment,_logger);

            //Act
            var actionResult = await controller.UpdateProduct(newProduct, id);

            //Arrange
            var result = actionResult as CreatedAtActionResult;
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
            _logger.calledCount.Equals(2);  

        }
        
        [Fact]
        public async void ProductsController_DeleteProduct_ReturnNoContent()
        {
            //Arrange
            var product = A.Fake<ProductDto>();
            var id = Guid.NewGuid();
            A.CallTo(() => _productsService.GetProductByIdAsync(id)).Returns(product);
            A.CallTo(() => _productsService.GetPathOfProductImage(id)).Returns("");
            A.CallTo(() => _productsService.DeleteProductAsync(id)).DoesNothing();
            var controller = new ProductsController(_productsService,_webHostEnvironment,_logger);
            //Act
            var actionResult = await controller.DeleteProduct(id);

            //Assert 
            var result = actionResult as NoContentResult ;
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();    
            _logger.calledCount.Equals(1);


        }

    }

}
