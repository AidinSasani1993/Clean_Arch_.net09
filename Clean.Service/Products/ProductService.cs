using Clean.Application.Dtos.Products.Requests;
using Clean.Application.Repositories;
using Clean.Application.Services.ProductSevices;
using Clean.Domain.Entities;

namespace Clean.Service.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<long> CreateAsync(CreateProductDto dto)
        {
            var product = Product.Create(dto.Title, dto.Amount, dto.Fee, dto.CategoryRef, dto.Code);
            await _productRepository.CreateAsync(product);
            return product.Id;
        }
    }
}
