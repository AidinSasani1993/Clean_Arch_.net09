using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Categories.Requests;
using Clean.Application.Dtos.Categories.Responses;

namespace Clean.Application.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<PaginateViewModel<IEnumerable<GetCategoryDto>>> GetAllAsync(BaseFilterDto Dto);
        Task<GetCategoryDto> GetById(long id);
        Task<long> CreateAsync(CategoryDto dto);
        Task<long> UpdateAsync(long id, CategoryDto dto);
        Task DeleteAsync(long id);
        Task ActiveAsync(long id);
    }
}
