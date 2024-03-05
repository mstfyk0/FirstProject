using Domain.Additional;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    // class working on sql database, more specifically on products table
    public class SQLProductsRepository : IProductsRepository
    {
        private readonly FitnessPlannerContext _context;

        public SQLProductsRepository(FitnessPlannerContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;

            throw new NotImplementedException();
        }

        public async Task<int> CountProductAsync()
        {
            return await _context.Products.CountAsync();
            throw new NotImplementedException();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetAllProducts()
        {

            return _context.Products.AsQueryable();

            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(Guid id)
        {
            return _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting)
        {
            var products = _context.Products
                .Where(m => m.Calories <= range.MaxCalories && m.Calories <= range.MinCalories && m.Protein <= range.MaxProtein && m.Protein <= range.MinProtein && m.Carbonhydrates <= range.MaxCarbonHydrates && m.Carbonhydrates <= range.MinCarbonHydrates && m.Fat <= range.MaxFat && m.Fat <= range.MinFat)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            SearchByname(ref products, range.Name);

            if (ascendingSorting == null)
                return await products.ToListAsync();
            else if ((bool)ascendingSorting)
                return await products.OrderBy(x => x.Name).ToListAsync();
            else
                return await products.OrderByDescending(x => x.Name).ToListAsync();

            throw new NotImplementedException();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;

            throw new NotImplementedException();
        }
        private void SearchByname(ref IQueryable<Product> products, string nameOfProduct)
        {
            if (!products.Any() || string.IsNullOrWhiteSpace(nameOfProduct))
                return;
            products = products.Where(x => x.Name.ToLower().Contains(nameOfProduct.Trim().ToLower()));

        }

    }
}
