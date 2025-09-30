using Clean.Application.Dtos.BaseDtos;
using Clean.Application.Dtos.Categories.Responses;
using MediatR;

namespace Clean.Application.UseCase.Queries.Categories
{
    public record GetCategoryQuery(BaseFilterDto Filter) : IRequest<PaginateViewModel<IEnumerable<GetCategoryDto>>>;
}
