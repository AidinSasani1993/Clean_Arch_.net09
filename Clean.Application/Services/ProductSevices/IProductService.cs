using Clean.Application.Dtos.Products.Requests;

namespace Clean.Application.Services.ProductSevices
{
    public interface IProductService
    {
        Task<long> CreateAsync(CreateProductDto dto);
    }
}
