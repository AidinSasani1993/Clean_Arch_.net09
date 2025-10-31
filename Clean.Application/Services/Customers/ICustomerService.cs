using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Customers;

namespace Clean.Application.Services.Customers
{
    public interface ICustomerService
    {
        Task<long> CreateAsync(CustomerDto dto);
        Task<long> UpdateAsync(CustomerDto dto);
        Task DeleteAsync(long id);
        Task<CustomerDto> GetByIdAsync(long id);
        Task<PaginateViewModel<IEnumerable<CustomerDto>>> GetAllAsync(BaseFilterDto filter);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
    }
}
