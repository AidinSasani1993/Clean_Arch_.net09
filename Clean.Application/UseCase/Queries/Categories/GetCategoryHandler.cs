using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Categories.Responses;
using Clean.Application.Services.CategoryServices;
using MediatR;

namespace Clean.Application.UseCase.Queries.Categories
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, PaginateViewModel<IEnumerable<GetCategoryDto>>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<PaginateViewModel<IEnumerable<GetCategoryDto>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllAsync(request.Filter);
        }
    }
}
