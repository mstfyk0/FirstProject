
using Domain.Additional;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductsRepository
    {
        IQueryable<Product> GetAllProducts();
        Task<IEnumerable<Product>> GetProductsPagedAsync(int pageNumber,int pageSize,NutritionRange range,bool? ascendingSorting);
        Task<Product> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<int> CountProductAsync();


    }
}
