using Application.Dtos.ProductDtos;
using Application.Interfaces;
using Domain.Additional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondProject.Exceptions;
using SecondProject.Filters;
using SecondProject.Helpers;
using SecondProject.Wrapper;
using Microsoft.AspNetCore.OData.Query;
namespace SecondProject.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("prodcuts")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger _logger;

        public ProductsController(IProductsService productsService, IWebHostEnvironment webHostEnvironment, ILogger logger)
        {
            _productsService = productsService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet("all")]
        [EnableQuery]

        public ActionResult<IQueryable<ProductDto>> GetAllProducts()
        {
            _logger.LogInformation("Getting all products from the database as queryable.");
            return Ok(_productsService.GetAllProducts());
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetPagedProducts([FromQuery] PaginationFilter paginationFilter, [FromQuery] NutritionValuesFilter nutritionValuesFilter , [FromQuery] bool? ascendingSort )
        {
            _logger.LogInformation("Getting paged products from the database.");
            if (!nutritionValuesFilter.ValidFilterValues() ) {
                _logger.LogInformation("Invalid ranges for nutrition values filter were passed.");
                throw new InvalidFilterRangesException("Invalid ranges of nutrition values.");
            }
            var nutritionRange = new NutritionRange(nutritionValuesFilter.MaxCalories, nutritionValuesFilter.MinCalories, nutritionValuesFilter.MinProtein, nutritionValuesFilter.MaxProtein, nutritionValuesFilter.MinCarbonhydrates, nutritionValuesFilter.MaxCarbonhydrates, nutritionValuesFilter.MinFat, nutritionValuesFilter.MaxFat, nutritionValuesFilter.Name);

            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var products = await _productsService.GetAllProductsPagedAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize, nutritionRange, ascendingSort);

            var totalRecords = await _productsService.CountProductAsync();
            return Ok(PaginationHelper.CreatePagedResponse(products,validPaginationFilter,totalRecords));
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 120 , Location = ResponseCacheLocation.Any, VaryByQueryKeys =new string[] {"id"} )]
        [HttpGet("{id}")]

        public async Task<ActionResult<ProductDto>> GetProduct(Guid id )
        {
            _logger.LogInformation("Getting the product from the database by id.");

            var product = await _productsService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogInformation("The product was not found in the database.");
                throw new EntityNotFoundException("The passed id is wrong - the product doesn't exist.");
            }
            return Ok(new Response<ProductDto>(product));

        }
        [Authorize]
        [HttpPost]

        public async Task<ActionResult> AddProduct([FromForm]CreateProductDto newProduct)
        {
            _logger.LogInformation("Adding the product to the database.");

            string prodcutPhotoPath;

            if (newProduct.Image == null || newProduct.Image.Length == 0  ) {
                prodcutPhotoPath = "";
                _logger.LogInformation("The meal is being added without a photo");
            
            }
            else
            {
                string uniqueId = Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath , "UploadImages", uniqueId + newProduct.Image.FileName);

                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    await newProduct.Image.CopyToAsync(fileStream);
                    fileStream.Close(); 

                }
                prodcutPhotoPath = path;
            }
            var product = await _productsService.AddProductAsync(newProduct,prodcutPhotoPath);

            if (product == null ) {
                _logger.LogInformation("Adding product has not succeedeed.");
                throw new EntityValidationException("The product you are trying to add has invalid properties.");            
            }
            return Created($"/products.{product.Id}", new Response<ProductDto>(product));
        }
        [Authorize]
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateProduct([FromForm]UpdateProductDto updateProductDto,Guid id )
        {
            _logger.LogInformation("Updating the product from the database.");
            var product = _productsService.GetProductByIdAsync(id);

            if (product == null ) {
                _logger.LogInformation("The product to update was not found in the database.");
                throw new EntityNotFoundException("The passed id wrong.");
            }

            string productPhotoPath = await _productsService.GetPathOfProductImage(id);
            if (System.IO.File.Exists(productPhotoPath))
            {
                System.IO.File.Delete(productPhotoPath);
            }else
            {
                string uniqueId=Guid.NewGuid().ToString();  
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedImages", uniqueId+ updateProductDto.Image.FileName);
                using (FileStream fileStream = new FileStream(productPhotoPath, FileMode.Create))
                {
                    await updateProductDto.Image.CopyToAsync(fileStream);
                    fileStream.Close();
                }
                productPhotoPath = path;
            }
            var newProduct = await _productsService.UpdateProductAsync(updateProductDto, id, productPhotoPath);
            if (newProduct == null)
            {
                _logger.LogInformation("Updating product has not succeeded.");
                throw new EntityValidationException("The updated properties are not valid.");
            }
            return CreatedAtAction("Update",new Response<ProductDto>(newProduct));
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteProduct(Guid id  )
        {
            _logger.LogInformation("Deleting the meal from the database.");
            var product = await _productsService.GetProductByIdAsync(id);   
            if (product == null) {
                _logger.LogInformation("The product to delete was not found in the database.");
                throw new EntityNotFoundException("The passed id is wrong.");
            }
            string productPhotoPath = await _productsService.GetPathOfProductImage(id);
            if (System.IO.File.Exists(productPhotoPath))
            {
                System.IO.File.Delete(productPhotoPath);
            }
            await _productsService.DeleteProductAsync(id);  
            return NoContent(); 
        }


    }
}
