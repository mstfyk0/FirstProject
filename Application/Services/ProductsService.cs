

using Application.Dtos.ProductDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Additional;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services
{
    public class ProductsService : IProductsService
    {
        public readonly IProductsRepository _repository;
        public readonly IValidator<Product> _validator;
        public readonly IMapper _mapper;

        public  ProductsService(IProductsRepository repository, IValidator<Product> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }   


        public async Task<ProductDto> AddProductAsync(CreateProductDto newProductDto, string productPhotoPath)
        {
            try
            {
                var product = _mapper.Map<Product>(newProductDto);
                product.ProductPhotoPath = productPhotoPath;
                ValidationResult result = await _validator.ValidateAsync(product);
                if (!result.IsValid) { return null; }

                await _repository.AddProductAsync(product);
                return _mapper.Map<ProductDto>(product);    

            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
           
        }

        public async Task<int> CountProductAsync()
        {
            try
            {
                return await _repository.CountProductAsync();
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task DeleteProductAsync(Guid id)
        {
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                await _repository.DeleteProductAsync(product);
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            
            }
        }

        public IQueryable<ProductDto> GetAllProducts()
        {
            try
            {
                var product = _repository.GetAllProducts();
                return _mapper.ProjectTo<ProductDto>(product);

            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsPagedAsync(int pageNumber, int pageSize, NutritionRange range, bool? ascendingSorting)
        {
            try
            {
                var product = await _repository.GetProductsPagedAsync(pageNumber, pageSize, range, ascendingSorting);
                return _mapper.Map<IEnumerable<ProductDto>>(product);
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task<string> GetPathOfProductImage(Guid id)
        {
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                return product.ProductPhotoPath;    

            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            try
            {
                var product = await GetProductByIdAsync(id);
                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto, Guid id, string productPhotoPath)
        {
            try
            {
                var existingProduct = await _repository.GetProductByIdAsync(id);
                var product = _mapper.Map(updateProductDto, existingProduct);
                product.ProductPhotoPath = productPhotoPath;

                ValidationResult result = await _validator.ValidateAsync(product);
                if (!result.IsValid) { return null; }

                await _repository.UpdateProductAsync(product);  
                return _mapper.Map<ProductDto>(product);

            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }
    }
}
