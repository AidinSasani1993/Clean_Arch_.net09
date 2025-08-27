using Clean.Application.Dtos.Categories.Responses;

namespace Clean.Application.Services.CategoryServices
{
    public interface ICategoryDapperService
    {
        Task<IEnumerable<GetCategoryDto>> ReadAllAsync(string? title);
    }
}
