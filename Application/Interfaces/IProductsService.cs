
using Application.Dtos.ProductDtos;
using Domain.Additional;

namespace Application.Interfaces
{
    public interface IProductsService
    {
        IQueryable<ProductDto> GetAllProducts();
        Task<IEnumerable<ProductDto>> GetAllProductsPagedAsync(int pageNumber,int pageSize, NutritionRange range,bool? ascendingSorting);
        Task<ProductDto> AddProductAsync(CreateProductDto newProductDto, string productPhotoPath);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto,Guid id , string productPhotoPath);
        Task DeleteProductAsync(Guid id);
        Task<int> CountProductAsync();
        Task<string> GetPathOfProductImage(Guid id);
    }
}
